namespace VocabularySheet.Maui.Controls;

public partial class EditorForUrl : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(EditorForUrl),
        default(string));

    public EditorForUrl()
	{
		InitializeComponent();
	}

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}