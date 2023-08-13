using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Infrastructure.Data;
using VocabularySheet.Infrastructure.Data.Interfaces;

namespace VocabularySheet.Infrastructure.Repositories;

public class GoogleSheetConfigurationRepository : IGoogleSheetConfigurationRepository
{
    private readonly IJsonStorage _jsonStorage;

    public GoogleSheetConfigurationRepository(IJsonStorage jsonStorage)
    {
        _jsonStorage = jsonStorage;
    }

    public string GetGoogleSheetUrl()
    {
        return _jsonStorage.JsonConfiguration.GoogleSheetUrl;
    }  
    
    public string GetGoogleScriptUrl()
    {
        return _jsonStorage.JsonConfiguration.GoogleScriptUrl;
    }

    public void SetGoogleScriptUrl(string value)
    {
        JsonConfiguration data = _jsonStorage.JsonConfiguration;
        data.GoogleScriptUrl = value;
        _jsonStorage.JsonConfiguration = data;
    }

    public void SetGoogleSheetUrl(string value)
    {
        JsonConfiguration data = _jsonStorage.JsonConfiguration;
        data.GoogleSheetUrl = value;
        _jsonStorage.JsonConfiguration = data;
    }
}
