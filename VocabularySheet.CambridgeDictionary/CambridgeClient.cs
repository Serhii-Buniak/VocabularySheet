using Microsoft.Extensions.Logging;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.CambridgeDictionary;

public class CambridgeClient
{
    public const string Base = "https://dictionary.cambridge.org";
    private const string BaseDictionary = $"{Base}/dictionary";

    private readonly HttpClient _httpClient;
    private readonly ILogger<CambridgeClient> _logger;

    public CambridgeClient(HttpClient httpClient, ILogger<CambridgeClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string> Page(string word, WordLanguage language, CancellationToken cancellationToken)
    {
        string link = $"{BaseLang(language)}/{word}";

        _logger.LogInformation("Full link: {link}", link);

        HttpResponseMessage httpResponse = await _httpClient.GetAsync(link, cancellationToken);
        return await httpResponse.Content.ReadAsStringAsync(cancellationToken);
    }

    private static string BaseLang(WordLanguage language)
    {
        string lang = language switch
        {
            WordLanguage.Ua => "english-ukrainian",
            WordLanguage.Ru => "english-russian",
            _ => "english",
        };

        return $"{BaseDictionary}/{lang}";
    }
}
