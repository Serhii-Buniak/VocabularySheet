using CsvHelper.Configuration;

namespace VocabularySheet.Infrastructure.Csv.Interfaces;

public interface ICsvStreamer<TRecord, CMap> where CMap : ClassMap<TRecord>
{
    Task<IEnumerable<TRecord>> ReadAsync(Stream stream, CancellationToken cancellationToken);
}
