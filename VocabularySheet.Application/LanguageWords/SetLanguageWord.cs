using VocabularySheet.Common;
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
            private readonly IConfigurator<LocalizationConfig> _configuration;

            public Handler(IConfigurator<LocalizationConfig> configuration)
            {
                _configuration = configuration;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _configuration.Set(new LocalizationConfig()
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
