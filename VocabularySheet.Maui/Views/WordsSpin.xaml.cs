using VocabularySheet.Maui.ViewModels;

namespace VocabularySheet.Maui.Views;

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
            const int red = 0;
            const int green = 120;
            const int blue = 240;
            
            var old = WordsLayout.BackgroundColor;

            WordsLayout.BackgroundColor = Color.FromRgba(red, green, blue, 10);
            await Task.Delay(10);    
            
            WordsLayout.BackgroundColor = Color.FromRgba(red, green, blue, 20);
            await Task.Delay(10);  
            
            WordsLayout.BackgroundColor = Color.FromRgba(red, green, blue, 30);
            await Task.Delay(10);
            
            WordsLayout.BackgroundColor = Color.FromRgba(red, green, blue, 40);
            await Task.Delay(10);    
            
            WordsLayout.BackgroundColor = Color.FromRgba(red, green, blue, 50);
            await Task.Delay(10);    
            
            WordsLayout.BackgroundColor = Color.FromRgba(red, green, blue, 40);
            await Task.Delay(10);
            
            WordsLayout.BackgroundColor = Color.FromRgba(red, green, blue, 30);
            await Task.Delay(10);       
            
            WordsLayout.BackgroundColor = Color.FromRgba(red, green, blue, 20);
            await Task.Delay(10); 
            
            WordsLayout.BackgroundColor = Color.FromRgba(red, green, blue, 10);
            await Task.Delay(25);
            
            WordsLayout.BackgroundColor = old;
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