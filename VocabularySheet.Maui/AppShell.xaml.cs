using VocabularySheet.Maui.ViewModels;

namespace VocabularySheet.Maui;

public partial class AppShell : Shell
{
    public AppShell(AppShellVM appShellVM)
    {
        InitializeComponent();
        BindingContext = appShellVM;
    }
}