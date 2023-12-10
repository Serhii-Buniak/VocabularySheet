using Microsoft.AspNetCore.Mvc;
using VocabularySheet.CambridgeDictionary;
using VocabularySheet.Common;

namespace Sandbox.Controllers;

public record CambridgeReq
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
}