using GoogleSheetsVM = VocabularySheet.Maui.Domain.ViewModels.GoogleSheetsVM;
using WordsSpinVM = VocabularySheet.Maui.Domain.ViewModels.WordsSpinVM;

namespace VocabularySheet.Maui.Domain.Views;

public partial class WordsSpin : ContentPage
{
    private readonly WordsSpinVM _wordsSpinVM;

    public WordsSpin(WordsSpinVM wordsSpinVM, GoogleSheetsVM googleSheetsVm)
    {
        _wordsSpinVM = wordsSpinVM;
        InitializeComponent();
        BindingContext = wordsSpinVM;
        
        googleSheetsVm.OnSynchronize += async (_, _) => await wordsSpinVM.HandleSynchronize();
        wordsSpinVM.OnClipboard += async (_, _) =>
        {
            const byte red = 47;
            const byte green = 79;
            const byte blue = 79;
            
            Color originalColor = WordsLayout.BackgroundColor;
            Color darkerColor = Color.FromRgb(red, green, blue); 

            // Animate to darker color
            await WordsLayout.FadeTo(0.5, 125, Easing.Linear);
            WordsLayout.BackgroundColor = darkerColor;
            await WordsLayout.FadeTo(1, 125, Easing.Linear);

            // Implement your additional logic here

            // Animate back to the original color
            await WordsLayout.FadeTo(0.5, 125, Easing.Linear);
            WordsLayout.BackgroundColor = originalColor;
            await WordsLayout.FadeTo(1, 125, Easing.Linear);
        };
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (_wordsSpinVM.MaxIndex == 0)
        {
            await _wordsSpinVM.SetMaxIndex();
            _wordsSpinVM.ResetIndex();
        }
        
        _wordsSpinVM.ResetSpin();
    }
}