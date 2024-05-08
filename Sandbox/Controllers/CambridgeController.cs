using Microsoft.AspNetCore.Mvc;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Common;
using VocabularySheet.Common.Extensions;
using VocabularySheet.Infrastructure.Repositories.Pages;

namespace Sandbox.Controllers;

public record CambridgeReq
{
    public required string Word { get; set; }
}

[ApiController]
[Route("[controller]")]
public class CambridgeController : ControllerBase
{
    private readonly ICambridgeRepository _cabridge;
    private readonly ImageClient _imageClient;

    public CambridgeController(ICambridgeRepository cabridge, ImageClient imageClient)
    {
        _cabridge = cabridge;
        _imageClient = imageClient;
    }

    [HttpPost("Word")]
    public async Task<IActionResult> GetWord([FromBody]CambridgeReq req, [FromQuery]WordLanguage language = WordLanguage.En)
    {
        var page = await _cabridge.Get(req.Word, language, CancellationToken.None);

        return Ok(page);
    }

    [HttpGet("image/file/{word}")]
    public async Task<IActionResult> GetImageWord([FromRoute] string word, [FromQuery]WordLanguage language = WordLanguage.En)
    {
        var page = await _cabridge.Get(word, language, CancellationToken.None);
        if (page == null)
        {
            return NotFound();
        }

        string? imageUrl = page.Content.Blocks.SelectMany(x => x.Articles).SelectMany(x => x.SubArticles)
            .Select(x => x.FullImageLink())
            .WhereNotNull()
            .FirstOrDefault();
        if (imageUrl == null)
        {
            return NotFound();
        }

        Stream? stream = await _imageClient.GetImageStream(imageUrl, CancellationToken.None);
        if (stream == null)
        {
            return NotFound();
        }

        return File(stream, "image/png");
    }
    
    [HttpGet("examples/{word}")]
    public async Task<IActionResult> GetExamples([FromRoute] string word)
    {
        var page = await _cabridge.Get(word, WordLanguage.En, CancellationToken.None);
        if (page == null)
        {
            return Ok(new List<string>());
        }

        List<string> examples = page.Content.Blocks
            .SelectMany(x => x.Articles)
            .SelectMany(x => x.SubArticles)
            .SelectMany(x => x.Examples)
            .ToList();
        
        return Ok(examples);
    }
}