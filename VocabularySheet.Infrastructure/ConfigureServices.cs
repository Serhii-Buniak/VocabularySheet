using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Infrastructure.Data;
using VocabularySheet.Infrastructure.Data.Csv;
using VocabularySheet.Infrastructure.Data.Csv.Interfaces;
using VocabularySheet.Infrastructure.HttpClients;
using VocabularySheet.Infrastructure.Services;

namespace VocabularySheet.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, InfrastructureOptions options)
    {
        services.AddSingleton(options);

        services.AddSingleton<JsonStorage>();
        services.AddDbContext<AppDbContext>(o
            => o.UseSqlite($"Filename={Path.Combine(options.DataDirectory, "words.db")}", x
            => x.MigrationsAssembly("VocabularySheet.Infrastructure")
            )
        );

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddHttpClient<IGoogleSheetClient, GoogleSheetClient>();

        services.AddSingleton<IMapperService, MapperService>();
        services.AddScoped<IAppDbContext, AppDbContext>();

        services.AddSingleton(typeof(ICsvStreamer<,>), typeof(CsvStreamer<,>))
                .AddSingleton<ICsvWordStreamer, CsvWordStreamer>();

        return services;
    }

    public record InfrastructureOptions
    {
        public string DataDirectory { get; set; } = null!;
    }
}