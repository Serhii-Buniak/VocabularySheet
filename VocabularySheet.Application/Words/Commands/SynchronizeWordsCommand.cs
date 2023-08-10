using MediatR;
using VocabularySheet.Application.Commons.Interfaces;

namespace VocabularySheet.Application.Words.Commands;

public class SynchronizeWordsCommand : IRequest
{
    public class SynchronizeWordsCommandHandler : IRequestHandler<SynchronizeWordsCommand>
    {
        private readonly IGoogleSheetService _googleSheetService;

        public SynchronizeWordsCommandHandler(IGoogleSheetService googleSheetService)
        {
            _googleSheetService = googleSheetService;
        }

        public async Task Handle(SynchronizeWordsCommand request, CancellationToken cancellationToken)
        {
            await _googleSheetService.SynchronizeDataAsync(cancellationToken);
        }
    }

}
