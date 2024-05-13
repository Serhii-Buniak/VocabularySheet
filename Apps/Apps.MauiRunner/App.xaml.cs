using Apps.MauiRunner.ViewModels;
using ApplicationMaui = Microsoft.Maui.Controls.Application;

namespace Apps.MauiRunner;

public partial class App : ApplicationMaui
{
    public App(AppShellVM appShellVm)
    {
        InitializeComponent();
        
        if (Current != null)
        {
            Current.UserAppTheme = AppTheme.Dark;
        }
        
        MainPage = new AppShell(appShellVm);
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