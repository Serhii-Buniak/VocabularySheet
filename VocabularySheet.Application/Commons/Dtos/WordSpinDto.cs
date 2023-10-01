namespace VocabularySheet.Application.Commons.Dtos;

public record WordSpinDto
{
    public static WordSpinDto Sample => new()
    {
        Index = 0,
        Original = "Word",
        Translation = "word translation",
        Description = "a single unit of language that has meaning and can be spoken or written:\r\n- Your essay should be no more than two thousand words long.\r\n- Some words are more difficult to spell than others.\r\n- What's the word for bikini in French?\r\n- It's sometimes difficult to find exactly the right word to express what you want to say.",
    };

    public required int Index { get; set; }

    public required string Original { get; set; }

    public required string Translation { get; set; }

    public string? Description { get; set; }

}