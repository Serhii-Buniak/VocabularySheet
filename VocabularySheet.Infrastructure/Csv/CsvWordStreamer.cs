using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;
using VocabularySheet.Infrastructure.Csv.Interfaces;
using VocabularySheet.Infrastructure.Csv.Models;

namespace VocabularySheet.Infrastructure.Csv;

public class CsvWordStreamer : ICsvWordStreamer
{
    private readonly ICsvStreamer<WordCsv, WordCsvClassMap> _csvStreamer;
    private readonly IMapperService _mapper;

    public CsvWordStreamer(ICsvStreamer<WordCsv, WordCsvClassMap> csvStreamer, IMapperService mapper)
    {
        _csvStreamer = csvStreamer;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Word>> ReadAsync(Stream stream, CancellationToken cancellationToken)
    {
        IEnumerable<WordCsv> words = await _csvStreamer.ReadAsync(stream, cancellationToken);
        return _mapper.Map<IEnumerable<Word>>(words);
    }
}
