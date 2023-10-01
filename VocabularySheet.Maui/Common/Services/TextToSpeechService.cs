using System.Globalization;
using VocabularySheet.Domain.Extensions;

namespace VocabularySheet.Maui.Common.Services;

public class TextToSpeechService
{
    public async Task<LocaleAndText> GetLocaleAndTextForTextAsync(string text)
    {
        var locales = await TextToSpeech.GetLocalesAsync();

        return new LocaleAndText(text, locales.Random()!);
    }
}

public struct LocaleAndText
{
    public LocaleAndText(string text, Locale locale)
    {
        Text = text;
        Locale = locale;
    }

    public string Text { get; set; }
    public Locale Locale { get; set; }
}