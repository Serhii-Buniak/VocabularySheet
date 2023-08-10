namespace VocabularySheet.Infrastructure.HttpClients.Interfaces;

public interface IGoogleSheetClient
{
    Task<Stream> GetCsvFileAsync(string url, CancellationToken cancellationToken);
}