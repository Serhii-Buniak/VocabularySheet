using Application.Common.Commons.Interfaces;

namespace Application.Common.Words.Commands;

public static class SynchronizeWords
{
    public class Command : IRequest
    {
        public class Handler : IRequestHandler<Command>
        {
            private readonly IGoogleSheetService _googleSheetService;

            public Handler(IGoogleSheetService googleSheetService)
            {
                _googleSheetService = googleSheetService;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _googleSheetService.SynchronizeDataAsync(cancellationToken);
            }
        }
    }
    public class Validation : AbstractValidator<Command>
    {
        public Validation()
        {
        }
    }
}