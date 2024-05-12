using System.Windows.Input;

namespace VocabularySheet.Maui.Domain.Xml.Controls;

public partial class SwiftButton : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(SwiftButton),
        default(string));

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(SwiftButton),
        default(ICommand));


    public static readonly BindableProperty CommandParameterProperty =
    BindableProperty.Create(
        nameof(CommandParameter),
        typeof(object),
        typeof(SwiftButton),
        null);


    public SwiftButton()
    {
        InitializeComponent();
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

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

}