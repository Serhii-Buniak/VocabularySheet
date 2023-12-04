using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Commons.Mappings;
using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Application.Words.Queries;

public static class GetSpinWord
{
    public class Query : IRequest<WordModel?>
    {
        public required long Id { get; init; }
        
        public class Handler : IRequestHandler<Query, WordModel?>
        {
            private readonly IWordsRepository _repository;
            private readonly IConfigurator<LocalizationConfig> _configuration;

            public Handler(IWordsRepository repository, IConfigurator<LocalizationConfig> configuration)
            {
                _repository = repository;
                _configuration = configuration;
            }

            public async Task<WordModel?> Handle(Query request, CancellationToken cancellationToken)
            {
                var languages = await _configuration.Get(cancellationToken);

                var word = await _repository.GetById(request.Id, cancellationToken);
                var index = await _repository.GetIndexOf(request.Id, cancellationToken);

                return word?.ToWordSpin(index ?? 0, languages.OriginLang, languages.TranslateLang);
            }
        }
    }

    public class Validation : AbstractValidator<Query>
    {
        
    }
}

