using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Domain.Extensions;

namespace VocabularySheet.Maui.Common.Services;

public class TextToSpeechService
{
    public TextToSpeechService()
    {
        
    }
    
    public async Task<LocaleAndText?> GetLocaleAndTextForTextAsync(string text, WordLanguage lang)
    {
        string localeLang = lang switch
        {
            WordLanguage.Ua => "uk",
            WordLanguage.Ru => "ru",
            _ => "en"
        };
        
        var locales = (await TextToSpeech.GetLocalesAsync()).ToList();
 
        Locale? locale = locales.Where(l => l.Language.Contains(localeLang)).Random();
        locale ??= locales.Where(l => l.Language.Contains("en")).Random();
        locale ??= locales.Random();

        if (locale == null)
        {
            return null;
        }
        
        return new LocaleAndText(text, locale);
    }
}

public record LocaleAndText(string Text, Locale Locale);