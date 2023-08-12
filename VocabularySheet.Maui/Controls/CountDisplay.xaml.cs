namespace VocabularySheet.Maui.Controls;

public partial class CountDisplay : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(CountDisplay),
        default(string));

    public static readonly BindableProperty CountProperty = BindableProperty.Create(
        nameof(Count),
        typeof(string),
        typeof(CountDisplay),
        default(string));

    public CountDisplay()
	{
		InitializeComponent();
	}
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Count
    {
        get => (string)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }
}