﻿using System.Globalization;
using VocabularySheet.Parsing.Common;

namespace VocabularySheet.Maui.Converters;

public class ImageUrlConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IHaveNullImageUrl image)
            return image.FullImageLink();

        return Binding.DoNothing;
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Convert(value, targetType, parameter, culture);
    }
}