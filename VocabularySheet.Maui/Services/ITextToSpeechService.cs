namespace VocabularySheet.Maui.Services
{
    public interface ITextToSpeechService
    {
        Task<LocaleAndText> GetLocaleAndTextForTextAsync(string text);
    }
}