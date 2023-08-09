using MediatR;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Application.GoogleSheets.Queries;

namespace VocabularySheet.Application.GoogleSheets.Commands;

public class SetGoogleSheetUrlCommand : IRequest
{
    public required string Url { get; set; }

    public class SetGoogleSheetUrlCommandHandler : IRequestHandler<SetGoogleSheetUrlCommand>
    {
        private readonly IJsonStorage _jsonStorage;

        public SetGoogleSheetUrlCommandHandler(IJsonStorage jsonStorage)
        {
            _jsonStorage = jsonStorage;
        }

        public async Task Handle(SetGoogleSheetUrlCommand request, CancellationToken cancellationToken)
        {
            _jsonStorage.GoogleSheetUrl = request.Url;

            await Task.CompletedTask;
        }
    }
}
