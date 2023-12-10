using VocabularySheet.Parsing.Common;

namespace VocabularySheet.CambridgeDictionary.Entities;

public record CambridgeAudio
{
    public string? LanguageCode { get; init; }
    public string? Transcription { get; init; }
    public List<CambridgeAudioLink> Links { get; init; } = new();
}

public record CambridgeAudioLink : IHaveAudioLink
{
    public required string Type { get; init; }
    public required string Src { get; init; }

    public string FullLink()
    {
        return CambridgeClient.Base + Src;
    }
}
