using System.Globalization;
using VocabularySheet.Domain.Pages;
using WebSources.ReversoContext;

namespace VocabularySheet.Maui.AppRunner.Converters;

public class ReversoContextLanguageConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return "";
        
        if (value is PublicReversoContextEntry entry)
            return $"{ReversoContextClient.LangToString(entry.Language)}-{ReversoContextClient.LangToString(entry.TraslationLanguage)}";

        return Binding.DoNothing;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Convert(value, targetType, parameter, culture);
    }
}