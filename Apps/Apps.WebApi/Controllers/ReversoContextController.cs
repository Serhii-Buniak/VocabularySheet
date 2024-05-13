using Domain.Localization;
using Microsoft.AspNetCore.Mvc;
using WebSources.ReversoContext;

namespace Apps.WebApi.Controllers;

public record ReversoContextReq
{
    public required string Word { get; set; }
}

[ApiController]
[Route("[controller]")]
public class ReversoContextController : ControllerBase
{
    private readonly ReversoContextClient _client;
    private readonly ReversoContextPageBuilder _builder;

    public ReversoContextController(ReversoContextClient client, ReversoContextPageBuilder builder)
    {
        _client = client;
        _builder = builder;
    }

    [HttpPost("Word")]
    public async Task<IActionResult> GetWord([FromBody]ReversoContextReq req, [FromQuery]WordLanguage language = WordLanguage.En, [FromQuery]WordLanguage translationLanguage = WordLanguage.En)
    {
        var page = await _builder.Build(req.Word, language, translationLanguage, CancellationToken.None);
        
        return Ok(page);
    }
}