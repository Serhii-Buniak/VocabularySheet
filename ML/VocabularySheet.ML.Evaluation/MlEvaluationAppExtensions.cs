using Microsoft.Extensions.DependencyInjection;

namespace VocabularySheet.ML.Evaluation;

public static class MlEvaluationAppExtensions
{
    public static void AddEvaluation(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IWordEvaluationService, MlWordEvaluationService>();
    }
}