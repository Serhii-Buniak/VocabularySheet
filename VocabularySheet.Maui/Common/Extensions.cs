using VocabularySheet.Maui.Views;

namespace VocabularySheet.Maui.Common;

public static class Extensions
{
    public static async Task GoToWordDetails(this Shell shell)
    {
        await shell.GoToAsync(nameof(WordDetails));
    }
}