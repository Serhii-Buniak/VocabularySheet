using Domain.WordModels;

namespace Infrastructure.Data.Repositories.Interfaces;

public interface IGoogleSheetWordsRepository
{
    Task<IEnumerable<Word>?> GetAllAsync(CancellationToken cancellationToken);
}