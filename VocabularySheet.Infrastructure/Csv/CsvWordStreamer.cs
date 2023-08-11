using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;
using VocabularySheet.Infrastructure.Commons.Mappings;
using VocabularySheet.Infrastructure.Csv.Interfaces;
using VocabularySheet.Infrastructure.Csv.Models;

namespace VocabularySheet.Infrastructure.Csv;

public class CsvWordStreamer : ICsvWordStreamer
{
    private readonly ICsvStreamer<WordCsv, WordCsvClassMap> _csvStreamer;

    public CsvWordStreamer(ICsvStreamer<WordCsv, WordCsvClassMap> csvStreamer)
    {
        _csvStreamer = csvStreamer;
    }

    public async Task<IEnumerable<Word>> ReadAsync(Stream stream, CancellationToken cancellationToken)
    {
        IEnumerable<WordCsv> words = await _csvStreamer.ReadAsync(stream, cancellationToken);

        return words.ToWordsCsv();
    }
}
