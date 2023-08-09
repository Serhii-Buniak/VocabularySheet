using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace VocabularySheet.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ConfigureServices).GetTypeInfo().Assembly;

        services.AddMediatR(cfg =>
             cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}