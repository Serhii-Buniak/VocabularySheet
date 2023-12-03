using VocabularySheet.Maui.Views;

namespace VocabularySheet.Maui.Common;

public static class Extensions
{
    public static async Task GoToWordDetails(this Shell shell, long wordId)
    {
        await shell.GoToAsync($"{nameof(WordDetails)}?Id={wordId}");
    }
    
    public static async Task GoToBack(this Shell shell)
    {
        await shell.GoToAsync("..");
    }
}