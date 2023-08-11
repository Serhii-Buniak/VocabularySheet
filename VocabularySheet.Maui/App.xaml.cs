using ApplicationMaui = Microsoft.Maui.Controls.Application;

namespace VocabularySheet.Maui;

public partial class App : ApplicationMaui
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        if (window != null)
        {
            window.Title = "Word Collusion";
        }

        return window!;
    }
}
