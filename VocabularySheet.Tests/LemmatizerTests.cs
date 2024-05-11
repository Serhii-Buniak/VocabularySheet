using VocabularySheet.ML.Evaluation;

namespace VocabularySheet.Tests;

public class LemmatizerTests
{
    [TestCase("cucumbers", ExpectedResult = "cucumber")]
    [TestCase("better", ExpectedResult = "well")]
    [TestCase("jumping", ExpectedResult = "jump")]
    [TestCase("foxes", ExpectedResult = "fox")]
    [TestCase("crucifixion", ExpectedResult = "crucifixion")]
    [TestCase("crucified", ExpectedResult = "crucify")]
    public string Lemmatize(string text)
    {
        return text.Lemmatize();
    }
}