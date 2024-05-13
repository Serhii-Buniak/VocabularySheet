using Domain.Localization;

namespace Application.Common.Commons.Dtos;

public record WordLanguageItem(WordLanguage Key, string Description)
{
    public override string ToString()
    {
        return Description;
    }
}