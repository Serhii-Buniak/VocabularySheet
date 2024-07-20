using Application.Common.Commons.Interfaces;
using Domain.Common;

namespace Application.Common.GoogleSheets.Queries;

public static class GetGoogleSheetConfig
{
    public class Query : IRequest<GoogleSheetConfig>
    {
        public class Handler : IRequestHandler<Query, GoogleSheetConfig>
        {
            private readonly IConfigurator<GoogleSheetConfig> _configuration;

            public Handler(IConfigurator<GoogleSheetConfig> configuration)
            {
                _configuration = configuration;
            }

            public async Task<GoogleSheetConfig> Handle(Query request, CancellationToken cancellationToken)
            {
                var googleSheetConfiguration = await _configuration.Get(cancellationToken);
                return googleSheetConfiguration;
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
