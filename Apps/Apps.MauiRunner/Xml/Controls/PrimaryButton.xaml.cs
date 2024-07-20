using System.Windows.Input;

namespace Apps.MauiRunner.Xml.Controls;

public partial class PrimaryButton : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(PrimaryButton),
        default(string));

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(PrimaryButton),
        default(ICommand));   
    
    public new static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
        nameof(IsVisible),
        typeof(bool),
        typeof(PrimaryButton),
        true);

    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
        nameof(IsEnabled),
        typeof(bool),
        typeof(PrimaryButton),
        true);
    
    public new static readonly BindableProperty WidthRequestProperty = BindableProperty.Create(
        nameof(WidthRequest),
        typeof(double),
        typeof(PrimaryButton),
        170.0);
    
    public new static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(
        nameof(HeightRequest),
        typeof(double),
        typeof(PrimaryButton),
        50.0);
    
    public new static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
        nameof(ImageSource),
        typeof(ImageSource),
        typeof(PrimaryButton));

    public PrimaryButton()
    {
        InitializeComponent();
    }

    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }    
    
    public new bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }
    
    public new double? WidthRequest
    {
        get => (double?)GetValue(WidthRequestProperty);
        set => SetValue(WidthRequestProperty, value);
    }
    public new double? HeightRequest
    {
        get => (double?)GetValue(HeightRequestProperty);
        set => SetValue(HeightRequestProperty, value);
    }
    
    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }
}

