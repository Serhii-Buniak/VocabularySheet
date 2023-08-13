namespace VocabularySheet.Application.GoogleSheets.Queries;

public static class GetGoogleScriptUrl
{
    public class Query : IRequest<string>
    {
        public class Handler : IRequestHandler<Query, string>
        {
            private readonly IGoogleSheetConfigurationRepository _configurationRepository;

            public Handler(IGoogleSheetConfigurationRepository configurationRepository)
            {
                _configurationRepository = configurationRepository;
            }

            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(_configurationRepository.GetGoogleScriptUrl());
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
