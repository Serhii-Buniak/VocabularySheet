using VocabularySheet.Common;

namespace VocabularySheet.Application.Commons.Dtos;

public record WordLanguageItem(WordLanguage Key, string Description)
{
    public override string ToString()
    {
        return Description;
    }
}