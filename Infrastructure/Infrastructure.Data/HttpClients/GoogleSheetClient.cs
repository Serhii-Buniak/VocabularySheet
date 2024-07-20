using System.Net;
using System.Text.RegularExpressions;
using Infrastructure.Data.HttpClients.Interfaces;
using Tools.Http.Exceptions;

namespace Infrastructure.Data.HttpClients;

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

        string fileUrl = string.Format(@"https://docs.google.com/spreadsheets/u/0/d/{0}/export?format=xlsx&id={0}", id);

        HttpResponseMessage httpResponse = await _httpClient.GetAsync(fileUrl, cancellationToken);


        if (!httpResponse.IsSuccessStatusCode)
        {
            throw httpResponse.StatusCode switch
            {
                HttpStatusCode.InternalServerError => new HttpClientInternetServerException(),
                HttpStatusCode.NotFound => new HttpClientNotFoundException(),
                _ => new HttpClientException(),
            };

        }

        if (httpResponse.RequestMessage?.RequestUri?.Authority == "accounts.google.com")
        {
            throw new GoogleSheetNotPublicException();
        }


        return await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
    }

    public async Task RunScriptAsync(string url, CancellationToken cancellationToken)
    {
        HttpResponseMessage httpResponse = await _httpClient.GetAsync(url, cancellationToken);

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new HttpClientException("Script is not valid.");
        }
    }
}

public class GoogleSheetNotPublicException : HttpClientException
{
    public GoogleSheetNotPublicException() : base("Google sheet is not public.")
    {
    }
}
