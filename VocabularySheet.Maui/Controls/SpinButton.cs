using System.Windows.Input;

namespace VocabularySheet.Maui.Controls;

public class SpinButton : ContentView
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

    public SpinButton()
    {
        Content = new Button
        {
            FontSize = 24,
            Margin = 5,
            WidthRequest = 160,
            Text = Text,
            Command = Command,
            IsEnabled = IsEnabled,
        };

    }
}