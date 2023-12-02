using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Application.Commons.Mappings;

public static class WordMapping
{
    public static WordReadDto ToWordRead(this Word word) => new()
    {
        Id = word.Id,
        Original = word.Original,
        Translation = word.Translation,
        Description = word.Description,
    };


    public static IEnumerable<WordReadDto> ToWordsRead(this IEnumerable<Word> words) => words
        .Select((word, index) => word.ToWordRead());    
    
    
    public static WordSpinDto ToWordSpin(this Word word, int index, WordLanguage language, WordLanguage transLang) => new()
    {
        Index = index,
        OrignalLanguage = language,
        TranslationlLanguage = transLang,
        Original = word.Original,
        Translation = word.Translation,
        Description = word.Description,
    };


    public static IEnumerable<WordSpinDto> ToWordsSpin(this IEnumerable<Word> words, WordLanguage language, WordLanguage transLang) => words
        .Select((word, index) => word.ToWordSpin(index + 1, language, transLang));
}
