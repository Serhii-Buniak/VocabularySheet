using MediatR;
using VocabularySheet.Application.Commons.Interfaces;

namespace VocabularySheet.Application.GoogleSheets.Queries;

public class GetGoogleSheetUrlQuery : IRequest<string>
{

    public class GetGoogleSheetUrlQueryHandler : IRequestHandler<GetGoogleSheetUrlQuery, string>
    {
        private readonly IGoogleSheetConfigurationRepository _configurationRepository;

        public GetGoogleSheetUrlQueryHandler(IGoogleSheetConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<string> Handle(GetGoogleSheetUrlQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_configurationRepository.GetGoogleSheetUrl()) ?? "https://docs.google.com/spreadsheets";
        }
    }

}
