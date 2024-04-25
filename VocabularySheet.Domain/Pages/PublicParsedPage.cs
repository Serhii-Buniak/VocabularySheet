using VocabularySheet.Common;
using WebSources.CambridgeDictionary.Entities;
using WebSources.ReversoContext.Entities;

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

public record PublicReversoContextEntry : PublicParsedPage<ReversoContextContent>
{

}