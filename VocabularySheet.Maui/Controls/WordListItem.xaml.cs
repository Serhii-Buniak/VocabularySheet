using System.Windows.Input;
using VocabularySheet.Application.Commons.Dtos;

namespace VocabularySheet.Maui.Controls;

public partial class WordListItem : ContentView
{
    public static readonly BindableProperty WordProperty = BindableProperty.Create(
        nameof(Word),
        typeof(WordSpinDto),
        typeof(WordListItem));
    
    public static readonly BindableProperty OnClickProperty = BindableProperty.Create(
        nameof(OnClick),
        typeof(ICommand),
        typeof(WordListItem));
    
    public static readonly BindableProperty MaximumWidthProperty = BindableProperty.Create(
        nameof(MaximumWidth),
        typeof(int?),
        typeof(WordListItem));
    
    public static readonly BindableProperty IsWordVisibleProperty = BindableProperty.Create(
        nameof(IsWordVisible),
        typeof(bool),
        typeof(DescriptionArea),
        true);

    
    public WordListItem()
    {
        InitializeComponent();
    }
    
    public ICommand? OnClick
    {
        get => (ICommand)GetValue(OnClickProperty);
        set => SetValue(OnClickProperty, value);
    }
    
    public WordSpinDto Word
    {
        get => (WordSpinDto)GetValue(WordProperty);
        set => SetValue(WordProperty, value);
    }
    
    public int? MaximumWidth
    {
        get => (int?)GetValue(MaximumWidthProperty);
        set => SetValue(MaximumWidthProperty, value);
    }
    
    public bool IsWordVisible
    {
        get => (bool)GetValue(IsWordVisibleProperty);
        set => SetValue(IsWordVisibleProperty, value);
    }
    
    public void OnFrameTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.GestureRecognizers[0] is TapGestureRecognizer tapGestureRecognizer)
        {
            OnClick?.Execute(tapGestureRecognizer.CommandParameter ?? 0);
        }
    }
}

