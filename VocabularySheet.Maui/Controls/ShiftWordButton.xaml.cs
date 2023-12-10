using System.Windows.Input;
using VocabularySheet.Application.Commons.Dtos;

namespace VocabularySheet.Maui.Controls;

public partial class ShiftWordButton : ContentView
{
    public static readonly BindableProperty WordProperty = BindableProperty.Create(
        nameof(Word),
        typeof(WordModel),
        typeof(ShiftWordButton));
    
    public static readonly BindableProperty OnClickProperty = BindableProperty.Create(
        nameof(OnClick),
        typeof(ICommand),
        typeof(ShiftWordButton));
    
    public ShiftWordButton()
    {
        InitializeComponent();
    }
    
    public ICommand? OnClick
    {
        get => (ICommand)GetValue(OnClickProperty);
        set => SetValue(OnClickProperty, value);
    }
    
    public WordModel? Word
    {
        get => (WordModel?)GetValue(WordProperty);
        set => SetValue(WordProperty, value);
    }
    
    private async void Tapped(object sender, EventArgs e)
    {
        if (sender is Frame frame)
        {
            Color originalColor = frame.BackgroundColor;
#pragma warning disable CS0618 // Type or member is obsolete
            Color darkerColor = Color.FromHex("#0A0F1C"); // Adjust the color code as needed
#pragma warning restore CS0618 // Type or member is obsolete

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

        if (Word != null)
        {
            OnClick?.Execute(Word.Id);
        }
    }

}

