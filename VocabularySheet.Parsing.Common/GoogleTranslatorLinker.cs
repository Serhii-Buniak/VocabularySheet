using System.Web;
using VocabularySheet.Common;
using VocabularySheet.Domain;

namespace VocabularySheet.Parsing.Common;

public static class GoogleTranslatorLinker
{
    private const string Base = "https://translate.google.com";
    
    private static string WordLink(string word, WordLanguage language, WordLanguage translationLanguage)
    {
        word = HttpUtility.UrlEncode(word);
        string link = $"{Base}/?sl={LangToString(language)}&tl={LangToString(translationLanguage)}&text={word}&op=translate";
        return link;
    }

    public static GoogleTranslatorLink Link(string word, WordLanguage language, WordLanguage translationLanguage)
    {
        return new GoogleTranslatorLink()
        {
            Word = word,
            Language = language,
            TranslationLanguage = translationLanguage,
            Link = WordLink(word, language, translationLanguage),
        };
    }
    
    private static string LangToString(WordLanguage language)
    {
        return language switch
        {
            WordLanguage.Ua => "uk",
            WordLanguage.Ru => "ru",
            _ => "en",
        };
    }
}

public record GoogleTranslatorLink
{
    public required string Word { get; init; }
    public required WordLanguage Language { get; init; }
    public required WordLanguage TranslationLanguage { get; init; }
    public required string Link { get; init; }
}