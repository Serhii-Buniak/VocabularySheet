using LinkBoxVm = Apps.MauiRunner.ViewModels.LinkBoxVm;

namespace Apps.MauiRunner.Xml.Controls;

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