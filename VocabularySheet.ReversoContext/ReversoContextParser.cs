using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using VocabularySheet.Domain.Extensions;
using VocabularySheet.Parsing.Common;
using VocabularySheet.ReversoContext.Entities;

namespace VocabularySheet.ReversoContext;

public class ReversoContextParser
{
    public async Task<ReversoContextContent?> Page(string html, CancellationToken cancellationToken)
    {
        var htmlParser = new HtmlParser();

        var document = await htmlParser.ParseDocumentAsync(html, cancellationToken);

        var bodyContentEl = document.QuerySelector("#body-content");
        if (bodyContentEl == null)
        {
            return null;
        }
        
        var categotyLabels = bodyContentEl
            .QuerySelector("#pos-filters")?
            .QuerySelectorAll<IHtmlButtonElement>("button")
            .ToList() ?? new List<IHtmlButtonElement>();
        
        var translationsLabels = bodyContentEl
            .QuerySelector("#translations-content")?
            .QuerySelectorAll("a")
            .ToList() ?? new List<IElement>();
        
        var examples = bodyContentEl
            .QuerySelector("#examples-content")?
            .QuerySelectorAll(".example")
            .ToList() ?? new List<IElement>();
        
        return new ReversoContextContent()
        {
            Title = CreateTitle(bodyContentEl),
            CategoryGroups = CreateCategoryGroups(categotyLabels, translationsLabels),
            Examples = CreateExamples(examples),
        };
    }

    private static string? CreateTitle(IElement bodyContentEl)
    {
        var titleEl = bodyContentEl.QuerySelector(".search-text");
        var innerTrash = titleEl?.QuerySelector(".source-term-pos");
        if (innerTrash != null)
        {
            titleEl?.RemoveChild(innerTrash);
        }
        
        return titleEl?.InnerHtmlStriped();
    }

    private static List<ReversoContextExample> CreateExamples(List<IElement> examples)
    {
        return examples.Select(tl =>
        {
            string? origin = tl
                .QuerySelector(".src")?
                .InnerHtmlStriped();
            string? translation = tl
                .QuerySelector(".trg")?
                .InnerHtmlStriped();
            if (origin == null || translation == null)
            {
                return null;
            }

            return new ReversoContextExample
            {
                Origin = origin,
                Translation = translation,
            };
        }).WhereNotNull().ToList();
    }

    private static List<ReversoContextCetegoryGroup> CreateCategoryGroups(List<IHtmlButtonElement> categotyLabels, List<IElement> translationsLabels)
    {
        var categories = categotyLabels.Select(cl => new ReversoContextCetegory
        {
            Name = cl.InnerHtml.Trim(),
            Type = cl.ClassName != "no-action" ? cl.ClassName : null,
        }).ToList();
        
        var translations = translationsLabels.Select(tl =>
        {
            string? name = tl.QuerySelector(".display-term")?.InnerHtml;
            if (name == null)
            {
                return null;
            }

            return new ReversoContextTranslation
            {
                Name = name,
                Type = tl.QuerySelector(".pos-mark")?.QuerySelector("span")?.ClassName
            };
        }).WhereNotNull().ToList();
        
        return ReversoContextCetegoryGroup.CreateList(categories, translations);
    }
}