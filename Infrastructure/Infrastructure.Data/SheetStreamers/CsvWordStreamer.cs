using Domain.WordModels;
using Infrastructure.Data.Commons.Mappings;
using Infrastructure.Data.SheetStreamers.Models;
using Tools.Parsers;

namespace Infrastructure.Data.SheetStreamers;

internal class SheetWordStreamer
{
    public Task<IEnumerable<Word>> ReadAsync(Stream stream, string? sheetName, CancellationToken _)
    {
        IEnumerable<WordCsv> words = XlsxExtensions.SkipHeader
            .Deserialize<WordCsv>(stream, sheetName)
            .ToList();

        return Task.FromResult(words.Where(w => !IsBroken(w)).ToWordsCsv());
    }

    private static bool IsBroken(WordCsv wordCsv)
    {
        return string.IsNullOrWhiteSpace(wordCsv.Original);
    }
}
