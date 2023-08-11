using CsvHelper.Configuration;
using System.Globalization;

namespace VocabularySheet.Infrastructure.Csv.Models;

public class WordCsvClassMap : ClassMap<WordCsv>
{
    public WordCsvClassMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
    }
}