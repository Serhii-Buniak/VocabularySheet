using VocabularySheet.Common;

namespace VocabularySheet.ML.Evaluation;

public static class MlDatasetsFolder
{
    public static readonly string DatasetsPath = Path.Combine("ML", "VocabularySheet.ML.Evaluation", "datasets");
    public static string CreatePath(string path) => Path.Combine(DatasetsPath, path);

    internal static Dictionary<string, AppFileEntry> GetFilesPath(string folderPath)
    {
        string path = Path.Combine(DatasetsPath, folderPath);
        return AppFolder.GetFilesPath(path, DatasetsPath);
    }
    
    internal static Dictionary<string, AppFolderEntry> GetFoldersPath(string folderPath)
    {
        string path = Path.Combine(DatasetsPath, folderPath);
        return AppFolder.GetFoldersPath(path, DatasetsPath);
    }
}