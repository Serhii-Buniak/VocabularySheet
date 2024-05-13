using System.Text.RegularExpressions;
using Application.Common.Commons.Interfaces;
using Domain.Common;

namespace Application.Common.GoogleSheets.Commands;

public static partial class SetGoogleSheetUrl
{
    public class Command : IRequest
    {
        public required string Url { get; set; }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IConfigurator<GoogleSheetConfig> _configuration;

            public Handler(IConfigurator<GoogleSheetConfig> configuration)
            {
                _configuration = configuration;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _configuration.Set(conf =>
                {
                    conf.SheetUrl = request.Url;
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

        [GeneratedRegex("https:\\/\\/docs\\.google\\.com\\/spreadsheets\\/(u\\/\\d\\/)?d\\/[a-zA-Z0-9_-]+(\\/(edit(\\?usp=sharing)?|(\\?usp=#gid=\\d+))?)?")]
        private static partial Regex GetUrlRegex();
    }
}
