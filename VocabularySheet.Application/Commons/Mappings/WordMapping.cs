using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Application.Commons.Mappings;

public static class WordMapping
{
    public static WordModel ToWordSpin(this Word word, int index, WordLanguage language, WordLanguage transLang) => new()
    {
        Id = word.Id,
        Index = index,
        OrignalLanguage = language,
        TranslationlLanguage = transLang,
        Original = word.Original,
        Translation = word.Translation,
        Description = word.Description,
    };


    public static IEnumerable<WordModel> ToWordsSpin(this IEnumerable<Word> words, WordLanguage language, WordLanguage transLang) => words
        .Select((word, index) => word.ToWordSpin(index + 1, language, transLang));
}
