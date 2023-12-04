using Microsoft.Extensions.Configuration;
using System.IO;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Infrastructure.Csv.Interfaces;
using VocabularySheet.Infrastructure.HttpClients.Interfaces;
using VocabularySheet.Infrastructure.Repositories.Configurations;
using VocabularySheet.Infrastructure.Repositories.Interfaces;

namespace VocabularySheet.Infrastructure.Repositories;

public class GoogleSheetWordsRepository : IGoogleSheetWordsRepository
{
    private readonly IGoogleSheetClient _client;
    private readonly ICsvWordStreamer _streamer;
    private readonly IConfigurator<GoogleSheetConfig> _configuration;

    public GoogleSheetWordsRepository(IGoogleSheetClient client, ICsvWordStreamer streamer, IConfigurator<GoogleSheetConfig> configuration)
    {
        _client = client;
        _streamer = streamer;
        _configuration = configuration;
    }
    
    public async Task<IEnumerable<Word>?> GetAllAsync(CancellationToken cancellationToken)
    {
        var configuration = await _configuration.Get(cancellationToken);
        await _client.RunScriptAsync(configuration.ScriptUrl, cancellationToken);

        await using Stream stream = await _client.GetCsvFileAsync(configuration.SheetUrl, cancellationToken);

        return await _streamer.ReadAsync(stream, cancellationToken);
    }
}