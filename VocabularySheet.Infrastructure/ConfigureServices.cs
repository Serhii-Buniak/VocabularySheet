using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Infrastructure.CsvStreamers;
using VocabularySheet.Infrastructure.Data;
using VocabularySheet.Infrastructure.Data.Interfaces;
using VocabularySheet.Infrastructure.HttpClients;
using VocabularySheet.Infrastructure.HttpClients.Interfaces;
using VocabularySheet.Infrastructure.Repositories;
using VocabularySheet.Infrastructure.Repositories.Configurations;
using VocabularySheet.Infrastructure.Repositories.Interfaces;
using VocabularySheet.Infrastructure.Repositories.Pages;
using VocabularySheet.Infrastructure.Services;
using VocabularySheet.Infrastructure.Services.Interfaces;
using WebSources.CambridgeDictionary;
using WebSources.ReversoContext;

namespace VocabularySheet.Infrastructure;

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
            => x.MigrationsAssembly("VocabularySheet.Infrastructure")
            )
        );
    }
    
    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAppDataService, AppDataService>();
        services.AddScoped<IGoogleSheetService, GoogleSheetService>();
    }

    private static void AddCsvSteamers(this IServiceCollection services)
    {
        services.AddSingleton<CsvWordStreamer>();
    }
    
    private static void AddConfigurationRepository<TEntity, TRepository>(this IServiceCollection services)
        where TEntity : BaseConfigurationEntity<TEntity>, new()
        where TRepository : BaseConfigurator<TEntity>
    {
        services.AddScoped<IConfigurator<TEntity>, TRepository>();
    }
}
