namespace VocabularySheet.Infrastructure.Data.Csv.Models;

public record WordCsv
{
    public required string Original { get; set; }
    public required string Translation { get; set; }
    public string? Description { get; set; }
    public string? Link1 { get; set; }
    public string? Link2 { get; set; }
    public string? Link3 { get; set; }
}
