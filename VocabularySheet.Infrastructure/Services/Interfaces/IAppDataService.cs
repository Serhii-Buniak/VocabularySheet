namespace VocabularySheet.Infrastructure.Services.Interfaces;

public interface IAppDataService
{
    Stream GetJsonConfigurationFile(out bool wasCreated);
}