using System.Net;
using Tools.Http.Exceptions;

namespace Infrastructure.Data.HttpClients;

public class StreamFetcherClient
{
    private readonly HttpClient _httpClient;

    public StreamFetcherClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Stream> Fetch(string url, CancellationToken cancellationToken)
    {
        HttpResponseMessage httpResponse = await _httpClient.GetAsync(url, cancellationToken);


        if (!httpResponse.IsSuccessStatusCode)
        {
            throw httpResponse.StatusCode switch
            {
                HttpStatusCode.InternalServerError => new HttpClientInternetServerException(),
                HttpStatusCode.NotFound => new HttpClientNotFoundException(),
                _ => new HttpClientException(),
            };

        }
        
        return await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
    }
}
