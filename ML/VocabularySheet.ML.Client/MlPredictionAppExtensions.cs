using Microsoft.Extensions.DependencyInjection;

namespace VocabularySheet.ML.Client;

public static class MlModelConstants
{
    public const string WordArticlesFileName = "wordArticles.zip"; 
}
public interface IMlModelsFolder
{
    Task<Stream> GetModel(string path);
}

public static class MlPredictionAppExtensions
{
    public static void AddPrediction<T>(this IServiceCollection serviceCollection, T folder) where T : IMlModelsFolder
    {
        serviceCollection.AddSingleton<IWordClassificationService, MlWordService>();
        serviceCollection.AddSingleton<IMlModelsFolder>(folder);

    }
    
    public static void AddPredictionTransient<T>(this IServiceCollection serviceCollection, T folder) where T : IMlModelsFolder
    {
        serviceCollection.AddTransient<IWordClassificationService, MlWordService>();
        serviceCollection.AddSingleton<IMlModelsFolder>(folder);
    }
}