using System.Net;
using System.Text.RegularExpressions;
using VocabularySheet.Domain.Exceptions.HttpClientExceptions;
using VocabularySheet.Infrastructure.HttpClients.Interfaces;

namespace VocabularySheet.Infrastructure.HttpClients;

public class GoogleSheetClient : IGoogleSheetClient
{
    private readonly HttpClient _httpClient;

    public GoogleSheetClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Stream> GetCsvFileAsync(string url, CancellationToken cancellationToken)
    {
        string pattern = @"\/d\/([a-zA-Z0-9_-]+)";

        Match match = Regex.Match(url, pattern);

        string id = match.Groups[1].Value;

        string fileUrl = string.Format(@"https://docs.google.com/spreadsheets/u/0/d/{0}/export?format=csv&id={0}&gid=0", id);

        HttpResponseMessage httpResponse = await _httpClient.GetAsync(fileUrl, cancellationToken);

        if (httpResponse.IsSuccessStatusCode)
        {
            return await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
        }

        throw httpResponse.StatusCode switch
        {
            HttpStatusCode.InternalServerError => new HttpClientInternetServerException(),
            HttpStatusCode.NotFound => new HttpClientNotFoundException(),
            _ => new HttpClientException(),
        };
    }

}
