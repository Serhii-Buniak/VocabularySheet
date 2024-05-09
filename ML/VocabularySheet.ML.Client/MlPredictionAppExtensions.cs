using Microsoft.Extensions.DependencyInjection;

namespace VocabularySheet.ML.Client;

public static class MlPredictionAppExtensions
{
    public static void AddPrediction(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IWordClassificationService, MlWordService>();
    }
    
    public static void AddPredictionTransient(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IWordClassificationService, MlWordService>();
    }
}