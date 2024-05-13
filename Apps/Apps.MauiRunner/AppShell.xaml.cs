using Apps.MauiRunner.ViewModels;
using Apps.MauiRunner.Views;

namespace Apps.MauiRunner;

public partial class AppShell : Shell
{
    public AppShell(AppShellVM appShellVm)
    {
        InitializeComponent();

        BindingContext = appShellVm;
        
        Routing.RegisterRoute(nameof(WordDetails), typeof(WordDetails));
    }
}