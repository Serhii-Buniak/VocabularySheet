namespace VocabularySheet.Common.Extensions;

public static class FilePathExtensions
{
    public static string AppPath(this AppDomain appDomain)
    {
        string path = appDomain.BaseDirectory;
        int index = path.IndexOf("VocabularySheet\\", StringComparison.OrdinalIgnoreCase);
            
        if (index != -1)
        {
            return path[..(index + "VocabularySheet\\".Length)];
        }
        else
        {
            throw new ArgumentException("The directory 'VocabularySheet\\' was not found in the provided path.");
        }

    }
}