using System.Windows.Input;

namespace VocabularySheet.Maui.Controls;

public partial class SpinButton : ContentView
{

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(SpinButton), string.Empty);
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }


    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SpinButton), null);

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }


    public static readonly new BindableProperty IsEnabledProperty =
        BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(SpinButton), true);

    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    public static readonly BindableProperty AltTextProperty =
        BindableProperty.Create(nameof(AltText), typeof(string), typeof(SpinButton), string.Empty);
    public string AltText
    {
        get => (string)GetValue(AltTextProperty);
        set => SetValue(AltTextProperty, value);
    }


    public static readonly BindableProperty AltCommandProperty =
        BindableProperty.Create(nameof(AltCommand), typeof(ICommand), typeof(SpinButton), null);

    public ICommand AltCommand
    {
        get => (ICommand)GetValue(AltCommandProperty);
        set => SetValue(AltCommandProperty, value);
    }


    public static readonly BindableProperty AltIsEnabledProperty =
        BindableProperty.Create(nameof(AltIsEnabled), typeof(bool), typeof(SpinButton), true);

    public bool AltIsEnabled
    {
        get => (bool)GetValue(AltIsEnabledProperty);
        set => SetValue(AltIsEnabledProperty, value);
    }   
    
    
    public static readonly BindableProperty IsAlternativeModeProperty =
        BindableProperty.Create(nameof(IsAlternativeMode), typeof(bool), typeof(SpinButton), false);

    public bool IsAlternativeMode
    {
        get => (bool)GetValue(IsAlternativeModeProperty);
        set => SetValue(IsAlternativeModeProperty, value);
    }

    public bool IsNotAlternativeMode => !IsAlternativeMode;


    public SpinButton()
    {
        InitializeComponent();
        BindingContext = this;
    }
}