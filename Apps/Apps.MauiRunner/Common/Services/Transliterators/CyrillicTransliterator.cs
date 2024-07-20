using Application.Common.Commons.Dtos;
using Domain.Localization;
using NickBuhro.Translit;

namespace Apps.MauiRunner.Common.Services.Transliterators;

internal class CyrillicTransliterator
{
    private static readonly Dictionary<WordLanguage, TransliteratorBase> Transliterator = new()
    {
        [WordLanguage.Ua] = new UkrainianTransliterator(),
        [WordLanguage.Ru] = new RussianTransliterator(),
    };
    
    public string Transliterate(WordWithLanguage word)
    {
        return Transliterator[word.Language].Transliterate(word.Word).Replace("`", string.Empty);
    }
}

internal abstract class TransliteratorBase
{
    protected abstract Language Language { get; }

    public string Transliterate(string text)
    {
        return Transliteration.CyrillicToLatin(text, Language);
    }
}

internal class UkrainianTransliterator : TransliteratorBase
{
    protected override Language Language => Language.Ukrainian;
}

internal class RussianTransliterator : TransliteratorBase
{
    protected override Language Language => Language.Russian;
}