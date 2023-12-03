using Microsoft.Extensions.DependencyInjection;

namespace VocabularySheet.CambridgeDictionary;

public static class ConfigureServices
{
    public static IServiceCollection AddCambridgeDictionary(this IServiceCollection services)
    {
        AddHttpClients(services);
        
        services.AddSingleton<CambridgeParser>();

        return services;
    }

    private static void AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<CambridgeClient>();
    }
}