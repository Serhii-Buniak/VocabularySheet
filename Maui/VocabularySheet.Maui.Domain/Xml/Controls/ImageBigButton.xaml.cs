using System.Windows.Input;
using WebSources.Common;

namespace VocabularySheet.Maui.Domain.Xml.Controls;

public partial class ImageBigButton : ContentView
{
    public static readonly BindableProperty TranslatorLinkProperty = BindableProperty.Create(
        nameof(TranslatorLink),
        typeof(GoogleTranslatorLink),
        typeof(ImageBigButton));
    
    public static readonly BindableProperty OpenLinkCommandProperty = BindableProperty.Create(
        nameof(OpenLinkCommand),
        typeof(ICommand),
        typeof(ImageBigButton));
    
    public ImageBigButton()
    {
        InitializeComponent();
    }
    
    public GoogleTranslatorLink TranslatorLink
    {
        get => (GoogleTranslatorLink)GetValue(TranslatorLinkProperty);
        set => SetValue(TranslatorLinkProperty, value);
    }
    
    public ICommand OpenLinkCommand
    {
        get => (ICommand)GetValue(OpenLinkCommandProperty);
        set => SetValue(OpenLinkCommandProperty, value);
    }
    
    private async void Tapped(object sender, EventArgs e)
    {
        if (sender is Border frame)
        {
            Color originalColor = frame.BackgroundColor;
#pragma warning disable CS0618 // Type or member is obsolete
            Color darkerColor = Color.FromHex("#0A0F1C"); // Adjust the color code as needed
#pragma warning restore CS0618 // Type or member is obsolete

            // Animate to darker color
            await frame.FadeTo(0.8, 10, Easing.Linear);
            frame.BackgroundColor = darkerColor;
            await frame.FadeTo(1, 10, Easing.Linear);

            // Implement your additional logic here

            // Animate back to the original color
            await frame.FadeTo(0.8, 10, Easing.Linear);
            frame.BackgroundColor = originalColor;
            await frame.FadeTo(1, 10, Easing.Linear);
        }
        
        OpenLinkCommand.Execute(TranslatorLink.Link);
    }
}

