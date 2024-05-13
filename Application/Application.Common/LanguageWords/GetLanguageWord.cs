using Application.Common.Commons.Interfaces;
using Domain.Localization;

namespace Application.Common.LanguageWords;

public static class GetLanguageWord
{
    public class Query : IRequest<LocalizationConfig>
    {
        public class Handler : IRequestHandler<Query, LocalizationConfig>
        {
            private readonly IConfigurator<LocalizationConfig> _configuration;

            public Handler(IConfigurator<LocalizationConfig> configuration)
            {
                _configuration = configuration;
            }

            public async Task<LocalizationConfig> Handle(Query request, CancellationToken cancellationToken)
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
