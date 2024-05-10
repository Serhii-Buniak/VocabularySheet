using System.Web;
using VocabularySheet.Common;

namespace WebSources.Common;

public static class OxfordDictionaryTranslatorLinker
{
    private const string Base = "https://www.oxfordlearnersdictionaries.com/definition/english";
    
    private static string WordLink(string word, WordLanguage language, WordLanguage translationLanguage)
    {
        if (language != WordLanguage.En)
        {
            throw new ArgumentException("OxfordDictionary support only En", nameof(language));
        }
        
        word = HttpUtility.UrlEncode(word);
        string link = $"{Base}/{word}";
        return link;
    }

    public static ExternalSourceLink Link(string word, WordLanguage language, WordLanguage translationLanguage)
    {
        return new ExternalSourceLink()
        {
            Word = word,
            Language = language,
            TranslationLanguage = translationLanguage,
            Link = WordLink(word, language, translationLanguage),
        };
    }
}