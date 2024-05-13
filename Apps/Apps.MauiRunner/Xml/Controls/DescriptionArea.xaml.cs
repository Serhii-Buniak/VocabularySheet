namespace Apps.MauiRunner.Xml.Controls;

public partial class DescriptionArea : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(DescriptionArea));
    
    
    public static readonly BindableProperty IsDescriptionVisibleProperty = BindableProperty.Create(
        nameof(IsDescriptionVisible),
        typeof(bool),
        typeof(DescriptionArea),
        true);
    
    public DescriptionArea()
    {
        InitializeComponent();
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public bool IsDescriptionVisible
    {
        get => (bool)GetValue(IsDescriptionVisibleProperty);
        set => SetValue(IsDescriptionVisibleProperty, value);
    }
}

