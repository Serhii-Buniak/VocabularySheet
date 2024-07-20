using Application.Common.Commons.Dtos;

namespace Application.Common.Commons.Interfaces;

public enum WordDescriptionProviderId
{
    GoogleSheet = 1,
    Cambridge = 2,
    ReversoContext = 3
}

public interface IWordDescriptionService
{
    public Task<WordDescriptionResult?> GetWordDescription(WordWithLanguage word, CancellationToken cancellationToken);
}

public record WordDescriptionResult(WordDescriptionProviderId Id)
{
    public required string Text { get; init; }
}