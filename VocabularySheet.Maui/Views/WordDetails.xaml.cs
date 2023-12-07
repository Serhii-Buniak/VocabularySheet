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
    
    private async void OnFrameTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame)
        {
            
            Color originalColor = frame.BackgroundColor;
#pragma warning disable CS0618 // Type or member is obsolete
            Color darkerColor = Color.FromHex("#555555"); // Adjust the color code as needed
#pragma warning restore CS0618 // Type or member is obsolete

            // Animate to darker color
            await frame.FadeTo(0.5, 250, Easing.Linear);
            frame.BackgroundColor = darkerColor;
            await frame.FadeTo(1, 250, Easing.Linear);

            // Implement your additional logic here

            // Animate back to the original color
            await frame.FadeTo(0.5, 250, Easing.Linear);
            frame.BackgroundColor = originalColor;
            await frame.FadeTo(1, 250, Easing.Linear);
        }
        // Implement your additional logic here
    }
}