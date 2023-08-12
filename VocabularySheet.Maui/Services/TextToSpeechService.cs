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

        if (IsCyrillicRegex().Matches(text).Any())
        {
            bool userUA = UserCultureInfo.Name.Contains("ua", StringComparison.InvariantCultureIgnoreCase);

            if (userUA)
            {
                var uaLocales = locales.Where(l => l.Language.Contains("ua", StringComparison.InvariantCultureIgnoreCase));

                if (uaLocales.Any())
                {
                    return new LocaleAndText(text, uaLocales.Random()!);
                }

                text.Replace("и", "ы");
                text.Replace("і", "и");
                text.Replace("е", "э");
                text.Replace("є", "е");
            }


            var ruLocales = locales.Where(l => l.Language.Contains("ru", StringComparison.InvariantCultureIgnoreCase));

            if (ruLocales.Any())
            {
                return new LocaleAndText(text, ruLocales.Random()!);
            }


            text = Transliteration.CyrillicToLatin(text, Language.Unknown);
        }

        var enLocales = locales.Where(l => l.Language.Contains("en", StringComparison.InvariantCultureIgnoreCase));

        return new LocaleAndText(text, enLocales.Random()!);
    }

    [GeneratedRegex("\\p{IsCyrillic}")]
    private static partial Regex IsCyrillicRegex();
}