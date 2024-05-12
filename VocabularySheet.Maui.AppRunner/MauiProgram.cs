using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using VocabularySheet.Application;
using VocabularySheet.Infrastructure;
using VocabularySheet.Maui.AppRunner.Common.Services;
using VocabularySheet.Maui.AppRunner.ViewModels;
using VocabularySheet.Maui.AppRunner.Views;
using VocabularySheet.Maui.AppRunner.Xml.Controls;
using VocabularySheet.ML.Client;

namespace VocabularySheet.Maui.AppRunner;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif


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
        builder.Services.AddPrediction(new MlModelsFolder());

        builder.Services.AddApplicationServices();

        builder.Services.AddSingleton<AppShellVM>();
        builder.Services.AddSingleton(AudioManager.Current);

        builder.Services.AddSingleton<TextToSpeechService>();

        builder.Services.AddSingleton<GoogleSheets>()
            .AddSingleton<GoogleSheetsVM>();

        builder.Services.AddSingleton<WordsSpin>()
            .AddSingleton<WordsSpinVM>();
        
        builder.Services.AddSingleton<WordsList>()
            .AddSingleton<WordsListVM>();
        
        builder.Services.AddSingleton<WordDetails>()
            .AddSingleton<WordDetailsVm>();
        
        builder.Services.AddSingleton<WordSearch>()
            .AddSingleton<WordSearchVm>();
        
        builder.Services.AddSingleton<WordClassification>()
            .AddSingleton<WordClassificationVm>();

        return builder.Build();
    }
}