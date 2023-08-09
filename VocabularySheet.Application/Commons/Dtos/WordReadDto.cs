namespace VocabularySheet.Application.Commons.Dtos;

internal class WordReadDto
{
    public long Index { get; set; }

    public string Original { get; set; } = null!;

    public string Translation { get; set; } = null!;

    public string? Description { get; set; }
}
