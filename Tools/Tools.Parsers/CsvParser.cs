using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Tools.Parsers;

public sealed class AutoClassMap<T> : ClassMap<T>
{
    public static readonly AutoClassMap<T> Default = new();
    
    private AutoClassMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
    }
}

public interface ICsvFile
{
    static abstract ClassMap CsvParser { get; }
}

public interface ICsvAutoFile<T> : ICsvFile
{
    static ClassMap ICsvFile.CsvParser => AutoClassMap<T>.Default;
}

public class HeaderValidationEmptyException(CsvContext context)
    : HeaderValidationException(context, Array.Empty<InvalidHeader>(), "CsvStreamers file is empty");

public static class CsvParser
{
    public static readonly CsvConfiguration Simple = new(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = false,
    };
    public static readonly CsvConfiguration Header = new(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
    };
    
    public static readonly CsvConfiguration HeaderTab = new(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        Delimiter = "\t"
    };
    
    public static string Serialize<T>(this CsvConfiguration configuration, IEnumerable<T> data) where T : ICsvFile
    {
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, configuration);
        csv.Context.RegisterClassMap(T.CsvParser);
        
        csv.WriteRecords(data);
        
        return writer.ToString();
    }

    public static IEnumerable<T> Deserialize<T>(this CsvConfiguration configuration, string csvText) where T : ICsvFile
    {
        using var reader = new StringReader(csvText);
        using var csv = new CsvReader(reader, configuration);
        if (csvText.Length == 0)
        {
            throw new HeaderValidationEmptyException(csv.Context);
        }

        csv.Context.RegisterClassMap(T.CsvParser);

        foreach (var csvRecord in csv.GetRecords<T>())
        {
            yield return csvRecord;
        }
    }
    
    public static async IAsyncEnumerable<T> DeserializeAsync<T>(this CsvConfiguration configuration, Stream stream, CancellationToken cancellationToken)  where T : ICsvFile
    {
        using StreamReader reader = new(stream, leaveOpen: true);
        using CsvReader csv = new(reader, configuration, leaveOpen: true);
        
        if (stream.Length == 0)
        {
            throw new HeaderValidationEmptyException(csv.Context);
        }
        csv.Context.RegisterClassMap(T.CsvParser);


        await foreach (var csvRecord in csv.GetRecordsAsync<T>(cancellationToken))
        {
            yield return csvRecord;
        }
    }

}