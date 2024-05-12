using VocabularySheet.Maui.Domain.ViewModels;

namespace VocabularySheet.Maui.Domain.Xml.Controls;

public partial class BoxModels : ContentView
{
    public static readonly BindableProperty BoxVmProperty = BindableProperty.Create(
        nameof(BoxVm),
        typeof(LinkBoxVm),
        typeof(BoxModels));
    

    public BoxModels()
    {
        InitializeComponent();
    }

    public LinkBoxVm BoxVm
    {
        get => (LinkBoxVm)GetValue(BoxVmProperty);
        set => SetValue(BoxVmProperty, value);
    }
}