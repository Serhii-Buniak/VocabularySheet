using System.Globalization;
using Microsoft.ML.Data;
using Microsoft.UI.Xaml.Data;

namespace VocabularySheet.ML.Evaluation.App.Converters;

public class ConfusionMatrixConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)   
    {
        if (value is ConfusionMatrix confusionMatrix)
        {
            return confusionMatrix.GetFormattedConfusionTable();
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return Convert(value, targetType, parameter, language);
    }
}
