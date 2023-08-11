using VocabularySheet.Domain;
using VocabularySheet.Infrastructure.Csv.Models;

namespace VocabularySheet.Infrastructure.Commons.Mappings;

public static class WordMapping
{
    public static Word ToWordCsv(this WordCsv wordCsv) => new()
    {
        Original = wordCsv.Original,
        Translation = wordCsv.Translation,
        Description = wordCsv.Description,
    };

    public static IEnumerable<Word> ToWordsCsv(this IEnumerable<WordCsv> wordsCsv) => wordsCsv
        .Select(wordCsv => wordCsv.ToWordCsv());
}
