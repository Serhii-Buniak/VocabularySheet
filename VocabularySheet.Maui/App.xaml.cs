using VocabularySheet.Maui.Controls;
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
        Window? window = base.CreateWindow(activationState);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (window != null)
        {
            window.Title = "Vocabulary Sheet";
        }

        return window!;
    }
}