using VocabularySheet.Common;

namespace VocabularySheet.ML.Client;

public static class MlModelsFolder
{
    public static readonly string ModelsPath = Path.Combine("ML", "VocabularySheet.ML.Client", "models");
    public static string CreatePath(string path) => AppFolder.CreatePath(Path.Combine(ModelsPath, path));
}