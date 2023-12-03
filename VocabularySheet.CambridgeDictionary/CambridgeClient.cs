using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.Extensions.Logging;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Domain.Extensions;

namespace VocabularySheet.CambridgeDictionary;

public class CambridgeClient
{
    public const string Base = "https://dictionary.cambridge.org";
    private const string BaseDictionary = $"{Base}/dictionary";

    private readonly HttpClient _httpClient;
    private readonly ILogger<CambridgeClient> _logger;

    public CambridgeClient(HttpClient httpClient, ILogger<CambridgeClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string> Page(string word, WordLanguage language, CancellationToken cancellationToken)
    {
        string link = $"{BaseLang(language)}/{word}";

        _logger.LogInformation("Full link: {link}", link);

        HttpResponseMessage httpResponse = await _httpClient.GetAsync(link, cancellationToken);
        return await httpResponse.Content.ReadAsStringAsync(cancellationToken);
    }

    private static string BaseLang(WordLanguage language)
    {
        string lang = language switch
        {
            WordLanguage.Ua => "english-ukrainian",
            WordLanguage.Ru => "english-russian",
            _ => "english",
        };

        return $"{BaseDictionary}/{lang}";
    }
}

public class CambridgeParser
{
    private static string? StripHtml(string? html)
    {
        if (html == null)
        {
            return null;
        }
        
        // This regular expression removes HTML tags from the input string
        return Regex.Replace(html, "<.*?>", string.Empty);
    }
    
    public async Task<CambridgePageWithHtml?> Page(string html, CancellationToken cancellationToken)
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
            .QuerySelector(".pr.dictionary")
            ?.QuerySelector(".di-body")
            ?.QuerySelectorAll(".entry");

        var subBlocksEls = pageContentEl
            .QuerySelector(".pr.dictionary")
            ?.QuerySelectorAll(".dlink");

        blocksEls ??= pageContentEl
            .QuerySelectorAll(".entry-body__el");
        
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
        
        return new CambridgePageWithHtml()
        {
            Title = mainTitle,
            HtmlContent = pageContentEl.OuterHtml,
            Blocks = SetBlocks(blocksEls),
            SubBlocks = SetSubBlocks(subBlocksEls),
            Ref = refItem,
        };
    }

    private List<CambridgeWordSubBlock> SetSubBlocks(IHtmlCollection<IElement>? subBlocksEls)
    {
        List<CambridgeWordSubBlock> blocks = new();
        if (subBlocksEls == null)
        {
            return blocks;
        }

        foreach (var subBlock in subBlocksEls)
        {
            var header = subBlock.QuerySelector(".di-head");
            var subArticles = subBlock.QuerySelectorAll(".def-block");
            blocks.Add(new CambridgeWordSubBlock()
            {
                Title = header?.QuerySelector("h2")?.InnerHtml,
                Category = header?.QuerySelector(".dpos")?.InnerHtml,
                Other = header?.QuerySelector(".lmr-0")?.InnerHtml,
                Audios = SetAudio(new[] { subBlock }),
                SubArticles = SetSubArticles(subArticles),
            });
        }
        
        return blocks;
    }

    private static List<CambridgeWordBlock> SetBlocks(IHtmlCollection<IElement>? entriesEls)
    {
        List<CambridgeWordBlock> blocks = new();
        if (entriesEls == null)
        {
            return blocks;
        }
        
        foreach (var entry in entriesEls)
        {
            var posHeader = entry.QuerySelector(".pos-header");
            var posBody = entry.QuerySelector(".pos-body");
            var articlesEls = posBody?.QuerySelectorAll(".dsense");

            string? tilte = posHeader?.QuerySelector(".hw")?.InnerHtml;
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
            var subArticles = article.QuerySelectorAll(".def-block");
            articles.Add(new CambridgeArticle()
            {
                Header = new CambridgeArticleHeader()
                {
                    Title = headerEl?.QuerySelector(".hw")?.InnerHtml,
                    Category = headerEl?.QuerySelector(".pos")?.InnerHtml,
                    Meaning = headerEl?.QuerySelector(".dsense_gw")?.QuerySelector("span")?.InnerHtml,
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
                    Title = StripHtml(header?.QuerySelector(".ddef_d")?.InnerHtml),
                    Level = header?.QuerySelector(".epp-xref")?.InnerHtml,
                    Translation = body?.QuerySelector(".dtrans")?.InnerHtml,
                },
                ImageUrl = subArticleEl.QuerySelector(".dimg_i")?.GetAttribute("src"),
                Examples = body?.QuerySelectorAll(".deg")
                    .Select(e => StripHtml(e.InnerHtml))
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
            string? transcription = StripHtml(ipa?.InnerHtml);

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

public record CambridgePage
{
    public required string Title { get; init; }
    public required List<CambridgeWordBlock> Blocks { get; init; }
    public required List<CambridgeWordSubBlock> SubBlocks { get; init; }
    public CambridgeRef? Ref { get; init; }
}

public interface ICambridgeWordBlock
{
    string? Title { get; init; }
    string? Category { get; init; }
    List<string> Irregulars { get; init; }
    List<CambridgeAudio> Audios { get; init; }
}

public record CambridgeWordBlock : ICambridgeWordBlock
{
    public string? Title { get; init; }
    public string? Category { get; init; }
    public List<string> Irregulars { get; init; } = new List<string>();
    public List<CambridgeAudio> Audios { get; init; } = new();
    public List<CambridgeArticle> Articles { get; init; } = new();
}

public record CambridgeRef
{
    public required string Title { get; set; }
    public required string Url { get; set; }
    
    public string FullLink()
    {
        return CambridgeClient.Base + Url;
    }
}

public record CambridgeWordSubBlock : ICambridgeWordBlock
{
    public string? Title { get; init; }
    public string? Category { get; init; }
    public string? Other { get; init; }
    public List<string> Irregulars { get; init; } = new();
    public List<CambridgeAudio> Audios { get; init; } = new();
    public List<CambridgeSubArticle> SubArticles { get; init; } = new();
}

public record CambridgeArticle
{
    public required CambridgeArticleHeader Header { get; init; }
    public List<CambridgeSubArticle> SubArticles { get; init; } = new();
}

public record CambridgeSubArticle
{
    public required CambridgeSubArticleHeader Header { get; init; }
    public List<string> Examples { get; init; } = new();
    public string? ImageUrl { get; init; }
    
    public string? FullImageLink()
    {
        if (ImageUrl == null)
        {
            return null;
        }
        
        return CambridgeClient.Base + ImageUrl;
    }
}

public record CambridgeSubArticleHeader
{
    public string? Title { get; set; }
    public string? Level { get; set; }
    public string? Translation { get; set; }
}

public record CambridgeArticleHeader
{
    public string? Title { get; set; }
    public string? Category { get; set; }
    public string? Meaning { get; set; }
}

public record CambridgeAudio
{
    public string? LanguageCode { get; init; }
    public string? Transcription { get; init; }
    public List<CambridgeAudioLink> Links { get; init; } = new();
}

public record CambridgeAudioLink
{
    public required string Type { get; init; }
    public required string Src { get; init; }

    public string FullLink()
    {
        return CambridgeClient.Base + Src;
    }
}

public interface IHasHtmlContent
{
    string HtmlContent { get; }
};

public record CambridgePageWithHtml : CambridgePage, IHasHtmlContent
{
    public required string HtmlContent { get; init; }

    public CambridgePage WithOutHtml()
    {
        return this with
        {
            HtmlContent = ""
        };
    }
}