using Apps.MauiRunner.Views;
using Domain.WordModels;
using ML.Predictor;

namespace Apps.MauiRunner;

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

    public static Color ToMauiColor(this Category category)
    {
        return category switch
        {
            Category.Red => Color.FromRgba(255, 0, 0, 255),       // Red color
            Category.Green => Color.FromRgba(0, 255, 0, 255),     // Green color
            Category.Yellow => Color.FromRgba(255, 255, 0, 255),  // Yellow color
            Category.Orange => Color.FromRgba(255, 165, 0, 255),  // Orange color
            Category.Purple => Color.FromRgba(128, 0, 128, 255),  // Purple color
            Category.Pink => Color.FromRgba(255, 192, 203, 255),  // Pink color
            _ => Color.FromRgba(0, 0, 0, 255)                     // Default to black for Unknown or any undefined category
        };
    }

    public static Color ToMauiColor(this ArticleType articleType)
    {
        return articleType switch
        {
            ArticleType.Sport => Color.FromRgba(30, 40, 40, 240),
            ArticleType.Science => Color.FromRgba(12, 15, 30, 240),
            ArticleType.Religion => Color.FromRgba(30, 30, 20, 240),
            ArticleType.Politics => Color.FromRgba(90, 90, 90, 240),
            ArticleType.Medical => Color.FromRgba(25, 4, 4, 240),
            ArticleType.Historical => Color.FromRgba(35, 18, 17, 240),
            ArticleType.Fantasy => Color.FromRgba(20, 8, 25, 240),
            ArticleType.Economic => Color.FromRgba(18, 69, 48, 240),
            ArticleType.Digital => Color.FromRgba(0, 25, 25, 240),
            ArticleType.Culinary => Color.FromRgba(12, 27, 12, 240),
            _ => Color.FromRgba(12, 12, 12, 240)
        };
    }

    public static Color ToAlphaMauiColor(this ArticleType articleType)
    {
        return articleType switch
        {
            _ => articleType.ToMauiColor().WithAlpha(0.60f)
        };
    }

}