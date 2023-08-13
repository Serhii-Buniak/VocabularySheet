using CommunityToolkit.Mvvm.ComponentModel;
using VocabularySheet.Maui.ViewModels;
using ApplicationMaui = Microsoft.Maui.Controls.Application;

namespace VocabularySheet.Maui;

public partial class App : ApplicationMaui
{
    public App(AppShellVM appShellVM)
    {
        InitializeComponent();
        MainPage = new AppShell(appShellVM);
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

