using System.Text.RegularExpressions;

namespace VocabularySheet.Application.GoogleSheets.Commands;

public static class SetGoogleSheetUrl
{
    public class Command : IRequest
    {
        public required string Url { get; set; }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IGoogleSheetConfigurationRepository _configurationRepository;

            public Handler(IGoogleSheetConfigurationRepository configurationRepository)
            {
                _configurationRepository = configurationRepository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _configurationRepository.SetGoogleSheetUrl(request.Url);

                await Task.CompletedTask;
            }
        }
    }

    public class Validation : AbstractValidator<Command>
    {
        public readonly static Regex UrlRegex = new(@"https:\/\/docs\.google\.com\/spreadsheets\/(u\/\d\/)?d\/[a-zA-Z0-9_-]+(\/(edit(\?usp=sharing)?|(\?usp=#gid=\d+))?)?");

        public Validation()
        {
            RuleFor(command => command.Url)
              .Matches(UrlRegex);
        }
    }
}
