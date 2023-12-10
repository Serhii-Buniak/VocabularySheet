using System.Text.Json;
using VocabularySheet.CambridgeDictionary.Entities;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Domain.Pages;

public abstract record PublicParsedPage<TContent> where TContent : class
{
    public required string Word { get; init; }
    public required WordLanguage Language { get; init; }
    public required WordLanguage TraslationLanguage { get; init; }
    public required string Link { get; init; }
    public required TContent Content { get; init; }
}

public record PublicCambridgeEntry : PublicParsedPage<CambridgeContent>
{

}