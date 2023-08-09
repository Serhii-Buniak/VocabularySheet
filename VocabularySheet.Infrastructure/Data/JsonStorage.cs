using Newtonsoft.Json;
using VocabularySheet.Application.Commons.Interfaces;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace VocabularySheet.Infrastructure.Data;

public class JsonStorage : IJsonStorage
{
    private readonly IAppDataService _appDataService;
    private readonly JsonSerializer serializer = new();

    public JsonStorage(IAppDataService appDataService)
    {
        _appDataService = appDataService;

    }

    public string? GoogleSheetUrl
    {
        get
        {
            return JsonConfigurationData.GoogleSheetUrl;
        }
        set
        {
            JsonConfiguration data = JsonConfigurationData;
            data.GoogleSheetUrl = value;
            JsonConfigurationData = data;
        }
    }

    private JsonConfiguration JsonConfigurationData
    {
        get
        {
            using Stream stream = _appDataService.GetJsonConfigurationFile(out bool _);
            using StreamReader streamReader = new(stream);
            using JsonReader jsonReader = new JsonTextReader(streamReader);

            return serializer.Deserialize<JsonConfiguration>(jsonReader) ?? new();
        }

        set
        {
            using Stream stream = _appDataService.GetJsonConfigurationFile(out bool _);
            using StreamWriter streamWriter = new(stream);
            using JsonWriter jsonWriter = new JsonTextWriter(streamWriter);

            serializer.Serialize(jsonWriter, value);
        }
    }

    private class JsonConfiguration
    {
        public string? GoogleSheetUrl { get; set; }
    }

}
