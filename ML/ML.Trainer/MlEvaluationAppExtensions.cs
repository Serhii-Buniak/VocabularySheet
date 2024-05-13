using Microsoft.Extensions.DependencyInjection;

namespace ML.Trainer;

public static class MlEvaluationAppExtensions
{
    public static void AddEvaluation<T>(this IServiceCollection serviceCollection, T datasetsFolder) where T : IMlDatasetsFolder
    {
        serviceCollection.AddSingleton<IWordEvaluationService, MlWordEvaluationService>();
        serviceCollection.AddSingleton<MlDataSets>();
        serviceCollection.AddSingleton<IMlDatasetsFolder>(datasetsFolder);
    }
}