using MediatR;
using VocabularySheet.Application.Commons.Interfaces;

namespace VocabularySheet.Application.GoogleSheets.Queries;

public class GetGoogleSheetUrl
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
                return await Task.FromResult(_configurationRepository.GetGoogleSheetUrl());
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
