using VocabularySheet.Common;

namespace WebSources.Common;

public record ExternalSourceLink
{
    public required string Word { get; init; }
    public required WordLanguage Language { get; init; }
    public required WordLanguage TranslationLanguage { get; init; }
    public required string Link { get; init; }
}