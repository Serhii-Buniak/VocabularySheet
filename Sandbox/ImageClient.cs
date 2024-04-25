namespace Sandbox;

public class ImageClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ImageClient> _logger;

    public ImageClient(HttpClient httpClient, ILogger<ImageClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<Stream?> GetImageStream(string imageUrl, CancellationToken cancellationToken)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(imageUrl, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }
}