using Application.Common.Commons.Dtos;
using Application.Common.Commons.Interfaces;
using Domain.Localization;

namespace Application.Common.Words.Queries;

public static class GetWordDetails
{
    public class QueryTextToSpeech : IRequest<TextToSpeechResult?>
    {
        public required WordWithLanguage Word { get; init; }
        
        public class Handler : IRequestHandler<QueryTextToSpeech, TextToSpeechResult?>
        {
            private readonly ITextToSpeechService _textToSpeechService;
            
            public Handler(ITextToSpeechService textToSpeechService)
            {
                _textToSpeechService = textToSpeechService;
            }
            
            public async Task<TextToSpeechResult?> Handle(QueryTextToSpeech request, CancellationToken cancellationToken)
            {
                return await _textToSpeechService.GetTextToSpeech(request.Word, cancellationToken);
            }
        }
    }

    public class ValidationTextToSpeech : AbstractValidator<QueryTextToSpeech>
    {
        public ValidationTextToSpeech()
        {

        }
    }
    
    public class QueryWordDescription : IRequest<WordDescriptionResult?>
    {
        public required long Id { get; init; }
        
        public class Handler : IRequestHandler<QueryWordDescription, WordDescriptionResult?>
        {
            private readonly IWordsRepository _repository;
            private readonly IConfigurator<LocalizationConfig> _configuration;
            private readonly IWordDescriptionService _wordDescriptionService;

            public Handler(IWordsRepository repository, IConfigurator<LocalizationConfig> configuration, IWordDescriptionService wordDescriptionService)
            {
                _repository = repository;
                _configuration = configuration;
                _wordDescriptionService = wordDescriptionService;
            }

            public async Task<WordDescriptionResult?> Handle(QueryWordDescription request, CancellationToken cancellationToken)
            {
                var config = await _configuration.Get(cancellationToken);
                var word = await _repository.GetById(request.Id, cancellationToken);
                if (word == null)
                {
                    return null;
                }

                return await _wordDescriptionService.GetWordDescription(new WordWithLanguage(word.Original, config.OriginLang), cancellationToken);
            }
        }
    }

    public class ValidationWordDescription : AbstractValidator<QueryWordDescription>
    {
        public ValidationWordDescription()
        {

        }
    }
}