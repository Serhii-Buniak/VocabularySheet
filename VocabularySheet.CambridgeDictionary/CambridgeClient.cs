using Microsoft.Extensions.Logging;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Parsing.Common;

namespace VocabularySheet.CambridgeDictionary;

public class CambridgeClient : WebPageClient
{
    public const string Base = "https://dictionary.cambridge.org";
    private const string BaseDictionary = $"{Base}/dictionary";
    
    public CambridgeClient(HttpClient httpClient, ILogger<CambridgeClient> logger) : base(httpClient, logger)
    {
    }

    public override async Task<WebPageResponse?> Page(string word, WordLanguage language, CancellationToken cancellationToken)
    {
        try
        {
            string link = $"{BaseLang(language)}/{word}";

            Logger.LogInformation("Full link: {link}", link);

            HttpResponseMessage httpResponse = await HttpClient.GetAsync(link, cancellationToken);
        
            if (!httpResponse.IsSuccessStatusCode)
            {
                return null;
            }
              
            var html = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        
            return new WebPageResponse()
            {
                Word = word,
                Language = language,
                Html = html,
                Link = link,
            };

        }
        catch (Exception)
        {
            return null;
        }
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
