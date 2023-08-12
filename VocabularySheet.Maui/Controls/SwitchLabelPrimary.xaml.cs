namespace VocabularySheet.Maui.Controls;

public partial class SwitchLabelPrimary : ContentView
{
    public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(
        nameof(IsToggled),
        typeof(bool),
        typeof(SwitchLabelPrimary),
        default(bool),
        defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
        nameof(LabelText),
        typeof(string),
        typeof(SwitchLabelPrimary),
        default(string));

    public SwitchLabelPrimary()
	{
		InitializeComponent();
	}

    public bool IsToggled
    {
        get => (bool)GetValue(IsToggledProperty);
        set => SetValue(IsToggledProperty, value);
    }   
    
    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

}