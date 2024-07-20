using Application.Common.Commons.Dtos;
using Application.Common.Commons.Interfaces;

namespace Infrastructure.Data.Services.Interfaces;

public interface ITextToSpeechProvider
{
    TextToSpeechProviderId TextToSpeechId { get; }
    Task<TextToSpeechResult?> GetTextToSpeech(WordWithLanguage word, CancellationToken cancellationToken);
}