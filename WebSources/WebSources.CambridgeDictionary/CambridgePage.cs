using VocabularySheet.Common;
using WebSources.CambridgeDictionary.Entities;
using WebSources.Common;

namespace WebSources.CambridgeDictionary;

public record CambridgePage : IParsedPage<CambridgeContent>
{
    public required string Word { get; init; }
    public required WordLanguage Language { get; init; }
    public required WordLanguage TranslationLanguage { get; init; }
    public required string Html { get; init; }
    public required string Link { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required CambridgeContent Content { get; init; }
}