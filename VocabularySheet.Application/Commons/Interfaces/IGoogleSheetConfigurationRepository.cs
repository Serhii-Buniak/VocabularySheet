namespace VocabularySheet.Application.Commons.Interfaces
{
    public interface IGoogleSheetConfigurationRepository
    {
        string GetGoogleSheetUrl();
        void SetGoogleSheetUrl(string value);
    }
}