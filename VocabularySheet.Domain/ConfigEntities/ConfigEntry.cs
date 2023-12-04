using System.Text.Json;

namespace VocabularySheet.Domain.ConfigEntities;

public record ConfigEntry : IEntity<ConfigType>
{
    public required ConfigType Id { get; init; }
    public required string JsonData { get; set; }
}

public enum ConfigType
{
    GoogleSheet = 0,
    VocabularyLocalization = 1,
}

public abstract record BaseConfigurationEntity<T>
{
    public abstract ConfigType Type { get; }
    
    public string ToJsonString()
    {
        if (this is T obj)
        {
            return JsonSerializer.Serialize(obj);
        }
        
        return JsonSerializer.Serialize(this);
    }
    
    public static T? Create(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
}