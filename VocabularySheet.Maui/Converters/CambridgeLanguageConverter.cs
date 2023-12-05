using System.Globalization;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Maui.Converters;

public class CambridgeLanguageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is WordLanguage wordLanguage)
            return wordLanguage switch
            {
                WordLanguage.En => "English",
                WordLanguage.Ua => "English-Ukrainian",
                WordLanguage.Ru => "English-404?",
                _ => ""
            };

        return Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Convert(value, targetType, parameter, culture);
    }
}