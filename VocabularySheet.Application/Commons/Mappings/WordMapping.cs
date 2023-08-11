using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Domain;

namespace VocabularySheet.Application.Commons.Mappings;

public static class WordMapping
{
    public static WordReadDto ToWordRead(this Word word, long index) => new()
    {
        Index = index,
        Original = word.Original,
        Translation = word.Translation,
        Description = word.Description,
    };


    public static IEnumerable<WordReadDto> ToWordsRead(this IEnumerable<Word> words) => words
        .Select((word, index) => word.ToWordRead(index + 1));
}
