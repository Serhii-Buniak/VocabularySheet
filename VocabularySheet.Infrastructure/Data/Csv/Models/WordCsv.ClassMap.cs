using CsvHelper.Configuration;

namespace VocabularySheet.Infrastructure.Data.Csv.Models;

public class WordCsvClassMap : ClassMap<WordCsv>
{
    public WordCsvClassMap()
    {
        Map(p => p.Original).Name("Original");
        Map(p => p.Translation).Name("Translation");
        Map(p => p.Description).Name("Description");
    }
}