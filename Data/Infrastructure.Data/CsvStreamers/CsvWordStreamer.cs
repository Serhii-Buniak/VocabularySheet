using Domain.WordModels;
using Infrastructure.Data.Commons.Mappings;
using Infrastructure.Data.CsvStreamers.Models;
using Tools.Parsers;

namespace Infrastructure.Data.CsvStreamers;

internal class CsvWordStreamer
{
    public async Task<IEnumerable<Word>> ReadAsync(Stream stream, CancellationToken cancellationToken)
    {
        IEnumerable<WordCsv> words = await CsvParser.Header
            .DeserializeAsync<WordCsv>(stream, cancellationToken)
            .ToListAsync(cancellationToken);

        return words.ToWordsCsv();
    }
}
