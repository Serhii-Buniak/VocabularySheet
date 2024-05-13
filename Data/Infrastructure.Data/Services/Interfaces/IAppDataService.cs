namespace Infrastructure.Data.Services.Interfaces;

public interface IAppDataService
{
    Stream GetJsonConfigurationFile(out bool wasCreated);
}