using MediatR;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Application.GoogleSheets.Queries;

namespace VocabularySheet.Application.GoogleSheets.Commands;

public class SetGoogleSheetUrlCommand : IRequest
{
    public required string Url { get; set; }

    public class SetGoogleSheetUrlCommandHandler : IRequestHandler<SetGoogleSheetUrlCommand>
    {
        private readonly IGoogleSheetConfigurationRepository _configurationRepository;

        public SetGoogleSheetUrlCommandHandler(IGoogleSheetConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task Handle(SetGoogleSheetUrlCommand request, CancellationToken cancellationToken)
        {
            _configurationRepository.SetGoogleSheetUrl(request.Url);

            await Task.CompletedTask;
        }
    }
}
