using System.Text.RegularExpressions;

namespace VocabularySheet.Application.GoogleSheets.Commands;

public static class SetGoogleScriptUrl
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

    public class Validation : AbstractValidator<Command>
    {
        public readonly static Regex UrlRegex = new(@"https:\/\/script\.google\.com\/macros\/s\/.*?\/exec");

        public Validation()
        {
            RuleFor(command => command.Url)
              .Matches(UrlRegex);
        }
    }
}
