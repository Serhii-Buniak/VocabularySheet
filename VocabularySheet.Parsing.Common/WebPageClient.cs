using Microsoft.Extensions.Logging;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Parsing.Common;

public abstract class WebPageClient
{
    protected HttpClient HttpClient { get; }
    protected ILogger Logger { get; }

    protected WebPageClient(HttpClient httpClient, ILogger logger)
    {
        HttpClient = httpClient;
        Logger = logger;
    }

    public abstract Task<WebPageResponse?> Page(string word, WordLanguage language, CancellationToken cancellationToken);
}