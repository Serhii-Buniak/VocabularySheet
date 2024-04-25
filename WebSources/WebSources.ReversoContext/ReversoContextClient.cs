using System.Web;
using Microsoft.Extensions.Logging;
using VocabularySheet.Common;
using WebSources.Common;

namespace WebSources.ReversoContext;

public class ReversoContextClient : WebPageClient
{
    public const string Base = "https://context.reverso.net/translation";
    
    public ReversoContextClient(HttpClient httpClient, ILogger<ReversoContextClient> logger) : base(httpClient, logger)
    {
    }
    
    public override async Task<WebPageResponse?> Page(string word, WordLanguage language,
        WordLanguage translationLanguage, CancellationToken cancellationToken)
    {
        var link = WordLink(word, language, translationLanguage);
        
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

    public static string WordLink(string word, WordLanguage language, WordLanguage translationLanguage)
    {
        word = HttpUtility.UrlEncode(word);
        string link = $"{BaseLang(language, translationLanguage)}/{word}";
        return link;
    }

    private static string BaseLang(WordLanguage language, WordLanguage translateLanguage)
    {
        return $"{Base}/{LangToString(language)}-{LangToString(translateLanguage)}";
    }

    public static string LangToString(WordLanguage language)
    {
        return language switch
        {
            WordLanguage.Ua => "ukrainian",
            WordLanguage.Ru => "russian",
            _ => "english",
        };
    }
}