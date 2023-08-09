using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Infrastructure.Data;
using VocabularySheet.Infrastructure.Data.Csv;
using VocabularySheet.Infrastructure.Data.Csv.Interfaces;
using VocabularySheet.Infrastructure.Services;

namespace VocabularySheet.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, Options options)
    {
        services.AddDbContext<AppDbContext>(o
            => o.UseSqlite($"Filename={Path.Combine(options.DataDirectory, "words.db")}", x
            => x.MigrationsAssembly(nameof(Infrastructure))
            )
        );

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton<IMapperService, MapperService>();

        services.AddSingleton(typeof(ICsvStreamer<,>), typeof(CsvStreamer<,>))
                .AddSingleton<ICsvWordStreamer, CsvWordStreamer>();

        return services;
    }

    public class Options
    {
        public string DataDirectory { get; set; } = null!;
    }
}