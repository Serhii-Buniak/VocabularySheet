using VocabularySheet.Common.Extensions;

namespace VocabularySheet.Common;

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

public static class AppFolder
{
    public static readonly string BasePath = AppDomain.CurrentDomain.AppPath();
    public static string CreatePath(string path) => Path.Combine(BasePath, path);
    
    public static AppFileEntry GetFilePath(string fileName)
    {
        string path = Path.Combine(BasePath, fileName);

        return new AppFileEntry
        {
            Name = fileName,
            Path = path.Replace(BasePath, string.Empty),
            FullPath = path,
            Content = File.ReadAllText(path)
        };
    }
    
    public static Dictionary<string, AppFileEntry> GetFilesPath(string folderPath, string? removeFromPath = null)
    {
        string fullPath = Path.Combine(BasePath, folderPath);
        if (!Directory.Exists(fullPath))
        {
            throw new DirectoryNotFoundException($"The folder '{folderPath}' does not exist.");
        }

        string[] files = Directory.GetFiles(fullPath);

        var mlFiles = new List<AppFileEntry>(files.Length);
        foreach (var file in files)
        {
            string path = file.Replace(BasePath, string.Empty);
            if (removeFromPath != null)
            {
                path = path.Replace(removeFromPath, string.Empty);
            }
            
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
    
    public static Dictionary<string, AppFolderEntry> GetFoldersPath(string folderPath, string? removeFromPath = null)
    {
        string fullPath = Path.Combine(BasePath, folderPath);
        if (!Directory.Exists(fullPath))
        {
            throw new DirectoryNotFoundException($"The folder '{folderPath}' does not exist.");
        }

        string[] directories = Directory.GetDirectories(fullPath);
        var mlFolders = new List<AppFolderEntry>(directories.Length);

        foreach (string directory in directories)
        {
            string path = directory.Replace(BasePath, string.Empty);
            if (removeFromPath != null)
            {
                path = path.Replace(removeFromPath, string.Empty);
            }
            
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