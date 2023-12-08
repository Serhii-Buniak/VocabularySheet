using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Parsing.Common;

public interface IParsedPage<TContent> where TContent : class
{
    string Word { get; init; }
    WordLanguage Language { get; init; }
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