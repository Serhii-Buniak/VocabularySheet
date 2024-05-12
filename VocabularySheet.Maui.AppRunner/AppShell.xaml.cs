using VocabularySheet.Maui.AppRunner.ViewModels;
using VocabularySheet.Maui.AppRunner.Views;

namespace VocabularySheet.Maui.AppRunner;

public partial class AppShell : Shell
{
    public AppShell(AppShellVM appShellVm)
    {
        InitializeComponent();

        BindingContext = appShellVm;
        
        Routing.RegisterRoute(nameof(WordDetails), typeof(WordDetails));
    }
}