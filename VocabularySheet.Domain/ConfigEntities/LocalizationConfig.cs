using VocabularySheet.Common;

namespace VocabularySheet.Domain.ConfigEntities;

public record LocalizationConfig : BaseConfigurationEntity<LocalizationConfig>
{
    public override ConfigType Type => ConfigType.VocabularyLocalization;
    
    public WordLanguage OriginLang { get; set; } = WordLanguage.En;
    public WordLanguage TranslateLang { get; set; } = WordLanguage.En;

    public HashSet<WordLanguage> SetOfLang()
    {
        return new HashSet<WordLanguage>()
        {
            OriginLang,
            TranslateLang
        };
    }
}