using System.Text.RegularExpressions;
using VocabularySheet.Application.Commons.Interfaces;

namespace VocabularySheet.Infrastructure.HttpClients;

public class GoogleSheetClient : IGoogleSheetClient
{
    private readonly HttpClient _httpClient;

    public GoogleSheetClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Stream> GetCsvFileAsync(string url)
    {
        string pattern = @"\/d\/([a-zA-Z0-9_-]+)";

        Match match = Regex.Match(url, pattern);

        string id = match.Groups[1].Value;

        string fileUrl = string.Format(@"https://docs.google.com/spreadsheets/u/0/d/{0}/export?format=csv&id={0}&gid=0", id);

        HttpResponseMessage httpResponse = await _httpClient.GetAsync(fileUrl);
        return await httpResponse.Content.ReadAsStreamAsync();
    }

}
