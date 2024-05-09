using VocabularySheet.Common.Extensions;

namespace VocabularySheet.MLApp;

internal record MlFileEntry
{
    public required string Name { get; init; }
    public required string Path { get; init; }
    
    public required string FullPath { get; init; }
    
    public required string Content { get; init; }
}

internal record MlFolderEntry
{
    public required string Name { get; init; }
    public required string Path { get; init; }
    public required string FullPath { get; init; }
}

internal static class MlFolder
{
    // VocabularySheet\ML\VocabularySheet.MLApp\DataSets
    private static readonly string BaseFolderPath = Path.Combine(AppDomain.CurrentDomain.AppPath(), "ML", "VocabularySheet.MLApp", "mldata", "DataSets");
    public static readonly string ModelsPath = Path.Combine(AppDomain.CurrentDomain.AppPath(), "ML", "VocabularySheet.MLApp", "mldata", "MLModels");
    public static string CreateModelsPath(string file) => Path.Combine(ModelsPath, file);
    
    public static MlFileEntry GetFilePath(string fileName)
    {
        string path = Path.Combine(BaseFolderPath, fileName);

        return new MlFileEntry
        {
            Name = fileName,
            Path = path.Replace(BaseFolderPath, string.Empty),
            FullPath = path,
            Content = File.ReadAllText(path)
        };
    }
    
    public static Dictionary<string, MlFileEntry> GetFilesPath(string folderPath)
    {
        string fullPath = Path.Combine(BaseFolderPath, folderPath);
        if (!Directory.Exists(fullPath))
        {
            throw new DirectoryNotFoundException($"The folder '{folderPath}' does not exist.");
        }

        string[] files = Directory.GetFiles(fullPath);

        var mlFiles = new List<MlFileEntry>(files.Length);
        foreach (var file in files)
        {
            mlFiles.Add(new MlFileEntry
            {
                Name = Path.GetFileName(file),
                Path = file.Replace(BaseFolderPath, string.Empty),
                FullPath = file,
                Content = File.ReadAllText(file)
            });
        }
        
        return mlFiles.ToDictionary(f => f.Name);
    }
    
    public static Dictionary<string, MlFolderEntry> GetFoldersPath(string folderPath)
    {
        string fullPath = Path.Combine(BaseFolderPath, folderPath);
        if (!Directory.Exists(fullPath))
        {
            throw new DirectoryNotFoundException($"The folder '{folderPath}' does not exist.");
        }

        string[] directories = Directory.GetDirectories(fullPath);
        var mlFolders = new List<MlFolderEntry>(directories.Length);

        foreach (string directory in directories)
        {
            mlFolders.Add(new MlFolderEntry
            {
                Name = directory.Replace(fullPath, string.Empty).TrimStart(Path.DirectorySeparatorChar),
                Path = directory.Replace(BaseFolderPath, string.Empty).TrimStart(Path.DirectorySeparatorChar),
                FullPath = directory
            });
        }
        
        return mlFolders.ToDictionary(f => f.Name);
    }
}
