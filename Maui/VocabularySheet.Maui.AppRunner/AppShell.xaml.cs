using VocabularySheet.Maui.Domain.ViewModels;
using VocabularySheet.Maui.Domain.Views;

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