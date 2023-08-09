namespace VocabularySheet.Application.Commons.Interfaces;

public interface IGoogleSheetClient
{
    Task<Stream> GetCsvFileAsync(string name);
}
