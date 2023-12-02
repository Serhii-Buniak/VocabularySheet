namespace VocabularySheet.Domain.ConfigEntities;

public enum WordLanguage
{
    En = 0,
    Ua = 1,
    Ru = 2,
}

public record LocalizationConfigurationEntity : BaseConfigurationEntity<LocalizationConfigurationEntity>
{
    public override ConfigType Type => ConfigType.VocabularyLocalization;
    
    public WordLanguage OriginLang { get; set; } = WordLanguage.En;
    public WordLanguage TranslateLang { get; set; } = WordLanguage.En;
}