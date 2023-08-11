using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Words.Queries;

namespace VocabularySheet.Maui.ViewModels;

public partial class WordsSpinVM : BaseViewModel
{
    [ObservableProperty]
    private GetSpinWords.Query queryParameters = new()
    {
        FromIndex = 1,
        ToIndex = 1,
        IsOriginalMode = true,
        IsTranslationMode = false,
    };

    [ObservableProperty]
    private WordSpinDto word = WordSpinDto.Sample;

    [ObservableProperty]
    private bool isDescriptionShowed = true;

    [ObservableProperty]
    private bool isTranslationEnable = true;

    public WordsSpinVM(IMediator mediator) : base(mediator)
    {

    }

    public async Task ResetIndex()
    {
        int max = await Mediator.Send(new GetWordsSpinMaxIndex.Query());

        if (max == 0)
        {
            QueryParameters.FromIndex = 0;
            QueryParameters.ToIndex = 0;
        }
        else
        {
            QueryParameters.ToIndex = 1;
        }

    }
}
