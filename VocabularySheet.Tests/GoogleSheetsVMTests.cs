using VocabularySheet.Application.Words.Queries;
using VocabularySheet.Maui.ViewModels;

namespace VocabularySheet.Tests;

public class GoogleSheetsVMTests
{
    private WordsSpinVM WordsSpinVM => _wordsSpinVM ?? throw new Exception();
    
    private WordsSpinVM? _wordsSpinVM;
    private Mock<IMediator>? _mediator;
    private Mock<ILogger<WordsSpinVM>>? _logger;
    
    [SetUp]
    public void Setup()
    {
        _mediator = new Mock<IMediator>();
        _logger = new Mock<ILogger<WordsSpinVM>>();
        _wordsSpinVM = new WordsSpinVM(_mediator.Object, _logger.Object, null!);
    }
    
    [Test]
    public async Task ResetIndexShouldSetZeroWhenMaxZero()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(0));
        await WordsSpinVM.SetMaxIndex();
        
        WordsSpinVM.ResetIndex();
        
        Assert.That(WordsSpinVM.FromIndex, Is.EqualTo(0));
        Assert.That(WordsSpinVM.ToIndex, Is.EqualTo(0));
    }
    
    [Test]
    public async Task ResetIndexShouldSetValues()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(100));
        await WordsSpinVM.SetMaxIndex();
    
        WordsSpinVM.ResetIndex();
        
        Assert.That(WordsSpinVM.FromIndex, Is.EqualTo(1));
        Assert.That(WordsSpinVM.ToIndex, Is.EqualTo(100));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldSetZeroWhenMaxZero()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(0));
    
        await WordsSpinVM.HandleSynchronize();
        
        Assert.That(WordsSpinVM.FromIndex, Is.EqualTo(0));
        Assert.That(WordsSpinVM.ToIndex, Is.EqualTo(0));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldResetIndex()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(100));
    
        await WordsSpinVM.HandleSynchronize();
        
        Assert.That(WordsSpinVM.FromIndex, Is.EqualTo(1));
        Assert.That(WordsSpinVM.ToIndex, Is.EqualTo(100));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldNotChangeIndex()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(100));

        WordsSpinVM.FromIndex = 20;
        WordsSpinVM.ToIndex = 60;
    
        await WordsSpinVM.HandleSynchronize();
        
        Assert.That(WordsSpinVM.FromIndex, Is.EqualTo(20));
        Assert.That(WordsSpinVM.ToIndex, Is.EqualTo(60));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldChangeToIndex()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(50));

        WordsSpinVM.FromIndex = 20;
        WordsSpinVM.ToIndex = 60;
    
        await WordsSpinVM.HandleSynchronize();
        
        Assert.That(WordsSpinVM.FromIndex, Is.EqualTo(20));
        Assert.That(WordsSpinVM.ToIndex, Is.EqualTo(50));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldIndexesBeSame()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(20));

        WordsSpinVM.FromIndex = 20;
        WordsSpinVM.ToIndex = 60;
    
        await WordsSpinVM.HandleSynchronize();
        
        Assert.That(WordsSpinVM.FromIndex, Is.EqualTo(20));
        Assert.That(WordsSpinVM.ToIndex, Is.EqualTo(20));
    }   
    
    [Test]
    public async Task HandleSynchronizeShouldWhenMaxLowerThanFromIndexResetIndex()
    {
        _mediator?
            .Setup(m => m.Send(It.IsAny<GetWordsSpinMaxIndex.Query>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(10));

        WordsSpinVM.FromIndex = 20;
        WordsSpinVM.ToIndex = 60;
    
        await WordsSpinVM.HandleSynchronize();
        
        Assert.That(WordsSpinVM.FromIndex, Is.EqualTo(1));
        Assert.That(WordsSpinVM.ToIndex, Is.EqualTo(10));
    }
}