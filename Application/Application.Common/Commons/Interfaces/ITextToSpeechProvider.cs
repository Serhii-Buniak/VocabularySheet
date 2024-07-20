using Application.Common.Commons.Dtos;

namespace Application.Common.Commons.Interfaces;

public enum TextToSpeechProviderId
{
    Cambridge = 1,
}

public interface ITextToSpeechService
{
    public Task<TextToSpeechResult?> GetTextToSpeech(WordWithLanguage word, CancellationToken cancellationToken);
}

public record TextToSpeechResult(TextToSpeechProviderId Id)
{
    public required string Url { get; init; }
}