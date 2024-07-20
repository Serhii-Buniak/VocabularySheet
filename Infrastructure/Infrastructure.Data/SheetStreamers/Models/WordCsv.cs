using OfficeOpenXml;
using Tools.Parsers;

namespace Infrastructure.Data.SheetStreamers.Models;

public record WordCsv : ICsvAutoFile<WordCsv>, IXlsxSerializable<WordCsv>
{
    public required string Original { get; set; }
    public required string Translation { get; set; }
    public string? Description { get; set; }
    public string? Sources { get; set; }
    public string? Category { get; set; }
    
    public static WordCsv ReadXlsxRow(Func<int, ExcelRange> range)
    {
        return new WordCsv
        {
            Original = range(1).GetStringValue(),
            Translation = range(2).GetStringValue(),
            Description = range(3).GetStringOrNullValue(),
            Sources = range(4).GetStringOrNullValue(),
            Category = range(5).GetStringOrNullValue(),
        };
    }

    public static IEnumerable<WordCsv> FilterXlsxSheet(IEnumerable<WordCsv> sheet)
    {
        return sheet.Where(s => !string.IsNullOrWhiteSpace(s.Original));
    }
}
