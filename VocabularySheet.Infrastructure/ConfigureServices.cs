using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Infrastructure.Csv;
using VocabularySheet.Infrastructure.Csv.Interfaces;
using VocabularySheet.Infrastructure.Data;
using VocabularySheet.Infrastructure.Data.Interfaces;
using VocabularySheet.Infrastructure.HttpClients;
using VocabularySheet.Infrastructure.HttpClients.Interfaces;
using VocabularySheet.Infrastructure.Repositories;
using VocabularySheet.Infrastructure.Repositories.Interfaces;
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
        services.AddJsonStorage();

        services.AddHttpClients();

        services.AddServices();

        services.AddCsvSteamers();

        services.AddMapper();

        return services;
    }

    private static void AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<IGoogleSheetClient, GoogleSheetClient>();
    }

    private static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAppDbContext, AppDbContext>();

        services.AddScoped<IWordsRepository, WordsRepository>();

        services.AddSingleton<IGoogleSheetConfigurationRepository, GoogleSheetConfigurationRepository>();
        services.AddSingleton<IGoogleSheetWordsRepository, GoogleSheetWordsRepository>();
    }

    private static void AddDatabase(this IServiceCollection services, InfrastructureOptions options)
    {
        services.AddDbContext<AppDbContext>(o
            => o.UseSqlite($"Filename={Path.Combine(options.DataDirectory, "words.db")}", x
            => x.MigrationsAssembly("VocabularySheet.Infrastructure")
            )
        );
    }

    private static void AddJsonStorage(this IServiceCollection services)
    {
        services.AddSingleton<IJsonStorage, JsonStorage>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IMapperService, MapperService>();
        services.AddSingleton<IAppDataService, AppDataService>();
        services.AddSingleton<IGoogleSheetService, GoogleSheetService>();
    }

    private static void AddCsvSteamers(this IServiceCollection services)
    {
        services.AddSingleton(typeof(ICsvStreamer<,>), typeof(CsvStreamer<,>))
                .AddSingleton<ICsvWordStreamer, CsvWordStreamer>();
    }
}
