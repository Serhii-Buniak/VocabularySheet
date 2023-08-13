using NickBuhro.Translit;
using System.Globalization;
using System.Text.RegularExpressions;
using VocabularySheet.Domain.Extensions;

namespace VocabularySheet.Maui.Services;

public partial class TextToSpeechService : ITextToSpeechService
{
    public static CultureInfo UserCultureInfo => CultureInfo.CurrentCulture;

    public async Task<LocaleAndText> GetLocaleAndTextForTextAsync(string text)
    {
        var locales = await TextToSpeech.GetLocalesAsync();

        return new LocaleAndText(text, locales.Random()!);
    }

    [GeneratedRegex("\\p{IsCyrillic}")]
    private static partial Regex IsCyrillicRegex();
}