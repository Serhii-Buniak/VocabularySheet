using Application.Common.Commons.Interfaces;
using Domain.Common;
using Domain.WordModels;
using Infrastructure.Data.HttpClients.Interfaces;
using Infrastructure.Data.Repositories.Interfaces;
using Infrastructure.Data.SheetStreamers;

namespace Infrastructure.Data.Repositories;

internal class GoogleSheetWordsRepository : IGoogleSheetWordsRepository
{
    private readonly IGoogleSheetClient _client;
    private readonly SheetWordStreamer _streamer;
    private readonly IConfigurator<GoogleSheetConfig> _configuration;

    public GoogleSheetWordsRepository(IGoogleSheetClient client, SheetWordStreamer streamer, IConfigurator<GoogleSheetConfig> configuration)
    {
        _client = client;
        _streamer = streamer;
        _configuration = configuration;
    }
    
    public async Task<List<Word>> GetAllAsync(CancellationToken cancellationToken)
    {
        var configuration = await _configuration.Get(cancellationToken);
        Task scriptTask = _client.RunScriptAsync(configuration.ScriptUrl, cancellationToken);
        
        await using Stream stream = await _client.GetCsvFileAsync(configuration.SheetUrl, cancellationToken);
        var result = await _streamer.ReadAsync(stream, configuration.SheetName, cancellationToken);
        
        await scriptTask;
        return result.ToList();
    }
}