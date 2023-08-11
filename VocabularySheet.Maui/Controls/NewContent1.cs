namespace VocabularySheet.Maui.Controls;

public class LabeledSwitch : ContentView
{
	public LabeledSwitch()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}