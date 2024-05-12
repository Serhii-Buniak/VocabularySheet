using System.Globalization;

namespace VocabularySheet.Maui.AppRunner.Converters;

public class JoinListConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is IList<string> enumerable)
            return string.Join(" | ", enumerable);

        return Binding.DoNothing;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Convert(value, targetType, parameter, culture);
    }
}