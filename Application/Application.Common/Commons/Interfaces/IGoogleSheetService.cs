namespace Application.Common.Commons.Interfaces;

public interface IGoogleSheetService
{
    Task SynchronizeDataAsync(CancellationToken cancellationToken);
}