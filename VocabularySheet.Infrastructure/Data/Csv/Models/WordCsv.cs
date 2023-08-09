namespace VocabularySheet.Infrastructure.Data.Csv.Models;

public class WordCsv
{
    public string Original { get; set; } = null!;
    public string Translation { get; set; } = null!;
    public string? Description { get; set; }
    public string? Link1 { get; set; }
    public string? Link2 { get; set; }
    public string? Link3 { get; set; }
}
