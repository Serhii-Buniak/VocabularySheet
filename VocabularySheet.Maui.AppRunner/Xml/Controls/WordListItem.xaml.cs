using System.Windows.Input;
using VocabularySheet.Application.Commons.Dtos;

namespace VocabularySheet.Maui.AppRunner.Xml.Controls;

public partial class WordListItem : ContentView
{
    public static readonly BindableProperty WordProperty = BindableProperty.Create(
        nameof(Word),
        typeof(WordModel),
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
        typeof(WordListItem),
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
    
    public WordModel Word
    {
        get => (WordModel)GetValue(WordProperty);
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
    
    private async void Tapped(object sender, EventArgs e)
    {
        if (sender is Border frame)
        {
            Color originalColor = frame.BackgroundColor;
#pragma warning disable CS0618 // Label or member is obsolete
            Color darkerColor = Color.FromHex("#0A0F1C"); // Adjust the color code as needed
#pragma warning restore CS0618 // Label or member is obsolete

            // Animate to darker color
            await frame.FadeTo(0.8, 10, Easing.Linear);
            frame.BackgroundColor = darkerColor;
            await frame.FadeTo(1, 10, Easing.Linear);

            // Implement your additional logic here

            // Animate back to the original color
            await frame.FadeTo(0.8, 10, Easing.Linear);
            frame.BackgroundColor = originalColor;
            await frame.FadeTo(1, 10, Easing.Linear);
        }
        
        OnClick?.Execute(Word.Id);
        // Implement your additional logic here
    }
}

