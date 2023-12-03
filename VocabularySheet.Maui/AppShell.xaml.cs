using VocabularySheet.Maui.ViewModels;
using VocabularySheet.Maui.Views;

namespace VocabularySheet.Maui;

public partial class AppShell : Shell
{
    public AppShell(AppShellVM appShellVM)
    {
        InitializeComponent();
        BindingContext = appShellVM;
        
        Routing.RegisterRoute(nameof(WordDetails), typeof(WordDetails));
    }
}