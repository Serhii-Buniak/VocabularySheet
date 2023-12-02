using MediatR;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Application.GoogleSheets.Queries;

public static class GetGoogleSheetUrl
{
    public class Query : IRequest<string>
    {
        public class Handler : IRequestHandler<Query, string>
        {
            private readonly IConfigurationRepository<GoogleSheetConfigurationEntity> _configuration;

            public Handler(IConfigurationRepository<GoogleSheetConfigurationEntity> configuration)
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
