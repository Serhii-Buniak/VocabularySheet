using System.Web;
using Domain.Localization;
using Microsoft.Extensions.Logging;
using WebSources.Common;

namespace WebSources.CambridgeDictionary;

public class CambridgeClient : WebPageClient
{
    public const string Base = "https://dictionary.cambridge.org";
    private const string BaseDictionary = $"{Base}/dictionary";

    public CambridgeClient(HttpClient httpClient, ILogger<CambridgeClient> logger) : base(httpClient, logger)
    {
    }

    public override async Task<WebPageResponse?> Page(string word, WordLanguage language, WordLanguage translationLanguage,
        CancellationToken cancellationToken)
    {
        var link = WordLink(word, language);

        var html = await Fetch(link, cancellationToken);
        if (html == null)
        {
            return null;
        }

        return new WebPageResponse()
        {
            Word = word,
            Language = language,
            TranslationLanguage = translationLanguage,
            Html = html,
            Link = link,
        };
    }

    public static string WordLink(string word, WordLanguage language)
    {
        word = word
            .Replace("/", "-")
            .Replace(" ", "-");

        word = HttpUtility.UrlEncode(word);
        string link = $"{BaseLang(language)}/{word}";
        return link;
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