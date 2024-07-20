using Application.Common.Commons.Dtos;
using Application.Common.Words.Queries;
using Apps.MauiRunner.Common.Services.Transliterators;
using Domain.Localization;
using Infrastructure.Data.HttpClients;
using MediatR;
using Plugin.Maui.Audio;
using Tools.Common.Extensions;

namespace Apps.MauiRunner.Common.Services;

public class MauiTextToSpeechService
{
    private static CyrillicTransliterator CyrillicTransliterator { get; } = new CyrillicTransliterator();
    
    private readonly IAudioManager _audioManager;
    private readonly IMediator _mediator;
    private readonly StreamFetcherClient _fetcherClient;

    public MauiTextToSpeechService(IAudioManager audioManager, IMediator mediator, StreamFetcherClient fetcherClient)
    {
        _audioManager = audioManager;
        _mediator = mediator;
        _fetcherClient = fetcherClient;
    }
    
    public async Task RunVoice(WordWithLanguage word, CancellationToken cancellationToken)
    {
        var wasPlayed = await ProviderVoice(word, cancellationToken);

        if (!wasPlayed)
        {
            await CoreVoice(word, cancellationToken);
        }
    }

    public async Task<bool> RunLinkVoice(string audioLink, CancellationToken cancellationToken)
    {
        try
        {
            var audio = await _fetcherClient.Fetch(audioLink, cancellationToken);
            var player = _audioManager.CreateAsyncPlayer(audio);
            await player.PlayAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private async Task<bool> ProviderVoice(WordWithLanguage word, CancellationToken cancellationToken)
    {
        try
        {
            var textToSpeechResult = await _mediator.Send(new GetWordDetails.QueryTextToSpeech
            {
                Word = word
            }, cancellationToken);
        
            if (textToSpeechResult == null)
            {
                return false;
            }

            return await RunLinkVoice(textToSpeechResult.Url, cancellationToken);
        }
        catch
        {
            return false;
        }
    }

    private static readonly Dictionary<WordLanguage, string[]> LocalesLanguages = new()
    {
        [WordLanguage.Ua] = ["uk-UA", "uk"],
        [WordLanguage.Ru] = ["ru-UA", "ru-RU","ru"],
        [WordLanguage.En] = ["en-US", "en-GB", "en"]
    };
    
    private static async Task<bool> CoreVoice(WordWithLanguage word, CancellationToken cancellationToken)
    {
        try
        {
            string wordToSpeech = word.Word;
            string[] localeLangs = LocalesLanguages.GetValueOrDefault(word.Language, LocalesLanguages[WordLanguage.En]);

            var locales = (await TextToSpeech.GetLocalesAsync()).ToList();

            Locale? locale = null;
            foreach (var localeLang in localeLangs)
            {
                locale ??= locales.Where(l => l.Language.Equals(localeLang, StringComparison.InvariantCultureIgnoreCase)).Random();
            }
            foreach (var localeLang in localeLangs)
            {
                locale ??= locales.Where(l => l.Language.Contains(localeLang, StringComparison.InvariantCultureIgnoreCase)).Random();
            }

            if (locale == null)
            {
                wordToSpeech = CyrillicTransliterator.Transliterate(word);
            }

            foreach (var localeLang in LocalesLanguages[WordLanguage.En])
            {
                locale ??= locales.Where(l => l.Language.Equals(localeLang, StringComparison.InvariantCultureIgnoreCase)).Random();
            }
            foreach (var localeLang in LocalesLanguages[WordLanguage.En])
            {
                locale ??= locales.Where(l => l.Language.Contains(localeLang, StringComparison.InvariantCultureIgnoreCase)).Random();
            }

            locale ??= locales.Random();
            if (locale == null)
            {
                return false;
            }

            await TextToSpeech.SpeakAsync(wordToSpeech, new SpeechOptions()
            {
                Locale = locale,
            }, cancellationToken);

            return true;
        }
        catch
        {
            return false;
        }
    }
}