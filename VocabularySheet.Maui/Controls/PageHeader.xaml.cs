using System.Windows.Input;

namespace VocabularySheet.Maui.Controls;

public partial class PageHeader : ContentView
{
    public static readonly BindableProperty ImageProperty = BindableProperty.Create(
        nameof(Image),
        typeof(ImageSource),
        typeof(PageHeader));
    
    public static readonly BindableProperty OpenLinkCommandProperty = BindableProperty.Create(
        nameof(OpenLinkCommand),
        typeof(ICommand),
        typeof(PageHeader));
    
    public static readonly BindableProperty LinkProperty = BindableProperty.Create(
        nameof(Link),
        typeof(string),
        typeof(PageHeader));
    
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(PageHeader));
    
    public static readonly BindableProperty LanguageProperty = BindableProperty.Create(
        nameof(Language),
        typeof(string),
        typeof(PageHeader));
    
    public PageHeader()
    {
        InitializeComponent();
    }

    public ImageSource Image
    {
        get => (ImageSource)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }
    
    public ICommand OpenLinkCommand
    {
        get => (ICommand)GetValue(OpenLinkCommandProperty);
        set => SetValue(OpenLinkCommandProperty, value);
    }
    
    public string Link
    {
        get => (string)GetValue(LinkProperty);
        set => SetValue(LinkProperty, value);
    }
    
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public string Language
    {
        get => (string)GetValue(LanguageProperty);
        set => SetValue(LanguageProperty, value);
    }
}