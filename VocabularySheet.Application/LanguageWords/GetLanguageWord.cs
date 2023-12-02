using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Application.LanguageWords;

public static class GetLanguageWord
{
    public class Query : IRequest<LocalizationConfigurationEntity>
    {
        public class Handler : IRequestHandler<Query, LocalizationConfigurationEntity>
        {
            private readonly IConfigurationRepository<LocalizationConfigurationEntity> _configuration;

            public Handler(IConfigurationRepository<LocalizationConfigurationEntity> configuration)
            {
                _configuration = configuration;
            }

            public async Task<LocalizationConfigurationEntity> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _configuration.Get(cancellationToken);
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
