using System.Windows.Input;
using WebSources.Common;

namespace VocabularySheet.Maui.Domain.Xml.Controls;

public partial class ButtonIcon : ContentView
{
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
        nameof(ImageSource),
        typeof(ImageSource),
        typeof(ButtonIcon));
    
    public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(
        nameof(ClickCommand),
        typeof(ICommand),
        typeof(ButtonIcon));
    
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(
        nameof(Color),
        typeof(Color),
        typeof(ButtonIcon));  
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Color),
        typeof(ButtonIcon));  

    
    public ButtonIcon()
    {
        InitializeComponent();
    }
    
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    
    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    
    public ICommand ClickCommand
    {
        get => (ICommand)GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }
    
    private async void Tapped(object sender, EventArgs e)
    {
        if (sender is Border frame)
        {
            Color originalColor = frame.BackgroundColor;
#pragma warning disable CS0618 // Label or member is obsolete
            Color darkerColor = Color.FromHex("#0A0F1C"); // Adjust the color code as needed
#pragma warning restore CS0618 // Label or member is obsolete

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
        
        ClickCommand.Execute(new object());
    }
}

