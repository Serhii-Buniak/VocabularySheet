using System.Globalization;

namespace VocabularySheet.Maui.AppRunner.Converters;

public class IrregularsConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is List<string> irregulars)
            return string.Join(" | ", irregulars);

        return Binding.DoNothing;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Convert(value, targetType, parameter, culture);
    }
}