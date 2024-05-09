using System.Text.Json;

namespace VocabularySheet.Common;

public static class Json
{
    public static readonly JsonSerializerOptions Pretty = new JsonSerializerOptions()
    {
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