using System.Windows.Input;
using VocabularySheet.Domain.Pages;

namespace VocabularySheet.Maui.Controls.Cambridge;

public partial class CambridgeComponent
{
    public static readonly BindableProperty EntryProperty = BindableProperty.Create(
        nameof(Entry),
        typeof(PublicCambridgeEntry),
        typeof(CambridgeComponent));
    
    public static readonly BindableProperty AudioCommandProperty = BindableProperty.Create(
        nameof(AudioCommand),
        typeof(ICommand),
        typeof(CambridgeComponent));
    
    public static readonly BindableProperty OpenLinkCommandProperty = BindableProperty.Create(
        nameof(OpenLinkCommand),
        typeof(ICommand),
        typeof(CambridgeComponent));
    public CambridgeComponent()
    {
        InitializeComponent();
    }
    
    public PublicCambridgeEntry? Entry
    {
        get => (PublicCambridgeEntry)GetValue(EntryProperty);
        set => SetValue(EntryProperty, value);
    }
    
    public ICommand AudioCommand
    {
        get => (ICommand)GetValue(AudioCommandProperty);
        set => SetValue(AudioCommandProperty, value);
    }
    
    public ICommand OpenLinkCommand
    {
        get => (ICommand)GetValue(OpenLinkCommandProperty);
        set => SetValue(OpenLinkCommandProperty, value);
    }
    
    private async void AudioTapped(object sender, EventArgs e)
    {
        if (sender is Border frame)
        {
            
            Color originalColor = frame.BackgroundColor;
#pragma warning disable CS0618 // Type or member is obsolete
            Color darkerColor = Color.FromHex("#555555"); // Adjust the color code as needed
#pragma warning restore CS0618 // Type or member is obsolete

            // Animate to darker color
            await frame.FadeTo(0.5, 250, Easing.Linear);
            frame.BackgroundColor = darkerColor;
            await frame.FadeTo(1, 250, Easing.Linear);

            // Implement your additional logic here

            // Animate back to the original color
            await frame.FadeTo(0.5, 250, Easing.Linear);
            frame.BackgroundColor = originalColor;
            await frame.FadeTo(1, 250, Easing.Linear);
        }
        // Implement your additional logic here
    }
}

