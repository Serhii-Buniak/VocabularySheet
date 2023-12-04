using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Infrastructure.Data.Interfaces;

namespace VocabularySheet.Infrastructure.Repositories.Configurations;

public class GoogleSheetConfigurator : BaseConfigurator<GoogleSheetConfig>
{
    public override ConfigType Type => ConfigType.GoogleSheet;
    
    public GoogleSheetConfigurator(IAppDbContext context) : base(context)
    {
    }
}

public class LocalizationConfigurator : BaseConfigurator<LocalizationConfig>
{
    public override ConfigType Type => ConfigType.VocabularyLocalization;
    
    public LocalizationConfigurator(IAppDbContext context) : base(context)
    {
    }
}