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