using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Commons.Mappings;
using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Application.Words.Queries;

public static class GetAllWords
{
    public class Query : IRequest<List<WordSpinDto>>
    {
        public class Handler : IRequestHandler<Query, List<WordSpinDto>>
        {
            private readonly IWordsRepository _repository;
            private readonly IConfigurationRepository<LocalizationConfigurationEntity> _configuration;

            public Handler(IWordsRepository repository, IConfigurationRepository<LocalizationConfigurationEntity> configuration)
            {
                _repository = repository;
                _configuration = configuration;
            }

            public async Task<List<WordSpinDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var localization = await _configuration.Get(cancellationToken);
                IEnumerable<Word> words = await _repository.GetAllAsync(cancellationToken);

                return words
                    .ToWordsSpin(localization.OriginLang, localization.TranslateLang)
                    .ToList();
            }
        }
    }

    public class Validation : AbstractValidator<Query>
    {
        public Validation()
        {

        }
    }
}

