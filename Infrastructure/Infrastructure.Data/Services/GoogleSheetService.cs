using Application.Common.Commons.Interfaces;
using Domain.WordModels;
using Infrastructure.Data.Repositories.Interfaces;
using ML.Predictor;

namespace Infrastructure.Data.Services;

public class GoogleSheetService : IGoogleSheetService
{
    private const float PassArticleValue = 0.42f;
    
    private readonly IWordsRepository _wordsRepository;
    private readonly IWordClassificationService _classificationService;
    private readonly IGoogleSheetWordsRepository _googleSheetWordsRepository;

    public GoogleSheetService(IWordsRepository wordsRepository, IWordClassificationService classificationService, IGoogleSheetWordsRepository googleSheetWordsRepository)
    {
        _wordsRepository = wordsRepository;
        _classificationService = classificationService;
        _googleSheetWordsRepository = googleSheetWordsRepository;
    }

    public async Task SynchronizeDataAsync(CancellationToken cancellationToken)
    {
        List<Word> words = await _googleSheetWordsRepository.GetAllAsync(cancellationToken);
        
        words.ForEach(SetArticleType);
        
        await _wordsRepository.Clear(cancellationToken);
        await _wordsRepository.AddRangeAsync(words, cancellationToken);
        await _wordsRepository.SaveAsync(cancellationToken);
    }

    private void SetArticleType(Word word)
    {
        var probability = _classificationService.GetProbability(word.Original);
        var list = probability.OrderedList;
        //var articleTypes = list.Where(p => p.Value >= MinArticleValue).ToList();

        //float sum = articleTypes.Sum(a => a.Value);

        if (/*sum >= SumArticlePass || */list.FirstOrDefault().Value >= PassArticleValue)
        {
            word.ArticleType = list.First().Key;;
        }
    }
}