namespace VocabularySheet.Maui.Services;

public struct LocaleAndText
{
    public LocaleAndText(string text, Locale locale)
    {
        Text = text;
        Locale = locale;
    }

    public string Text { get; set; }
    public Locale Locale { get; set; }
}