using Application.Common;
using Apps.MauiRunner.Common.Services;
using Apps.MauiRunner.ViewModels;
using Apps.MauiRunner.Views;
using Apps.MauiRunner.Xml.Controls;
using CommunityToolkit.Maui;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using VocabularySheet.ML.Client;

namespace Apps.MauiRunner;

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

        builder.Services.AddInfrastructureServices(new()
        {
            DataDirectory = FileSystem.Current.AppDataDirectory
        });
        builder.Services.AddPrediction(new MlModelsFolder());

        builder.Services.AddApplicationServices();

        builder.Services.AddSingleton<AppShellVM>();
        builder.Services.AddSingleton(AudioManager.Current);

        builder.Services.AddSingleton<MauiTextToSpeechService>();

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