namespace VocabularySheet.Application.Commons.Interfaces;

public interface IAppDataService
{
    Stream GetJsonConfigurationFile(out bool wasCreated);
}