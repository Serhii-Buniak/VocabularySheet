using CsvHelper.Configuration;

namespace VocabularySheet.Infrastructure.Csv.Models;

public class WordCsvClassMap : ClassMap<WordCsv>
{
    public WordCsvClassMap()
    {
        Map(p => p.Original).Name("Original");
        Map(p => p.Translation).Name("Translation");
        Map(p => p.Description).Name("Description");
        Map(p => p.Link1).Name("Link1");
        Map(p => p.Link2).Name("Link2");
        Map(p => p.Link3).Name("Link3");
    }
}