using Domain.Common;

namespace Domain.Localization;

public enum WordLanguage
{
    /// <summary>
    /// En
    /// </summary>
    En = 0,
    /// <summary>
    /// Ua
    /// </summary>
    Ua = 1,
    /// <summary>
    /// 404?
    /// </summary>
    Ru = 2,
}



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