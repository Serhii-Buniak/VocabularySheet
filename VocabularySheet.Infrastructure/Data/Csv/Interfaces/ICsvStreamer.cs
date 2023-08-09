using CsvHelper.Configuration;

namespace VocabularySheet.Infrastructure.Data.Csv.Interfaces;

public interface ICsvStreamer<TRecord, CMap> where CMap : ClassMap<TRecord>
{
    IEnumerable<TRecord> Read(Stream stream);
    Task WriteRecordsAsync(Stream stream, IEnumerable<TRecord> records);
}
