using Newtonsoft.Json;
using VocabularySheet.Infrastructure.Data.Interfaces;
using VocabularySheet.Infrastructure.Services.Interfaces;
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

    public JsonConfiguration JsonConfiguration
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
}
