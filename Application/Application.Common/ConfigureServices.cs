using System.Reflection;
using Application.Common.Commons.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ConfigureServices).GetTypeInfo().Assembly;

        services.AddMediator(applicationAssembly);

        services.AddValidation(applicationAssembly);

        services.AddLogger();

        return services;
    }

    private static void AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

    }

    private static void AddValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);
    }

    private static void AddLogger(this IServiceCollection services)
    {
        services.AddLogging();
    }
}