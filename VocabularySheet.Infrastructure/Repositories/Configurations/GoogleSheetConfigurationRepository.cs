using VocabularySheet.Domain.ConfigEntities;
using VocabularySheet.Infrastructure.Data.Interfaces;

namespace VocabularySheet.Infrastructure.Repositories.Configurations;

public class GoogleSheetConfigurationRepository : BaseConfigurationRepository<GoogleSheetConfigurationEntity>
{
    public override ConfigType Type => ConfigType.GoogleSheet;
    
    public GoogleSheetConfigurationRepository(IAppDbContext context) : base(context)
    {
    }
}

public class LocalizationConfigurationRepository : BaseConfigurationRepository<LocalizationConfigurationEntity>
{
    public override ConfigType Type => ConfigType.VocabularyLocalization;
    
    public LocalizationConfigurationRepository(IAppDbContext context) : base(context)
    {
    }
}