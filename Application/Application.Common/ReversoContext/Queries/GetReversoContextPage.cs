using Application.Common.Commons.Interfaces;
using Domain.Localization;
using Domain.WebSources;
using Domain.WordModels;

namespace Application.Common.ReversoContext.Queries;

public static class GetReversoContextPage
{
    public abstract class BaseHandler
    {
        protected readonly IReversoContextRepository repository;
        protected readonly IConfigurator<LocalizationConfig> configurator;
        
        protected BaseHandler(IReversoContextRepository repository, IConfigurator<LocalizationConfig> configurator)
        {
            this.repository = repository;
            this.configurator = configurator;
        }
        
        protected async Task<PublicReversoContextEntry?> BaseSearch(string word, CancellationToken cancellationToken)
        {
            var languages = await configurator.Get(cancellationToken);
            return await repository.Get(word, languages.OriginLang, languages.TranslateLang, cancellationToken);
        }

    }
    
    public record Query : IRequest<PublicReversoContextEntry?>
    {
        public required IWord Word { get; init; }
        
        public class Handler : BaseHandler, IRequestHandler<Query, PublicReversoContextEntry?>
        {
            public Handler(IReversoContextRepository repository, IConfigurator<LocalizationConfig> configurator) : base(repository, configurator)
            {
            }

            public async Task<PublicReversoContextEntry?> Handle(Query request, CancellationToken cancellationToken)
            {
                return await BaseSearch(request.Word.Original, cancellationToken);
            }
        }
    }
    
    public record QuerySimple : IRequest<PublicReversoContextEntry?>
    {
        public required string Word { get; init; }
        
        public class Handler : BaseHandler, IRequestHandler<QuerySimple, PublicReversoContextEntry?>
        {
            public Handler(IReversoContextRepository repository, IConfigurator<LocalizationConfig> configurator) : base(repository, configurator)
            {
            }

            public async Task<PublicReversoContextEntry?> Handle(QuerySimple request, CancellationToken cancellationToken)
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
