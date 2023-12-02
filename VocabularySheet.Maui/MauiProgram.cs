﻿using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using VocabularySheet.Application;
using VocabularySheet.Infrastructure;
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

#if DEBUG
        builder.Logging.AddDebug();
#endif
       

        builder.Services.AddInfrastructureServices(new()
        {
            DataDirectory = FileSystem.Current.AppDataDirectory
        });
        builder.Services.AddApplicationServices();

        builder.Services.AddSingleton<AppShellVM>();

        builder.Services.AddSingleton<TextToSpeechService>();

        builder.Services.AddSingleton<GoogleSheets>()
                        .AddSingleton<GoogleSheetsVM>();

        builder.Services.AddSingleton<WordsSpin>()
                        .AddSingleton<WordsSpinVM>();
        
        builder.Services.AddSingleton<LanguageWord>()
                        .AddSingleton<LanguageWordVM>();


        return builder.Build();
    }
}