using Microsoft.Extensions.Logging;
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

		builder.Services.AddInfrastructureServices(new()
		{
			DataDirectory = FileSystem.Current.AppDataDirectory
		});

		builder.Services.AddSingleton<DebugService>();

		builder.Services.AddApplicationServices();

        return builder.Build();
	}
}
