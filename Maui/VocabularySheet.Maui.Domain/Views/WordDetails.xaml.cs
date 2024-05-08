using WordDetailsVM = VocabularySheet.Maui.Domain.ViewModels.WordDetailsVM;

namespace VocabularySheet.Maui.Domain.Views;

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