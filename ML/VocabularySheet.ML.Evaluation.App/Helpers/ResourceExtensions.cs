using Microsoft.Windows.ApplicationModel.Resources;

namespace VocabularySheet.ML.Evaluation.App.Helpers;

public static class ResourceExtensions
{
    private static readonly ResourceLoader _resourceLoader = new();

    public static string GetLocalized(this string resourceKey) => _resourceLoader.GetString(resourceKey);
}
