using VocabularySheet.Common.Extensions;

namespace VocabularySheet.Tests;

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