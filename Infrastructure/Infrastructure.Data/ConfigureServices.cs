using Application.Common.Commons.Interfaces;
using Domain.Common;
using Domain.Localization;
using Infrastructure.Data.CsvStreamers;
using Infrastructure.Data.Data;
using Infrastructure.Data.Data.Interfaces;
using Infrastructure.Data.HttpClients;
using Infrastructure.Data.HttpClients.Interfaces;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories.Configurations;
using Infrastructure.Data.Repositories.Interfaces;
using Infrastructure.Data.Repositories.Pages;
using Infrastructure.Data.Services;
using Infrastructure.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebSources.CambridgeDictionary;
using WebSources.ReversoContext;

namespace Infrastructure.Data;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, InfrastructureOptions options)
    {
        services.AddSingleton(options);

        services.AddDatabase(options);
        services.AddRepositories();
        
        services.AddHttpClients();

        services.AddServices();

        services.AddCsvSteamers();
        
        services.AddCambridgeDictionary();
        services.AddReversoContext();
        
        return services;
    }
    
    private static void AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<StreamFetcherClient>();
        services.AddHttpClient<IGoogleSheetClient, GoogleSheetClient>();
    }
    
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAppDbContext, AppDbContext>();

        services.AddScoped<IWordsRepository, WordsRepository>();
        services.AddScoped<ICambridgeRepository, CambridgeRepository>();
        services.AddScoped<IReversoContextRepository, ReversoContextRepository>();

        services.AddConfigurationRepository<GoogleSheetConfig, GoogleSheetConfigurator>();
        services.AddConfigurationRepository<LocalizationConfig, LocalizationConfigurator>();
        
        
        services.AddScoped<IGoogleSheetWordsRepository, GoogleSheetWordsRepository>();
    }

    private static void AddDatabase(this IServiceCollection services, InfrastructureOptions options)
    {
        var path = $"Filename={Path.Combine(options.DataDirectory, "words.db")}";
        
        services.AddDbContext<AppDbContext>(o
            => o.UseSqlite(path, x
            => x.MigrationsAssembly("Infrastructure.Data")
            )
        );
    }
    
    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAppDataService, AppDataService>();
        services.AddScoped<IGoogleSheetService, GoogleSheetService>();
        services.AddTextToSpeech();
        services.AddWordDescription();
    }
    
    private static void AddWordDescription(this IServiceCollection services)
    {
        services.AddScoped<IWordDescriptionService, WordDescriptionService>();
        services.AddWordDescriptionProvider<CambridgeRepository>();
        services.AddWordDescriptionProvider<ReversoContextRepository>();
        services.AddWordDescriptionProvider<WordsRepository>();
    }
    
    private static void AddTextToSpeech(this IServiceCollection services)
    {
        services.AddScoped<ITextToSpeechService, TextToSpeechService>();
        services.AddTextToSpeechProvider<CambridgeRepository>();
    }
    
    private static void AddTextToSpeechProvider<T>(this IServiceCollection services) where T : class, ITextToSpeechProvider
    {
        services.AddScoped<ITextToSpeechProvider, T>();
    }
    
    private static void AddWordDescriptionProvider<T>(this IServiceCollection services) where T : class, IWordDescriptionProvider
    {
        services.AddScoped<IWordDescriptionProvider, T>();
    }

    private static void AddCsvSteamers(this IServiceCollection services)
    {
        services.AddSingleton<CsvWordStreamer>();
    }
    
    private static void AddConfigurationRepository<TEntity, TRepository>(this IServiceCollection services)
        where TEntity : BaseConfigurationEntity<TEntity>, new()
        where TRepository : BaseConfigurator<TEntity>
    {
        services.AddScoped<TRepository>();
        services.AddScoped<IConfigurator<TEntity>, TRepository>();
    }
}
