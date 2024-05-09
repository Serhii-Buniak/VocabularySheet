namespace VocabularySheet.ML.Evaluation.App.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
