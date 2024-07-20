using System.Windows.Input;

namespace Apps.MauiRunner.Xml.Controls;

public partial class PrimaryTriggerButton : ContentView
{
    public static readonly BindableProperty TriggerConditionProperty = BindableProperty.Create(
        nameof(TriggerCondition),
        typeof(bool),
        typeof(PrimaryTriggerButton),
        default(bool));

    public static readonly BindableProperty TriggerTextProperty = BindableProperty.Create(
        nameof(TriggerText),
        typeof(string),
        typeof(PrimaryTriggerButton),
        default(string));

    public static readonly BindableProperty TriggerCommandProperty = BindableProperty.Create(
        nameof(TriggerCommand),
        typeof(ICommand),
        typeof(PrimaryTriggerButton),
        default(ICommand));

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(PrimaryTriggerButton),
        default(string));

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(PrimaryTriggerButton),
        default(ICommand));


    public new static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
        nameof(IsVisible),
        typeof(bool),
        typeof(PrimaryTriggerButton),
        true);

    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
        nameof(IsEnabled),
        typeof(bool),
        typeof(PrimaryTriggerButton),
        true);
    
    public new static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
        nameof(ImageSource),
        typeof(ImageSource),
        typeof(PrimaryTriggerButton));
    
    public static readonly BindableProperty TriggerImageSourceProperty = BindableProperty.Create(
        nameof(TriggerImageSource),
        typeof(ImageSource),
        typeof(PrimaryTriggerButton));


    public PrimaryTriggerButton()
    {
        InitializeComponent();
    }

    public bool TriggerCondition
    {
        get => (bool)GetValue(TriggerConditionProperty);
        set => SetValue(TriggerConditionProperty, value);
    }

    public string TriggerText
    {
        get => (string)GetValue(TriggerTextProperty);
        set => SetValue(TriggerTextProperty, value);
    }

    public ICommand TriggerCommand
    {
        get => (ICommand)GetValue(TriggerCommandProperty);
        set => SetValue(TriggerCommandProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
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

    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }
    
    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    
    public ImageSource? TriggerImageSource
    {
        get => (ImageSource?)GetValue(TriggerImageSourceProperty);
        set => SetValue(TriggerImageSourceProperty, value);
    }

}