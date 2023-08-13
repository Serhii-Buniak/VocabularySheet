using Microsoft.Extensions.Configuration;
using System.IO;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;
using VocabularySheet.Infrastructure.Csv.Interfaces;
using VocabularySheet.Infrastructure.HttpClients.Interfaces;
using VocabularySheet.Infrastructure.Repositories.Interfaces;

namespace VocabularySheet.Infrastructure.Repositories;

public class GoogleSheetWordsRepository : IGoogleSheetWordsRepository
{
    private readonly IGoogleSheetClient _client;
    private readonly ICsvWordStreamer _streamer;
    private readonly IGoogleSheetConfigurationRepository _configuration;

    public GoogleSheetWordsRepository(IGoogleSheetClient client, ICsvWordStreamer streamer, IGoogleSheetConfigurationRepository configuration)
    {
        _client = client;
        _streamer = streamer;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Word>?> GetAllAsync(CancellationToken cancellationToken)
    {
        string scriptUrl = _configuration.GetGoogleScriptUrl();
        await _client.RunScriptAsync(scriptUrl, cancellationToken);


        string sheetUrl = _configuration.GetGoogleSheetUrl();

        using Stream stream = await _client.GetCsvFileAsync(sheetUrl, cancellationToken);

        return await _streamer.ReadAsync(stream, cancellationToken);
    }
}