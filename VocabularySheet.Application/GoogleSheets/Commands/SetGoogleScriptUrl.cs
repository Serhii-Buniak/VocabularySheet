using System.Text.RegularExpressions;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Application.GoogleSheets.Commands;

public static partial class SetGoogleScriptUrl
{
    public class Command : IRequest
    {
        public required string Url { get; set; }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IConfigurationRepository<GoogleSheetConfigurationEntity> _configuration;

            public Handler(IConfigurationRepository<GoogleSheetConfigurationEntity> configuration)
            {
                _configuration = configuration;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _configuration.Set(conf =>
                {
                    conf.ScriptUrl = request.Url;
                    return conf;
                }, cancellationToken);
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
