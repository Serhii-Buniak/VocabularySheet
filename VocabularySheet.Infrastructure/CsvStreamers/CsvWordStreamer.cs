using VocabularySheet.Domain;
using VocabularySheet.Common;
using VocabularySheet.Common.Parsers;
using VocabularySheet.Infrastructure.CsvStreamers.Models;

namespace VocabularySheet.Infrastructure.CsvStreamers;

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
