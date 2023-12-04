using VocabularySheet.CambridgeDictionary.Entities;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Domain.Pages;
using VocabularySheet.Parsing.Common;

namespace VocabularySheet.CambridgeDictionary;

public record CambridgePage : IParsedPage<CambridgeContent>
{
    public required string Word { get; init; }
    public required WordLanguage Language { get; init; }
    public required string Html { get; init; }
    public required string Link { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required CambridgeContent Content { get; init; }
}