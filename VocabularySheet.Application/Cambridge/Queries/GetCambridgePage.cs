using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Domain.Pages;

namespace VocabularySheet.Application.Cambridge.Queries;

public static class GetCambridgePage
{
    
    public abstract class BaseHandler
    {
        protected readonly ICambridgeRepository repository;
        protected readonly IConfigurator<LocalizationConfig> configurator;
        
        protected BaseHandler(ICambridgeRepository repository, IConfigurator<LocalizationConfig> configurator)
        {
            this.repository = repository;
            this.configurator = configurator;
        }
        
        protected async Task<Dictionary<WordLanguage, PublicCambridgeEntry>> BaseSearch(string word, CancellationToken cancellationToken)
        {
            var languages = (await configurator.Get(cancellationToken)).SetOfLang();
            var dictionary = new Dictionary<WordLanguage, PublicCambridgeEntry>();

            foreach (var language in languages)
            {
                var entry = await repository.Get(word, language, cancellationToken);
                if (entry != null)
                {
                    dictionary.Add(language, entry);
                }
            }

            return dictionary;
        }

    }
    
    public record Query : IRequest<Dictionary<WordLanguage, PublicCambridgeEntry>>
    {
        public required IWord Word { get; init; }
        
        public class Handler : BaseHandler, IRequestHandler<Query, Dictionary<WordLanguage, PublicCambridgeEntry>>
        {
            public Handler(ICambridgeRepository repository, IConfigurator<LocalizationConfig> configurator) : base(repository, configurator)
            {
            }

            public async Task<Dictionary<WordLanguage, PublicCambridgeEntry>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await BaseSearch(request.Word.Original, cancellationToken);
            }
        }
    }
    
    public record QuerySimple : IRequest<Dictionary<WordLanguage, PublicCambridgeEntry>>
    {
        public required string Word { get; init; }
        
        public class Handler : BaseHandler, IRequestHandler<QuerySimple, Dictionary<WordLanguage, PublicCambridgeEntry>>
        {
            public Handler(ICambridgeRepository repository, IConfigurator<LocalizationConfig> configurator) : base(repository, configurator)
            {
            }

            public async Task<Dictionary<WordLanguage, PublicCambridgeEntry>> Handle(QuerySimple request, CancellationToken cancellationToken)
            {
                return await BaseSearch(request.Word, cancellationToken);
            }

        }
    }

    public class Validation : AbstractValidator<Query>
    {
        public Validation()
        {
            RuleFor(q => q.Word.Original).NotEmpty();
        }
    }
    
    public class ValidationSimple : AbstractValidator<QuerySimple>
    {
        public ValidationSimple()
        {
            RuleFor(q => q.Word).NotEmpty();
        }
    }
}
