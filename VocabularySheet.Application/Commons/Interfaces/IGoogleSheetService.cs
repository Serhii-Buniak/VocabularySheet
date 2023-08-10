namespace VocabularySheet.Application.Commons.Interfaces;

public interface IGoogleSheetService
{
    Task SynchronizeDataAsync(CancellationToken cancellationToken);
}
