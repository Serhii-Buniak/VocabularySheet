using VocabularySheet.Maui.ViewModels;

namespace VocabularySheet.Maui.Views;

public partial class WordDetails : ContentPage
{
    private readonly WordDetailsVM _wordDetails;

    public WordDetails(WordDetailsVM wordDetails)
    {
        InitializeComponent();
        _wordDetails = wordDetails;
        BindingContext = wordDetails;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _wordDetails.LoadDataAsync();
    }
}