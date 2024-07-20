using System.Text.Json;
using OfficeOpenXml;

namespace Tools.Parsers;

public interface IXlsxSerializable<T>
{
    static abstract T ReadXlsxRow(Func<int, ExcelRange> range);
    static abstract IEnumerable<T> FilterXlsxSheet(IEnumerable<T> sheet);
}

public record XlsxConfiguration
{
    public required int RowStart { get; init; }
}

public static class XlsxExtensions
{
    static XlsxExtensions()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }
    
    public static readonly XlsxConfiguration SkipHeader = new XlsxConfiguration
    {
        RowStart = 2
    };
    
    public static IEnumerable<T> DeserializeFile<T>(this XlsxConfiguration configuration, string path, string? sheetName = null) where T : IXlsxSerializable<T>
    {
        using var package = new ExcelPackage(new FileInfo(path));
        return configuration.DeserializePackage<T>(package, sheetName);
    }
    
    public static IEnumerable<T> Deserialize<T>(this XlsxConfiguration configuration, Stream stream, string? sheetName = null) where T : IXlsxSerializable<T>
    {
        using var package = new ExcelPackage(stream);
        return configuration.DeserializePackage<T>(package, sheetName);
    }

    private static IEnumerable<T> DeserializePackage<T>(this XlsxConfiguration configuration, ExcelPackage package, string? sheetName = null) where T : IXlsxSerializable<T>
    {
        var entities = new List<T>();

        ExcelWorksheet? worksheet;
        if (!string.IsNullOrWhiteSpace(sheetName))
        {
            worksheet = package.Workbook.Worksheets[sheetName];
        }
        else
        {
            // Get first worksheet
            worksheet = package.Workbook.Worksheets[0];
        }
        if (worksheet == null)
        {
            throw new Exception($"Sheet {sheetName} not found"); 
        }

        int rowCount = worksheet.Dimension.Rows;

        for (int row = configuration.RowStart; row <= rowCount; row++) // start from row 2 to skip header
        {
            int cellsRow = row;
            var entity = T.ReadXlsxRow(column => worksheet.Cells[cellsRow, column]);
            entities.Add(entity);
        }

        return T.FilterXlsxSheet(entities);
    }
    
    public static string GetStringValue(this ExcelRange excelRange)
    {
        return GetStringOrNullValue(excelRange) ?? string.Empty;
    }    
    
    public static T? GetJson<T>(this ExcelRange excelRange) where T : class
    {
        string? json = GetStringOrNullValue(excelRange);
        if (json == null)
        {
            return null;
        }
        
        try
        {
            return JsonSerializer.Deserialize<T>(json);
        }
        catch
        {
            return null;
        }
    }        
    
    public static string? GetStringOrNullValue(this ExcelRange excelRange)
    {
        return excelRange.Value?.ToString();
    }    
    
    public static int GetInt32Value(this ExcelRange excelRange)
    {
        return int.Parse(GetStringNumber(excelRange));
    }
    
    public static int? GetInt32ValueOrNull(this ExcelRange excelRange)
    {
        string? number = GetStringOrNullValue(excelRange);
        if (number == null)
        {
            return null;
        }
        
        return int.Parse(number);
    }
    
    public static TEnum GetInt32Enum<TEnum>(this ExcelRange excelRange) where TEnum : Enum
    {
        return (TEnum)Enum.Parse(typeof(TEnum), GetStringNumber(excelRange));
    }

    private static string GetStringNumber(this ExcelRange excelRange)
    {
        return GetStringOrNullValue(excelRange) ?? "0";
    }

}