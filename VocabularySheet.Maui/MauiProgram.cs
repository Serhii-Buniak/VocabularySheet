using Microsoft.Extensions.Logging;
using VocabularySheet.Maui.Data;
using VocabularySheet.Infrastructure;
using VocabularySheet.Application;

namespace VocabularySheet.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<WeatherForecastService>();

		builder.Services.AddInfrastructureServices(new()
		{
			DataDirectory = FileSystem.Current.AppDataDirectory
		});

		builder.Services.AddApplicationServices();

        return builder.Build();
	}
}
