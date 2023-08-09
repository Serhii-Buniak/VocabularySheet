using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace VocabularySheet.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        // services.AddMediatR(Assembly.GetExecutingAssembly());

        var applicationAssembly = typeof(ConfigureServices).GetTypeInfo().Assembly;

        services.AddMediatR(cfg =>
             cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddFluentValidation(new[] { applicationAssembly });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}