using System.Text.Json;
using Domain.Localization;
using Domain.WebSources;
using WebSources.CambridgeDictionary;
using WebSources.CambridgeDictionary.Entities;
using WebSources.ReversoContext;
using WebSources.ReversoContext.Entities;

namespace Infrastructure.Data.Repositories.Pages;

public interface IParsedPageEntry
{
    string Word { get; }
    WordLanguage Language { get; }
    WordLanguage TranslationLanguage { get; }
    string Link { get; init; }
    string JsonContent { get; }
}

public abstract record ParsedPageEntry : IParsedPageEntry
{
    public required string Word { get; init; }
    public required WordLanguage Language { get; init; }
    public required WordLanguage TranslationLanguage { get; init; }
    public required string Html { get; init; }
    public required string Link { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string JsonContent { get; init; }

    public string CacheKey() => $"{Word}_{Language}_{TranslationLanguage}";
}

public record CambridgeEntry : ParsedPageEntry
{
    public static CambridgeEntry Create(CambridgePage page)
    {
        return new CambridgeEntry()
        {
            Word = page.Word,
            Language = page.Language,
            TranslationLanguage = page.TranslationLanguage,
            Html = page.Html,
            Link = page.Link,
            CreatedAt = page.CreatedAt,
            JsonContent = JsonSerializer.Serialize(page.Content),
        };
    }

    public PublicCambridgeEntry CreatePublic()
    {
        return new PublicCambridgeEntry()
        {
            Word = Word,
            Language = Language,
            TranslationLanguage = TranslationLanguage,
            Link = Link,
            Content = JsonSerializer.Deserialize<CambridgeContent>(JsonContent) ?? new CambridgeContent()
            {
                Title = Word,
                Blocks = new List<CambridgeWordBlock>(),
            }
        };
    }
    
    public static PublicCambridgeEntry CreatePublic(IParsedPageEntry page)
    {
        return new PublicCambridgeEntry()
        {
            Word = page.Word,
            Language = page.Language,
            TranslationLanguage = page.TranslationLanguage,
            Link = page.Link,
            Content = JsonSerializer.Deserialize<CambridgeContent>(page.JsonContent) ?? new CambridgeContent()
            {
                Title = page.Word,
                Blocks = new List<CambridgeWordBlock>(),
            }
        };
    }
}

public record ReversoContextEntry : ParsedPageEntry
{
    public static ReversoContextEntry Create(ReversoContextPage page)
    {
        return new ReversoContextEntry()
        {
            Word = page.Word,
            Language = page.Language,
            TranslationLanguage = page.TranslationLanguage,
            Html = page.Html,
            Link = page.Link,
            CreatedAt = page.CreatedAt,
            JsonContent = JsonSerializer.Serialize(page.Content),
        };
    }

    public PublicReversoContextEntry CreatePublic()
    {
        return new PublicReversoContextEntry()
        {
            Word = Word,
            Language = Language,
            TranslationLanguage = TranslationLanguage,
            Link = Link,
            Content = JsonSerializer.Deserialize<ReversoContextContent>(JsonContent) ?? new ReversoContextContent()
            {
                Title = Word,
                CategoryGroups = new List<ReversoContextCetegoryGroup>(),
                Examples = new List<ReversoContextExample>(),
            }
        };
    }
    
    public static PublicReversoContextEntry CreatePublic(IParsedPageEntry page)
    {
        return new PublicReversoContextEntry()
        {
            Word = page.Word,
            Language = page.Language,
            TranslationLanguage = page.TranslationLanguage,
            Link = page.Link,
            Content = JsonSerializer.Deserialize<ReversoContextContent>(page.JsonContent) ?? new ReversoContextContent()
            {
                Title = page.Word,
                CategoryGroups = new List<ReversoContextCetegoryGroup>(),
                Examples = new List<ReversoContextExample>(),
            }
        };
    }
}