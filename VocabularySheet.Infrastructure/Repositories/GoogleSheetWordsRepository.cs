using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Infrastructure.CsvStreamers;
using VocabularySheet.Infrastructure.HttpClients.Interfaces;
using VocabularySheet.Infrastructure.Repositories.Interfaces;

namespace VocabularySheet.Infrastructure.Repositories;

internal class GoogleSheetWordsRepository : IGoogleSheetWordsRepository
{
    private readonly IGoogleSheetClient _client;
    private readonly CsvWordStreamer _streamer;
    private readonly IConfigurator<GoogleSheetConfig> _configuration;

    public GoogleSheetWordsRepository(IGoogleSheetClient client, CsvWordStreamer streamer, IConfigurator<GoogleSheetConfig> configuration)
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