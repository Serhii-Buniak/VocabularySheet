using VocabularySheet.Common;

namespace WebSources.Common;

public interface IParsedPage<TContent> where TContent : class
{
    string Word { get; init; }
    WordLanguage Language { get; init; }
    WordLanguage TranslationLanguage { get; init; }
    string Html { get; init; }
    string Link { get; init; }
    DateTime CreatedAt { get; init; }
    TContent Content { get; init; }
}

public interface IHaveNullImageUrl
{
    string? ImageUrl { get; }

    string? FullImageLink();
}

public interface IHaveAudioLink
{
    string Type { get; }
    string Src { get; }

    string FullLink();
}