using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Linq;
using VocabularySheet.Domain.Exceptions;
using VocabularySheet.Infrastructure.Csv.Interfaces;

namespace VocabularySheet.Infrastructure.Csv;

public class CsvStreamer<TRecord, CMap> : ICsvStreamer<TRecord, CMap> where CMap : ClassMap<TRecord>
{
    public async Task<IEnumerable<TRecord>> ReadAsync(Stream stream, CancellationToken cancellationToken)
    {
        using StreamReader reader = new(stream, leaveOpen: true);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };

        using CsvReader csv = new(reader, config, leaveOpen: true);
        
        if (stream.Length == 0)
        {
            throw new HeaderValidationEmptyException(csv.Context);
        }

        csv.Context.RegisterClassMap<CMap>();

        return await csv.GetRecordsAsync<TRecord>(cancellationToken).ToListAsync(cancellationToken);
    }
}
