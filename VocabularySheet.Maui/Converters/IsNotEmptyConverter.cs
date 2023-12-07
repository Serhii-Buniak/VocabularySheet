﻿using System.Globalization;

namespace VocabularySheet.Maui.Converters;

public class IsNotEmptyConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is List<object> enumerable)
            return enumerable.Count > 0;

        return Binding.DoNothing;
    }

    public object ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return Convert(value, targetType, parameter, culture);
    }
}