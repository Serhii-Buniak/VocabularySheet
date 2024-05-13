using System.Text.Json;
using Tools.Parsers;

namespace Domain.Common;

public interface IEntity<out TId> where TId : notnull
{
    TId Id { get; }
}


public interface ILongEntity : IEntity<long>;


public enum ConfigType
{
    GoogleSheet = 0,
    VocabularyLocalization = 1,
}

public record ConfigEntry : IEntity<ConfigType>
{
    public required ConfigType Id { get; init; }
    public required string JsonData { get; set; }
}


public abstract record BaseConfigurationEntity<T>
{
    public abstract ConfigType Type { get; }
    
    public string ToJsonString()
    {
        if (this is T obj)
        {
            return JsonParser.Default.Serialize(obj);
        }
        
        return JsonParser.Default.Serialize(this);
    }
    
    public static T? Create(string json)
    {
        return JsonParser.Default.Deserialize<T>(json);
    }
}

public record GoogleSheetConfig : BaseConfigurationEntity<GoogleSheetConfig>
{
    public override ConfigType Type => ConfigType.GoogleSheet;
    
    public string SheetUrl { get; set; } = string.Empty;
    public string ScriptUrl { get; set; } = string.Empty;
}