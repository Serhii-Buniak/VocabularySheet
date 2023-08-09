namespace VocabularySheet.Maui;

using ApplicationMaui = Microsoft.Maui.Controls.Application;

public partial class App : ApplicationMaui
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        if (window != null)
        {
            window.Title = "Vocabulary Sheet";
        }

        return window!;
    }
}
