using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Application.LanguageWords;

public static class SetLanguageWord
{
    public class Command : IRequest
    {
        public required WordLanguage WordLang { get; init; }
        public required WordLanguage TranslateLang { get; init; }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IConfigurationRepository<LocalizationConfigurationEntity> _configuration;

            public Handler(IConfigurationRepository<LocalizationConfigurationEntity> configuration)
            {
                _configuration = configuration;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _configuration.Set(new LocalizationConfigurationEntity()
                {
                    OriginLang = request.WordLang,
                    TranslateLang = request.TranslateLang,
                } , cancellationToken);
            }
        }
    }

    public class Validation : AbstractValidator<Command>
    {
        public Validation()
        {
        }
    }
}
