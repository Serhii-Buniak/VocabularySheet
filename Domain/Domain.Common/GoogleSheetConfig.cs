namespace Domain.Common;

public record GoogleSheetConfig : BaseConfigurationEntity<GoogleSheetConfig>
{
    public override ConfigType Type => ConfigType.GoogleSheet;
    
    public string SheetUrl { get; set; } = string.Empty;
    public string? SheetName { get; set; }
    public string ScriptUrl { get; set; } = string.Empty;
}