using System.Windows.Input;
using VocabularySheet.Domain.Pages;

namespace VocabularySheet.Maui.Domain.Xml.Controls.ReversoContext;

public partial class ReversoContextComponent : ContentView
{
    public static readonly BindableProperty EntryProperty = BindableProperty.Create(
        nameof(Entry),
        typeof(PublicReversoContextEntry),
        typeof(ReversoContextComponent));

    public static readonly BindableProperty OpenLinkCommandProperty = BindableProperty.Create(
        nameof(OpenLinkCommand),
        typeof(ICommand),
        typeof(ReversoContextComponent));
    
    public ReversoContextComponent()
    {
        InitializeComponent();
    }

    public ICommand OpenLinkCommand
    {
        get => (ICommand)GetValue(OpenLinkCommandProperty);
        set => SetValue(OpenLinkCommandProperty, value);
    }
    
    public PublicReversoContextEntry Entry
    {
        get => (PublicReversoContextEntry)GetValue(EntryProperty);
        set => SetValue(EntryProperty, value);
    }
}

