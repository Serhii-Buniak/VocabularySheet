using Microsoft.AspNetCore.Mvc;
using VocabularySheet.CambridgeDictionary;
using VocabularySheet.Domain.ConfigEntities;

namespace Sandbox.Controllers;

[ApiController]
[Route("[controller]")]
public class CambridgeController : ControllerBase
{
    private readonly CambridgeClient _cambridgeClient;
    private readonly CambridgeParser _parser;

    public CambridgeController(CambridgeClient cambridgeClient, CambridgeParser parser)
    {
        _cambridgeClient = cambridgeClient;
        _parser = parser;
    }

    [HttpGet("Word/{word}")]
    public async Task<IActionResult> GetWord(string word, [FromQuery] WordLanguage language, CancellationToken cancellationToken)
    {
       var page = await _cambridgeClient.Page(word, language, cancellationToken);

       var parsed = await _parser.Page(page, cancellationToken);
       return Ok(parsed?.WithOutHtml());
    }    
    
    
    [HttpGet("Html/{word}")]
    public async Task<IActionResult> GetHtml(string word, [FromQuery] WordLanguage language, CancellationToken cancellationToken)
    {
       var page = await _cambridgeClient.Page(word, language, cancellationToken);
        return Ok(page);
    }
}