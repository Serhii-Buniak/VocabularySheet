using Microsoft.AspNetCore.Mvc;
using VocabularySheet.CambridgeDictionary;
using VocabularySheet.Domain.ConfigEntities;

namespace Sandbox.Controllers;

[ApiController]
[Route("[controller]")]
public class CambridgeController : ControllerBase
{
    private readonly CabridgePageBuilder _cabridge;

    public CambridgeController(CabridgePageBuilder cabridge)
    {
        _cabridge = cabridge;
    }

    [HttpGet("Word/{word}")]
    public async Task<IActionResult> GetWord(string word, [FromQuery] WordLanguage language, CancellationToken cancellationToken)
    {
        var page = await _cabridge.Build(word, language, cancellationToken);

        return Ok(page);
    }    
    
    
    [HttpGet("Html/{word}")]
    public async Task<IActionResult> GetHtml(string word, [FromQuery] WordLanguage language, CancellationToken cancellationToken)
    {
        // var page = await _cambridgeClient.Page(word, language, cancellationToken);
        return Ok("page");
    }
}