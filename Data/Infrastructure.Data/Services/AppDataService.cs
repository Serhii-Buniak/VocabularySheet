using Infrastructure.Data;
using Infrastructure.Data.Services.Interfaces;

namespace VocabularySheet.Infrastructure.Services;

public class AppDataService : IAppDataService
{
    public const string JsonConfigurationFileName = "jsonconfiguration.json";
    private readonly InfrastructureOptions _options;

    public AppDataService(InfrastructureOptions options)
    {
        _options = options;
    }

    public Stream GetJsonConfigurationFile(out bool wasCreated)
    {
        string targetFile = Path.Combine(_options.DataDirectory, JsonConfigurationFileName);

        if (File.Exists(targetFile))
        {
            FileStream openedFile = File.Open(targetFile, FileMode.Open, FileAccess.ReadWrite);
            wasCreated = false;
            return openedFile;
        }
        else
        {
            FileStream createdFile = File.Create(targetFile);
            wasCreated = true;
            return createdFile;
        }
    }
}
