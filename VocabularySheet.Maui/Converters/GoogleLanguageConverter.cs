using System.Globalization;
using VocabularySheet.Common;
using WebSources.Common;

namespace VocabularySheet.Maui.Converters;

public class GoogleLanguageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is GoogleTranslatorLink link)
        {
            return $"{ToText(link.Language)} - {ToText(link.TranslationLanguage)}";
        }

        return Binding.DoNothing;
    }

    private string ToText(WordLanguage language)
    {
        return language switch
        {
            WordLanguage.Ua => "Українська",
            WordLanguage.Ru => "404?",
            _ => "English"
        };
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Convert(value, targetType, parameter, culture);
    }
}