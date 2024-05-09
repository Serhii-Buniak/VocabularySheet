
using System.Text.Json;
using VocabularySheet.Application.Words.Queries;
using VocabularySheet.Maui.Domain.ViewModels;
using VocabularySheet.MLApp;

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

public class SomeTest
{
    [Test]
    public void Evaluate()
    {
        MlWordClassificationService.Evaluation();
    }
    
    [Test]
    [TestCase("glory", ExpectedResult = (ArticleType)0)]
    [TestCase("big deal", ExpectedResult = (ArticleType)0)]
    [TestCase("acceleration", ExpectedResult = (ArticleType)0)]
    [TestCase("hero", ExpectedResult = (ArticleType)0)]
    [TestCase("official", ExpectedResult = (ArticleType)0)]
    [TestCase("bbq", ExpectedResult = (ArticleType)0)]
    [TestCase("documents", ExpectedResult = (ArticleType)0)]
    [TestCase("funny attrations", ExpectedResult = (ArticleType)0)]
    [TestCase("history", ExpectedResult = (ArticleType)0)]
    [TestCase("stock market", ExpectedResult = ArticleType.Business)]
    [TestCase("celebrity gossip", ExpectedResult = ArticleType.Entertainment)]
    [TestCase("healthy recipes", ExpectedResult = ArticleType.Food)]
    [TestCase("graphic design tutorials", ExpectedResult = ArticleType.Graphics)]
    [TestCase("World War II", ExpectedResult = ArticleType.Historical)]
    [TestCase("latest medical research", ExpectedResult = ArticleType.Medical)]
    [TestCase("political analysis", ExpectedResult = ArticleType.Politics)]
    [TestCase("space exploration", ExpectedResult = ArticleType.Space)]
    [TestCase("soccer matches", ExpectedResult = ArticleType.Sport)]
    [TestCase("latest technology trends", ExpectedResult = ArticleType.Technologie)]
    public ArticleType Tests(string text)
    {
        var mlWordService = new MlWordService();
        return ArticleType.Business;
        // return mlWordService.GetSemanticType(text);
    }
    
    [Test]
    [TestCase("latest technology trends")]
    public void Tests2(string text)
    {
        var mlWordService = new MlWordService();

        var a = mlWordService.GetSemanticTypesWithProbabilities(text);

        Assert.Pass();
    }
}