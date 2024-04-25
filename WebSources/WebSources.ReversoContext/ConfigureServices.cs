using Microsoft.Extensions.DependencyInjection;

namespace WebSources.ReversoContext;

public static class ConfigureServices
{
    public static IServiceCollection AddReversoContext(this IServiceCollection services)
    {
        services.AddHttpClient<ReversoContextClient>();
        services.AddSingleton<ReversoContextParser>();
        services.AddSingleton<ReversoContextPageBuilder>();
        
        return services;
    }
}