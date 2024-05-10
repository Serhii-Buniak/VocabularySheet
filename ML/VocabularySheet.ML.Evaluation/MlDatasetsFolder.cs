using VocabularySheet.Common;

namespace VocabularySheet.ML.Evaluation;

public record AppFileEntry
{
    public required string Name { get; init; }
    public required string Path { get; init; }
    
    public required string FullPath { get; init; }
    
    public required string Content { get; init; }
}

public record AppFolderEntry
{
    public required string Name { get; init; }
    public required string Path { get; init; }
    public required string FullPath { get; init; }
}

public interface IMlDatasetsFolder
{
    string FolderPath { get; }
    string SaveModelsPath { get; }
    Dictionary<string, AppFileEntry> GetFilesPath(string folderPath);
    Dictionary<string, AppFolderEntry> GetFoldersPath(string folderPath);
}

public class MlDatasetsFolder : IMlDatasetsFolder
{
    public required string FolderPath { get; init; }
    public required string SaveModelsPath { get; init; }
    
    public Dictionary<string, AppFileEntry> GetFilesPath(string folderPath)
    {
        string fullPath = Path.Combine(FolderPath, folderPath);
        if (!Directory.Exists(fullPath))
        {
            throw new DirectoryNotFoundException($"The folder '{folderPath}' does not exist.");
        }

        string[] files = Directory.GetFiles(fullPath);

        var mlFiles = new List<AppFileEntry>(files.Length);
        foreach (var file in files)
        {
            string path = file.Replace(FolderPath, string.Empty);
            
            mlFiles.Add(new AppFileEntry
            {
                Name = Path.GetFileName(file),
                Path = path,
                FullPath = file,
                Content = File.ReadAllText(file)
            });
        }
        
        return mlFiles.ToDictionary(f => f.Name);
    }
    
    public Dictionary<string, AppFolderEntry> GetFoldersPath(string folderPath)
    {
        string fullPath = Path.Combine(FolderPath, folderPath);
        
        if (!Directory.Exists(fullPath))
        {
            throw new DirectoryNotFoundException($"The folder '{folderPath}' does not exist.");
        }

        string[] directories = Directory.GetDirectories(fullPath);
        
        var mlFolders = new List<AppFolderEntry>(directories.Length);

        foreach (string directory in directories)
        {
            string path = directory.Replace(FolderPath, string.Empty);
            
            mlFolders.Add(new AppFolderEntry
            {
                Name = directory.Replace(fullPath, string.Empty).TrimStart(Path.DirectorySeparatorChar),
                Path = path.TrimStart(Path.DirectorySeparatorChar),
                FullPath = directory
            });
        }
        
        return mlFolders.ToDictionary(f => f.Name);

    }
}