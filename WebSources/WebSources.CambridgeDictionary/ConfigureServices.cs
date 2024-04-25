using Microsoft.Extensions.DependencyInjection;

namespace WebSources.CambridgeDictionary;

public static class ConfigureServices
{
    public static IServiceCollection AddCambridgeDictionary(this IServiceCollection services)
    {
        services.AddHttpClient<CambridgeClient>();
        services.AddSingleton<CambridgeParser>();
        services.AddSingleton<CabridgePageBuilder>();
        
        return services;
    }
}