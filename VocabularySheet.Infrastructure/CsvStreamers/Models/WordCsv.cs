using VocabularySheet.Common.Parsers;

namespace VocabularySheet.Infrastructure.CsvStreamers.Models;

public record WordCsv : ICsvAutoFile<WordCsv>
{
    public required string Original { get; set; }
    public required string Translation { get; set; }
    public string? Description { get; set; }
    public string? Sources { get; set; }
    public string? Category { get; set; }
}
