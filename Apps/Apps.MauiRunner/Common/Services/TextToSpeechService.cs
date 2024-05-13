using Domain.Localization;
using Tools.Common.Extensions;
using VocabularySheet.ML.Client;

namespace Apps.MauiRunner.Common.Services;

public class TextToSpeechService
{
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


public class MlModelsFolder : IMlModelsFolder
{
    public string CreatePath(string path) => Path.Combine("MlModels", path);
    
    public Task<Stream> GetModel(string path)
    {
        path = CreatePath(path);
        return FileSystem.OpenAppPackageFileAsync(path);
    }
}
