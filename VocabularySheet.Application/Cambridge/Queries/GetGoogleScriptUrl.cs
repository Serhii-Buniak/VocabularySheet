using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Domain.Pages;

namespace VocabularySheet.Application.Cambridge.Queries;

public static class GetCambridgePage
{
    public record Query : IRequest<Dictionary<WordLanguage, PublicCambridgeEntry>>
    {
        public required IWord Word { get; init; }
        
        public class Handler : IRequestHandler<Query, Dictionary<WordLanguage, PublicCambridgeEntry>>
        {
            private readonly ICambridgeRepository _repository;
            private readonly IConfigurator<LocalizationConfig> _configurator;

            public Handler(ICambridgeRepository repository, IConfigurator<LocalizationConfig> configurator)
            {
                _repository = repository;
                _configurator = configurator;
            }

            public async Task<Dictionary<WordLanguage, PublicCambridgeEntry>> Handle(Query request, CancellationToken cancellationToken)
            {
                var languages = (await _configurator.Get(cancellationToken)).SetOfLang();
                var dictionary = new Dictionary<WordLanguage, PublicCambridgeEntry>();

                foreach (var language in languages)
                {
                    var entry = await _repository.Get(request.Word.Original, language, cancellationToken);
                    if (entry != null)
                    {
                        dictionary.Add(language, entry);
                    }
                }

                return dictionary;
            }
        }
    }

    public class Validation : AbstractValidator<Query>
    {
        public Validation()
        {
            RuleFor(q => q.Word).NotEmpty();
        }
    }
}
