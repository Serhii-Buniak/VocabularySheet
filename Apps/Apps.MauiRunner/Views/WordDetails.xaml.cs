using WordDetailsVm = Apps.MauiRunner.ViewModels.WordDetailsVm;

namespace Apps.MauiRunner.Views;

public partial class WordDetails : ContentPage
{
    private readonly WordDetailsVm _wordDetails;

    public WordDetails(WordDetailsVm wordDetails)
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