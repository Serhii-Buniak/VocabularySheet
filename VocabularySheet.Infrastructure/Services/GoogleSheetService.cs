using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;
using VocabularySheet.Infrastructure.Repositories.Interfaces;

namespace VocabularySheet.Infrastructure.Services;

public class GoogleSheetService : IGoogleSheetService
{
    private readonly IWordsRepository _wordsRepository;
    private readonly IGoogleSheetWordsRepository _googleSheetWordsRepository;

    public GoogleSheetService(IWordsRepository wordsRepository, IGoogleSheetWordsRepository googleSheetWordsRepository)
    {
        _wordsRepository = wordsRepository;
        _googleSheetWordsRepository = googleSheetWordsRepository;
    }

    public async Task SynchronizeDataAsync(CancellationToken cancellationToken)
    {
        await _wordsRepository.Clear(cancellationToken);
        IEnumerable<Word> words = await _googleSheetWordsRepository.GetAllAsync(cancellationToken) ?? Enumerable.Empty<Word>();
        await _wordsRepository.AddRangeAsync(words, cancellationToken);
        await _wordsRepository.SaveAsync(cancellationToken);
    }
}