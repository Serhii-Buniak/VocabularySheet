using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Application.Commons.Dtos;

public record WordModel : IWord
{
    public static WordModel Sample => new()
    {
        Id = 0,
        Index = 0,
        Original = "word",
        Translation = "word translation",
        OrignalLanguage = WordLanguage.En,
        TranslationlLanguage = WordLanguage.En,
        Description = "a single unit of language that has meaning and can be spoken or written:\r\n- Your essay should be no more than two thousand words long.\r\n- Some words are more difficult to spell than others.\r\n- What's the word for bikini in French?\r\n- It's sometimes difficult to find exactly the right word to express what you want to say.",
    };

    public required long Id { get; init; }
    public required int Index { get; init; }

    public required string Original { get; init; }
    public required WordLanguage OrignalLanguage { get; init; }

    public required string Translation { get; init; }
    public required WordLanguage TranslationlLanguage { get; init; }

    public string? Description { get; init; }

    public WordModel Self => this;
}