using Application.Common.Commons.Interfaces;
using Domain.Common;

namespace Application.Common.GoogleSheets.Queries;

public static class GetGoogleSheetUrl
{
    public class Query : IRequest<string>
    {
        public class Handler : IRequestHandler<Query, string>
        {
            private readonly IConfigurator<GoogleSheetConfig> _configuration;

            public Handler(IConfigurator<GoogleSheetConfig> configuration)
            {
                _configuration = configuration;
            }

            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                var googleSheetConfiguration = await _configuration.Get(cancellationToken);
                return googleSheetConfiguration.SheetUrl;
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
