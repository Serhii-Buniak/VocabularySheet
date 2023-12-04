using System.Text.Json;
using VocabularySheet.CambridgeDictionary;
using VocabularySheet.CambridgeDictionary.Entities;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Domain.Pages;

namespace VocabularySheet.Infrastructure.Repositories.Pages;

public interface IParsedPageEntry
{
    string Word { get; }
    WordLanguage Language { get; }
    string Link { get; init; }
    string JsonContent { get; }
}

public abstract record ParsedPageEntry : IParsedPageEntry
{
    public required string Word { get; init; }
    public required WordLanguage Language { get; init; }
    public required string Html { get; init; }
    public required string Link { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string JsonContent { get; init; }
}

public record CambridgeEntry : ParsedPageEntry
{
    public static CambridgeEntry Create(CambridgePage page)
    {
        return new CambridgeEntry()
        {
            Word = page.Word,
            Language = page.Language,
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
            Link = page.Link,
            Content = JsonSerializer.Deserialize<CambridgeContent>(page.JsonContent) ?? new CambridgeContent()
            {
                Title = page.Word,
                Blocks = new List<CambridgeWordBlock>(),
            }
        };
    }
}