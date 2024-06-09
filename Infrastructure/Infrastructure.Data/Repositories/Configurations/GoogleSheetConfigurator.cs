using Domain.Common;
using Domain.Localization;
using Infrastructure.Data.Data.Interfaces;

namespace Infrastructure.Data.Repositories.Configurations;

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