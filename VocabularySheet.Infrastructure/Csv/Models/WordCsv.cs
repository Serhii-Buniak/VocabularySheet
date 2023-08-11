namespace VocabularySheet.Infrastructure.Csv.Models;

public record WordCsv
{
    public required string Original { get; set; }
    public required string Translation { get; set; }
    public string? Description { get; set; }
    public string? Sources { get; set; }

}
