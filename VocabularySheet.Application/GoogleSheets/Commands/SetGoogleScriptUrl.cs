using System.Text.RegularExpressions;

namespace VocabularySheet.Application.GoogleSheets.Commands;

public static partial class  SetGoogleScriptUrl
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
                _configurationRepository.SetGoogleScriptUrl(request.Url);

                await Task.CompletedTask;
            }
        }
    }

    public partial class Validation : AbstractValidator<Command>
    {
        public readonly static Regex UrlRegex = GetUrlRegex();

        public Validation()
        {
            RuleFor(command => command.Url)
              .Matches(UrlRegex);
        }

        [GeneratedRegex("https:\\/\\/script\\.google\\.com\\/macros\\/s\\/.*?\\/exec")]
        private static partial Regex GetUrlRegex();
    }
}
