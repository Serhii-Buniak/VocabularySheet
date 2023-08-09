using MediatR;
using VocabularySheet.Application.Commons.Interfaces;

namespace VocabularySheet.Application.GoogleSheets.Queries;

public class GetGoogleSheetUrlQuery : IRequest<string>
{

    public class GetGoogleSheetUrlQueryHandler : IRequestHandler<GetGoogleSheetUrlQuery, string>
    {
        private readonly IJsonStorage _jsonStorage;

        public GetGoogleSheetUrlQueryHandler(IJsonStorage jsonStorage)
        {
            _jsonStorage = jsonStorage;
        }

        public async Task<string> Handle(GetGoogleSheetUrlQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_jsonStorage.GoogleSheetUrl) ?? "https://docs.google.com/spreadsheets";
        }
    }

}
