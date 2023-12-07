using Microsoft.AspNetCore.Mvc;
using VocabularySheet.CambridgeDictionary;
using VocabularySheet.Domain.ConfigEntities;

namespace Sandbox.Controllers;

public record class CambridgeReq
{
    public required string Word { get; set; }
}

[ApiController]
[Route("[controller]")]
public class CambridgeController : ControllerBase
{
    private readonly CabridgePageBuilder _cabridge;

    public CambridgeController(CabridgePageBuilder cabridge)
    {
        _cabridge = cabridge;
    }

    [HttpPost("Word")]
    public async Task<IActionResult> GetWord([FromBody]CambridgeReq req, [FromQuery]WordLanguage language = WordLanguage.En)
    {
        var page = await _cabridge.Build(req.Word, language, CancellationToken.None);

        return Ok(page);
    }    
    
    
    [HttpGet("Html/{word}")]
    public async Task<IActionResult> GetHtml([FromBody]CambridgeReq req, [FromQuery]WordLanguage language = WordLanguage.En)
    {
        // var page = await _cambridgeClient.Page(word, language, cancellationToken);
        return Ok("page");
    }
}