using Microsoft.Extensions.DependencyInjection;

namespace VocabularySheet.CambridgeDictionary;

public static class ConfigureServices
{
    public static IServiceCollection AddCambridgeDictionary(this IServiceCollection services)
    {
        AddCambridge(services);
        
        return services;
    }

    private static void AddCambridge(this IServiceCollection services)
    {
        services.AddHttpClient<CambridgeClient>();
        services.AddSingleton<CambridgeParser>();
        services.AddSingleton<CabridgePageBuilder>();
    }
}