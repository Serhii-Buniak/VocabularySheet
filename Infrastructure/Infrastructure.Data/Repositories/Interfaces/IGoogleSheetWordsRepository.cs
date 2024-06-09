using Domain.WordModels;

namespace Infrastructure.Data.Repositories.Interfaces;

public interface IGoogleSheetWordsRepository
{
    Task<List<Word>> GetAllAsync(CancellationToken cancellationToken);
}