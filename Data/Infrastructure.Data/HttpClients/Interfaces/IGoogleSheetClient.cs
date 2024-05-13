namespace Infrastructure.Data.HttpClients.Interfaces;

public interface IGoogleSheetClient
{
    Task<Stream> GetCsvFileAsync(string url, CancellationToken cancellationToken);
    Task RunScriptAsync(string url, CancellationToken cancellationToken);
}