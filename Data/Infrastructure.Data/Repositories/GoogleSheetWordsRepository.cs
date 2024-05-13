using Application.Common.Commons.Interfaces;
using Domain.Common;
using Domain.WordModels;
using Infrastructure.Data.CsvStreamers;
using Infrastructure.Data.HttpClients.Interfaces;
using Infrastructure.Data.Repositories.Interfaces;

namespace Infrastructure.Data.Repositories;

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