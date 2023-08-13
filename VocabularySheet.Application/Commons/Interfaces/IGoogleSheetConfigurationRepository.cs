namespace VocabularySheet.Application.Commons.Interfaces
{
    public interface IGoogleSheetConfigurationRepository
    {
        string GetGoogleSheetUrl();
        string GetGoogleScriptUrl();
        void SetGoogleScriptUrl(string value);
        void SetGoogleSheetUrl(string value);
    }
}