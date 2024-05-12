using VocabularySheet.Application.Words.Queries;
using VocabularySheet.Maui.AppRunner.ViewModels;

namespace VocabularySheet.Tests;

public class GoogleSheetsVmTests
{
    private WordsSpinVM WordsSpinVm => _wordsSpinVm ?? throw new Exception();
    
    private WordsSpinVM? _wordsSpinVm;
    private Mock<IMediator>? _mediator;
    private Mock<ILogger<WordsSpinVM>>? _logger;
    
    [SetUp]
    public void Setup()
    {
        _mediator = new Mock<IMediator>();
        _logger = new Mock<ILogger<WordsSpinVM>>();
        _wordsSpinVm = new WordsSpinVM(_mediator.Object, _logger.Object, null!);
    }
    
    [Test]
    public async Task ResetIndexShouldSetZeroWhenMaxZero()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(0));
        await WordsSpinVm.SetMaxIndex();
        
        WordsSpinVm.ResetIndex();
        
        Assert.That(WordsSpinVm.FromIndex, Is.EqualTo(0));
        Assert.That(WordsSpinVm.ToIndex, Is.EqualTo(0));
    }
    
    [Test]
    public async Task ResetIndexShouldSetValues()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(100));
        await WordsSpinVm.SetMaxIndex();
    
        WordsSpinVm.ResetIndex();
        
        Assert.That(WordsSpinVm.FromIndex, Is.EqualTo(1));
        Assert.That(WordsSpinVm.ToIndex, Is.EqualTo(100));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldSetZeroWhenMaxZero()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(0));
    
        await WordsSpinVm.HandleSynchronize();
        
        Assert.That(WordsSpinVm.FromIndex, Is.EqualTo(0));
        Assert.That(WordsSpinVm.ToIndex, Is.EqualTo(0));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldResetIndex()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(100));
    
        await WordsSpinVm.HandleSynchronize();
        
        Assert.That(WordsSpinVm.FromIndex, Is.EqualTo(1));
        Assert.That(WordsSpinVm.ToIndex, Is.EqualTo(100));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldNotChangeIndex()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(100));

        WordsSpinVm.FromIndex = 20;
        WordsSpinVm.ToIndex = 60;
    
        await WordsSpinVm.HandleSynchronize();
        
        Assert.That(WordsSpinVm.FromIndex, Is.EqualTo(20));
        Assert.That(WordsSpinVm.ToIndex, Is.EqualTo(60));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldChangeToIndex()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(50));

        WordsSpinVm.FromIndex = 20;
        WordsSpinVm.ToIndex = 60;
    
        await WordsSpinVm.HandleSynchronize();
        
        Assert.That(WordsSpinVm.FromIndex, Is.EqualTo(20));
        Assert.That(WordsSpinVm.ToIndex, Is.EqualTo(50));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldIndexesBeSame()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(20));

        WordsSpinVm.FromIndex = 20;
        WordsSpinVm.ToIndex = 60;
    
        await WordsSpinVm.HandleSynchronize();
        
        Assert.That(WordsSpinVm.FromIndex, Is.EqualTo(20));
        Assert.That(WordsSpinVm.ToIndex, Is.EqualTo(20));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldWhenMaxLowerThanFromIndexResetIndex()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(10));

        WordsSpinVm.FromIndex = 20;
        WordsSpinVm.ToIndex = 60;
    
        await WordsSpinVm.HandleSynchronize();
        
        Assert.That(WordsSpinVm.FromIndex, Is.EqualTo(1));
        Assert.That(WordsSpinVm.ToIndex, Is.EqualTo(10));
    }
}