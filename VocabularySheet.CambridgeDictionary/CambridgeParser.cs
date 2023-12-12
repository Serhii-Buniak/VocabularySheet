using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using VocabularySheet.CambridgeDictionary.Entities;
using VocabularySheet.Domain.Extensions;
using VocabularySheet.Parsing.Common;

namespace VocabularySheet.CambridgeDictionary;

public class CambridgeParser
{
    public async Task<CambridgeContent?> Page(string html, CancellationToken cancellationToken)
    {
        var htmlParser = new HtmlParser();

        var document = await htmlParser.ParseDocumentAsync(html, cancellationToken);

        var pageContentEl = document.QuerySelector("#page-content");
        if (pageContentEl == null)
        {
            return null;
        }

        var mainTitle = pageContentEl.QuerySelector("h1")?.QuerySelector("b")?.InnerHtml;
        if (mainTitle == null)
        {
            return null;
        }
        
        var blocksEls = pageContentEl
            .QuerySelector(".di.superentry")
            ?.QuerySelector(".di-body")
            ?.QuerySelectorAll(".entry-body__el").ToList() ?? new List<IElement>();

        blocksEls.AddRange(pageContentEl
            .QuerySelectorAll(".pr.idiom-block"));
        
        List<IElement> subBlocks = new List<IElement>();
        
        var dlinks = pageContentEl
            .QuerySelector(".pr.dictionary")
            ?.QuerySelectorAll(".dlink");
        if (dlinks != null)
        {
            subBlocks.AddRange(dlinks);
        }
        var idioms = pageContentEl
            .QuerySelectorAll(".pr.idiom-block");
        subBlocks.AddRange(idioms);
        
        
        
        CambridgeRef? refItem = null;

        var refEl = pageContentEl.QuerySelector(".Ref");
        var href = refEl?.GetAttribute("href");
        var refTitle = refEl?.QuerySelector("span")?.InnerHtml;
        if (href != null && refTitle != null)
        {
            refItem = new CambridgeRef()
            {
                Title = refTitle,
                Url = href,
            };
        }

        List<CambridgeWordBlock> blocks = SetBlocks(blocksEls.ToList());

        blocks.AddRange(SetSubBlocks(subBlocks).Select(sb => new CambridgeWordBlock
        {
            Title = sb.Title,
            Category = sb.Category,
            Irregulars = sb.Irregulars,
            Audios = sb.Audios,
            Articles = new List<CambridgeArticle>()
            {
                new CambridgeArticle()
                {
                    Header = new CambridgeArticleHeader(),
                    SubArticles = sb.SubArticles,
                }
            }
        }));
        
        return new CambridgeContent()
        {
            Title = mainTitle,
            Blocks = blocks,
            Ref = refItem,
        };
    }

    private List<CambridgeWordSubBlock> SetSubBlocks(List<IElement>? subBlocksEls)
    {
        List<CambridgeWordSubBlock> blocks = new();
        if (subBlocksEls == null)
        {
            return blocks;
        }

        foreach (var subBlock in subBlocksEls)
        {
            var header = subBlock.QuerySelector(".di-head");
            
            var phrases = subBlock.QuerySelectorAll(".phrase-block").ToList();
            foreach (var phrase in phrases)
            {
                phrase.Remove();
            }
            
            var subArticles = subBlock.QuerySelectorAll(".def-block").ToList();
            
            subArticles.AddRange(phrases);

            
            var dinfEls = header?.QuerySelectorAll(".dinf");
            List<string> irregulars = new List<string>();
            if (dinfEls != null)
            {
                irregulars = dinfEls.Select(d => d.InnerHtml).ToList();
            }
            
            blocks.Add(new CambridgeWordSubBlock()
            {
                Title = header?.QuerySelector("h2")?.InnerHtml,
                Category = header?.QuerySelector(".dpos")?.InnerHtml,
                Other = header?.QuerySelector(".lmr-0")?.InnerHtml,
                Irregulars = irregulars,
                Audios = SetAudio(new[] { subBlock }),
                SubArticles = SetSubArticles(subArticles),
            });
        }
        
        return blocks;
    }

    private static List<CambridgeWordBlock> SetBlocks(List<IElement> entriesEls)
    {
        List<CambridgeWordBlock> blocks = new();
        
        foreach (var entry in entriesEls)
        {
            var posHeader = entry.QuerySelector(".pos-header");
            var posBody = entry.QuerySelector(".pv-body") 
                          ?? entry.QuerySelector(".pos-body");
            var articlesEls = posBody?.QuerySelectorAll(".dsense");

            string? tilte = entry.QuerySelector("h2")?.InnerHtmlStriped() ?? posHeader?.QuerySelector(".hw")?.InnerHtml;
            string? category = posHeader?.QuerySelector(".pos.dpos")?.InnerHtml 
                               ?? entry.QuerySelector(".pos.dpos")?.InnerHtml;
            
            var audioEls = posHeader?.QuerySelectorAll(".dpron-i");

            var dinfEls = posHeader?.QuerySelectorAll(".dinf");

            List<string> irregulars = new List<string>();
            if (dinfEls != null)
            {
                irregulars = dinfEls.Select(d => d.InnerHtml).ToList();
            }

            blocks.Add(new CambridgeWordBlock()
            {
                Title = tilte,
                Category = category,
                Irregulars = irregulars,
                Audios = SetAudio(audioEls),
                Articles = SetArticles(articlesEls)
            });
        }

        return blocks;
    }

    private static List<CambridgeArticle> SetArticles(IHtmlCollection<IElement>? articlesEls)
    {
        List<CambridgeArticle> articles = new List<CambridgeArticle>();
        if (articlesEls == null)
        {
            return articles;
        }

        foreach (var article in articlesEls)
        {
            var headerEl = article.QuerySelector(".dsense_h");
            
            var phrases = article.QuerySelectorAll(".phrase-block").ToList();
            foreach (var phrase in phrases)
            {
                phrase.Remove();
            }
            
            var subArticles = article.QuerySelectorAll(".def-block").ToList();
            
            subArticles.AddRange(phrases);
            
            articles.Add(new CambridgeArticle()
            {
                Header = new CambridgeArticleHeader()
                {
                    Title = headerEl?.QuerySelector(".hw")?.InnerHtmlStriped(),
                    Category = headerEl?.QuerySelector(".pos")?.InnerHtmlStriped(),
                    Meaning = headerEl?.QuerySelector(".dsense_gw")?.QuerySelector("span")?.InnerHtmlStriped(),
                },
                SubArticles = SetSubArticles(subArticles),
            });
        }
        
        return articles;
    }

    private static List<CambridgeSubArticle> SetSubArticles(IEnumerable<IElement> subArticlesEls)
    {
        List<CambridgeSubArticle> subArticles = new List<CambridgeSubArticle>();

        foreach (var subArticleEl in subArticlesEls)
        {
            var header = subArticleEl.QuerySelector(".ddef_h");
            var body = subArticleEl.QuerySelector(".ddef_b");
            
            subArticles.Add(new CambridgeSubArticle()
            {
                Header = new CambridgeSubArticleHeader()
                {
                    Title = header?.QuerySelector(".ddef_d")?.InnerHtmlStriped(),
                    BlueTitle = subArticleEl.QuerySelector(".dphrase-title")?.InnerHtmlStriped(),
                    Level = header?.QuerySelector(".epp-xref")?.InnerHtml,
                    Translation = body?.QuerySelector(".dtrans")?.InnerHtml,
                },
                ImageUrl = subArticleEl.QuerySelector(".dimg_i")?.GetAttribute("src"),
                Examples = body?.QuerySelectorAll(".deg")
                    .Select(e => e.InnerHtmlStriped())
                    .WhereNotNull()
                    .ToList() ?? new List<string>(),
            });
        }

        return subArticles;
    }
    
    private static List<CambridgeAudio> SetAudio(IEnumerable<IElement>? audioEls)
    {
        List<CambridgeAudio> audios = new List<CambridgeAudio>();
        if (audioEls == null)
        {
            return audios;
        }

        foreach (var audioEl in audioEls)
        {
            var ipa = audioEl.QuerySelector(".ipa");
            string? languageCode = audioEl.QuerySelector(".region")?.InnerHtml;
            string? transcription = ipa?.InnerHtmlStriped();

            var htmlAudioEl = audioEl.QuerySelector("audio");

            var sourceEls = htmlAudioEl?.QuerySelectorAll<IHtmlSourceElement>("source");

            audios.Add(new CambridgeAudio()
            {
                LanguageCode = languageCode,
                Transcription = transcription,
                Links = SetSources(sourceEls),
            });
        }

        return audios;
    }
    
    private static List<CambridgeAudioLink> SetSources(IEnumerable<IHtmlSourceElement>? sourceEls)
    {
        List<CambridgeAudioLink> links = new();

        if (sourceEls == null)
        {
            return links;
        }

        foreach (var source in sourceEls)
        {
            var type = source.Type;
            var src = source.Source;
            if (type != null && src != null)
            {
                links.Add(new CambridgeAudioLink()
                {
                    Type = type,
                    Src = src.Replace("about://", ""),
                });
            }
        }

        return links;
    }
}