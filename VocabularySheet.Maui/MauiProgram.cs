using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using VocabularySheet.Application;
using VocabularySheet.Infrastructure;
using VocabularySheet.Maui.Controls;
using VocabularySheet.Maui.Views;
using VocabularySheet.Maui.ViewModels;
using TextToSpeechService = VocabularySheet.Maui.Common.Services.TextToSpeechService;

namespace VocabularySheet.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkitCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
        {
            if (v is BorderlessEditor)
            {
#if WINDOWS
                h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness()
                {
                    Bottom = 0,
                    Top = 0,
                    Left = 0,
                    Right = 0,
                };
#endif
            }
        });
        
#if DEBUG
        builder.Logging.AddDebug();
#endif
       

        builder.Services.AddInfrastructureServices(new()
        {
            DataDirectory = FileSystem.Current.AppDataDirectory
        });
        builder.Services.AddApplicationServices();

        builder.Services.AddSingleton<AppShellVM>();
        builder.Services.AddSingleton(AudioManager.Current);

        builder.Services.AddSingleton<TextToSpeechService>();

        builder.Services.AddSingleton<GoogleSheets>()
                        .AddSingleton<GoogleSheetsVM>();

        builder.Services.AddSingleton<WordsSpin>()
                        .AddSingleton<WordsSpinVM>();
        
        builder.Services.AddSingleton<LanguageWord>()
                        .AddSingleton<LanguageWordVM>();
        
        builder.Services.AddSingleton<WordsList>()
                        .AddSingleton<WordsListVM>();
        
        builder.Services.AddSingleton<WordDetails>()
                        .AddSingleton<WordDetailsVM>();

        return builder.Build();
    }
}