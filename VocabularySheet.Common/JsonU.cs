using System.Text.Json;
using System.Text.Json.Serialization;

namespace VocabularySheet.Common;

public static class Json
{
    public static readonly JsonSerializerOptions Camel = new JsonSerializerOptions()
    {
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public static readonly JsonSerializerOptions Pretty = new JsonSerializerOptions()
    {
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        WriteIndented = true
    };

    public static string Serialize<T>(this JsonSerializerOptions options, T data)
    {
        return JsonSerializer.Serialize(data, options);
    }
    
    public static T? Deserialize<T>(this JsonSerializerOptions options, string jsonText)
    {
        return JsonSerializer.Deserialize<T>(jsonText, options);
    }
}