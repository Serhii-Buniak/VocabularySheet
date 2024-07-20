using VocabularySheet.ML.Client;

namespace Apps.MauiRunner.Common.Services;

public class MlModelsFolder : IMlModelsFolder
{
    public string CreatePath(string path) => Path.Combine("MlModels", path);
    
    public Task<Stream> GetModel(string path)
    {
        path = CreatePath(path);
        return FileSystem.OpenAppPackageFileAsync(path);
    }
}