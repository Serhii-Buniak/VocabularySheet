using Microsoft.Extensions.Logging;
using VocabularySheet.Common;

namespace WebSources.Common;

public abstract class WebPageClient
{
    protected HttpClient HttpClient { get; }
    protected ILogger Logger { get; }

    protected WebPageClient(HttpClient httpClient, ILogger logger)
    {
        HttpClient = httpClient;
        Logger = logger;
    }

    protected async Task<string?> Fetch(string link, CancellationToken cancellationToken)
    {
        try
        {
            Logger.LogInformation("Full link: {link}", link);
            HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 Edg/120.0.0.0");
            HttpResponseMessage httpResponse = await HttpClient.GetAsync(link, cancellationToken);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return null;
            }

            return await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public abstract Task<WebPageResponse?> Page(string word, WordLanguage language, WordLanguage translateLanguage,
        CancellationToken cancellationToken);
}