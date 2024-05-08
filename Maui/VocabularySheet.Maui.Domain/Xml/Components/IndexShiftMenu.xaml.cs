using System.Windows.Input;

namespace VocabularySheet.Maui.Domain.Xml.Components;

public partial class IndexShiftMenu : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(IndexShiftMenu),
        default(string));

    public static readonly BindableProperty CountProperty = BindableProperty.Create(
        nameof(Count),
        typeof(string),
        typeof(IndexShiftMenu),
        default(string));

    public static readonly BindableProperty ShiftCommandProperty = BindableProperty.Create(
        nameof(ShiftCommand),
        typeof(ICommand),
        typeof(IndexShiftMenu),
        default(ICommand));


    public IndexShiftMenu()
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

    public ICommand ShiftCommand
    {
        get => (ICommand)GetValue(ShiftCommandProperty);
        set => SetValue(ShiftCommandProperty, value);
    }
}