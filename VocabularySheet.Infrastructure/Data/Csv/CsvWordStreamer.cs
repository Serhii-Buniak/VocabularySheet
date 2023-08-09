using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;
using VocabularySheet.Infrastructure.Data.Csv.Interfaces;
using VocabularySheet.Infrastructure.Data.Csv.Models;

namespace VocabularySheet.Infrastructure.Data.Csv;

public partial class CsvWordStreamer : ICsvWordStreamer
{
    private readonly ICsvStreamer<WordCsv, WordCsvClassMap> _csvStreamer;
    private readonly IMapperService _mapper;

    public CsvWordStreamer(ICsvStreamer<WordCsv, WordCsvClassMap> csvStreamer, IMapperService mapper)
    {
        _csvStreamer = csvStreamer;
        _mapper = mapper;
    }

    public IEnumerable<Word> Read(Stream stream)
    {
        IEnumerable<WordCsv> words = _csvStreamer.Read(stream);
        return _mapper.Map<IEnumerable<Word>>(words);
    }
}
