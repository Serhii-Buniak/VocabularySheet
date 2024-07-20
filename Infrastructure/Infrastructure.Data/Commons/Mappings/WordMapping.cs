using Domain.WordModels;
using Infrastructure.Data.CsvStreamers.Models;

namespace Infrastructure.Data.Commons.Mappings;

public static class WordMapping
{
    public static Word ToWordCsv(this WordCsv wordCsv, int rowNumber) => new Word()
    {
        Original = wordCsv.Original,
        Translation = wordCsv.Translation,
        Description = wordCsv.Description,
        Category = wordCsv.Category?.MapToCategoryEnum(),
        RowNumber = rowNumber
    };

    public static IEnumerable<Word> ToWordsCsv(this IEnumerable<WordCsv> wordsCsv) => wordsCsv
        .Select((wordCsv, index) => wordCsv.ToWordCsv(index + 1));
}
