namespace VocabularySheet.Domain.ConfigEntities;

public record GoogleSheetConfig : BaseConfigurationEntity<GoogleSheetConfig>
{
    public override ConfigType Type => ConfigType.GoogleSheet;
    
    public string SheetUrl { get; set; } = string.Empty;
    public string ScriptUrl { get; set; } = string.Empty;
}