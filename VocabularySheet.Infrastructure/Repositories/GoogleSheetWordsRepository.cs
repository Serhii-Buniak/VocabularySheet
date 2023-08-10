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
        string? url = _configuration.GetGoogleSheetUrl() 
            ?? throw new NullReferenceException("Google sheet Url is null");

        using Stream stream = await _client.GetCsvFileAsync(url, cancellationToken);

        return await _streamer.ReadAsync(stream, cancellationToken);
    }
}