﻿using System.Web;
using Domain.Localization;
using WebSources.Common;

namespace WebSources.Linker;

public static class GoogleTranslatorLinker
{
    private const string Base = "https://translate.google.com";
    
    private static string WordLink(string word, WordLanguage language, WordLanguage translationLanguage)
    {
        word = HttpUtility.UrlEncode(word);
        string link = $"{Base}/?sl={LangToString(language)}&tl={LangToString(translationLanguage)}&text={word}&op=translate";
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