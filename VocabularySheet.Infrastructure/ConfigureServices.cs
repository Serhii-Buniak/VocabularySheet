using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.CambridgeDictionary;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Infrastructure.Csv;
using VocabularySheet.Infrastructure.Csv.Interfaces;
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

        return services;
    }

    private static void AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<IGoogleSheetClient, GoogleSheetClient>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAppDbContext, AppDbContext>();

        services.AddScoped<IWordsRepository, WordsRepository>();
        services.AddScoped<ICambridgeRepository, CambridgeRepository>();

        services.AddConfigurationRepository<GoogleSheetConfig, GoogleSheetConfigurator>();
        services.AddConfigurationRepository<LocalizationConfig, LocalizationConfigurator>();
        
        
        services.AddSingleton<IGoogleSheetWordsRepository, GoogleSheetWordsRepository>();
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
        services.AddSingleton<IAppDataService, AppDataService>();
        services.AddSingleton<IGoogleSheetService, GoogleSheetService>();
    }

    private static void AddCsvSteamers(this IServiceCollection services)
    {
        services.AddSingleton(typeof(ICsvStreamer<,>), typeof(CsvStreamer<,>))
                .AddSingleton<ICsvWordStreamer, CsvWordStreamer>();
    }
    
    private static void AddConfigurationRepository<TEntity, TRepository>(this IServiceCollection services)
        where TEntity : BaseConfigurationEntity<TEntity>, new()
        where TRepository : BaseConfigurator<TEntity>
    {
        services.AddScoped<IConfigurator<TEntity>, TRepository>();
    }
}
