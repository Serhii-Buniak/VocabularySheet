using Application.Common.Commons.Dtos;
using Application.Common.Commons.Interfaces;
using Application.Common.Commons.Mappings;
using Domain.Localization;

namespace Application.Common.Words.Queries;

public static class GetSpinWord
{
    public class QueryId : IRequest<WordModel?>
    {
        public required long Id { get; init; }
        
        public class Handler : IRequestHandler<QueryId, WordModel?>
        {
            private readonly IWordsRepository _repository;
            private readonly IConfigurator<LocalizationConfig> _configuration;

            public Handler(IWordsRepository repository, IConfigurator<LocalizationConfig> configuration)
            {
                _repository = repository;
                _configuration = configuration;
            }

            public async Task<WordModel?> Handle(QueryId request, CancellationToken cancellationToken)
            {
                var languages = await _configuration.Get(cancellationToken);

                var word = await _repository.GetById(request.Id, cancellationToken);
                var index = await _repository.GetIndexOf(request.Id, cancellationToken);

                return word?.ToWordSpin(index ?? 0, languages.OriginLang, languages.TranslateLang);
            }
        }
    }
    
    public class QueryName : IRequest<WordModel?>
    {
        public required string Word { get; init; }
        
        public class Handler : IRequestHandler<QueryName, WordModel?>
        {
            private readonly IWordsRepository _repository;
            private readonly IConfigurator<LocalizationConfig> _configuration;

            public Handler(IWordsRepository repository, IConfigurator<LocalizationConfig> configuration)
            {
                _repository = repository;
                _configuration = configuration;
            }

            public async Task<WordModel?> Handle(QueryName request, CancellationToken cancellationToken)
            {
                var languages = await _configuration.Get(cancellationToken);

                var word = await _repository.GetByName(request.Word, cancellationToken);
                int index = 0;
                if (word != null)
                {
                    index = await _repository.GetIndexOf(word.Id, cancellationToken) ?? 0;
                }

                return word?.ToWordSpin(index, languages.OriginLang, languages.TranslateLang);
            }
        }
    }

    public class ValidationId : AbstractValidator<QueryId>
    {
        
    }
    
    public class ValidationName : AbstractValidator<QueryName>
    {
        
    }
}

