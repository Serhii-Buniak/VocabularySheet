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

    string SaveAndGetPath(string fileName, string content);
    AppFileEntry GetFilePath(string filePath);
    Dictionary<string, AppFileEntry> GetFilesPath(string folderPath);
    Dictionary<string, AppFolderEntry> GetFoldersPath(string folderPath);
}

public class MlDatasetsFolder : IMlDatasetsFolder
{
    public required string FolderPath { get; init; }
    public required string SaveModelsPath { get; init; }

    public string SaveAndGetPath(string fileName, string content)
    {
        const string crutches = "crutches";
        string fullPath = Path.Combine(FolderPath, crutches);

        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
        }
        
        string filePath = Path.Combine(fullPath, fileName);
        
        // Write the content to the file (overwrite if exists)
        File.WriteAllText(filePath, content);

        return filePath;
    }
    
    public AppFileEntry GetFilePath(string filePath)
    {
        string fullPath = Path.Combine(FolderPath, filePath);
        if (!File.Exists(fullPath))
        {
            throw new DirectoryNotFoundException($"The file '{filePath}' does not exist.");
        }
        
        string path = fullPath.Replace(FolderPath, string.Empty);

        return new AppFileEntry
        {
            Name = Path.GetFileName(fullPath),
            Path = path,
            FullPath = fullPath,
            Content = File.ReadAllText(fullPath)
        };
    }
    
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