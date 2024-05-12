using VocabularySheet.Maui.AppRunner.Views;

namespace VocabularySheet.Maui.Domain;

public static class Extensions
{
    private static string Permanent(bool permanent)
    {
        return permanent ? "//" : string.Empty;
    }
    public static async Task GoToWordDetails(this Shell shell, long wordId, bool permanent = false)
    {
        await shell.GoToAsync($"{Permanent(permanent)}{nameof(WordDetails)}?Id={wordId}");
    }
    
    public static async Task GoToWordClassification(this Shell shell, string? word = null)
    {
        await shell.GoToAsync($"//{nameof(WordClassification)}?WordParam={word}");
    }
    
    public static async Task GoToBack(this Shell shell)
    {
        await shell.GoToAsync("..");
    }
}