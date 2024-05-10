using VocabularySheet.Application.Words.Queries;
using VocabularySheet.Common.Extensions;
using VocabularySheet.Maui.Domain.ViewModels;

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

public class PercentageTests
{
    record PercentageClass() : IHasPercentage
    {
        public int Percentage { get; set; }
    }

    [TestCase(60, 35, 1)] 
    [TestCase(60, 35)]
    [TestCase(60, 35, 0)]
    [TestCase(35, 24, 1)]
    [TestCase(40, 30, 30)] // All elements sum to 100
    [TestCase(100)]         // Single element
    [TestCase(50, 50)]     // Two elements sum to 100
    [TestCase(33, 33, 33, 1)] // One element slightly adjusted to make the sum 100
    [TestCase(10, 20, 30, 40, 0)] // Five elements sum to 100
    [TestCase(50, 20, 30)] // Three elements sum to less than 100
    [TestCase(25, 25, 25, 25, 25)] // All elements sum to 125, will be adjusted to 100
    public void AdjustTo100(params int[] numbers)
    {
        var list = numbers.Select(n => new PercentageClass
        {
            Percentage = n
        }).ToList();

        var result = list.AdjustPercentageTo100();

        Assert.That(result.Sum(r => r.Percentage), Is.EqualTo(100));
        Assert.That(result.Any(r => r.Percentage < 0), Is.False);
    }
}