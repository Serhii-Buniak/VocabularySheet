using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using VocabularySheet.Infrastructure.Data.Csv.Interfaces;

namespace VocabularySheet.Infrastructure.Data.Csv;

public class CsvStreamer<TRecord, CMap> where CMap : ClassMap<TRecord>, ICsvStreamer<TRecord, CMap>
{
    public IEnumerable<TRecord> Read(Stream stream)
    {
        using StreamReader reader = new(stream, leaveOpen: true);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };

        using CsvReader csv = new(reader, config, leaveOpen: true);
        csv.Context.RegisterClassMap<CMap>();

        return csv.GetRecords<TRecord>().ToList();
    }
}
