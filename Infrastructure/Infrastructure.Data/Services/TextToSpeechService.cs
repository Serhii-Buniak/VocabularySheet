using Application.Common.Commons.Dtos;
using Application.Common.Commons.Interfaces;
using Infrastructure.Data.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Services;

public class TextToSpeechService : ITextToSpeechService
{
    private readonly List<ITextToSpeechProvider> _providers;

    public TextToSpeechService(IServiceProvider serviceProvider)
    {
        _providers = serviceProvider.GetServices<ITextToSpeechProvider>().OrderBy(p => p.TextToSpeechId).ToList();
    }
    
    public async Task<TextToSpeechResult?> GetTextToSpeech(WordWithLanguage word, CancellationToken cancellationToken)
    {
        foreach (var provider in _providers)
        {
            var textToSpeechResult = await provider.GetTextToSpeech(word, cancellationToken);
            if (textToSpeechResult != null)
            {
                return textToSpeechResult;
            }
        }

        return null;
    }
}

public class WordDescriptionService : IWordDescriptionService
{
    private readonly List<IWordDescriptionProvider> _providers;

    public WordDescriptionService(IServiceProvider serviceProvider)
    {
        _providers = serviceProvider.GetServices<IWordDescriptionProvider>().OrderBy(p => p.WordDescriptionId).ToList();
    }
    
    public async Task<WordDescriptionResult?> GetWordDescription(WordWithLanguage word, CancellationToken cancellationToken)
    {
        foreach (var provider in _providers)
        {
            var wordDescription = await provider.GetWordDescription(word, cancellationToken);
            if (wordDescription != null)
            {
                return wordDescription;
            }
        }
        
        return null;
    }
}