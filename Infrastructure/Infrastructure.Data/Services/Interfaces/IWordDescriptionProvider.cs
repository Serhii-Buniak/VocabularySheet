using Application.Common.Commons.Dtos;
using Application.Common.Commons.Interfaces;

namespace Infrastructure.Data.Services.Interfaces;

public interface IWordDescriptionProvider
{
    WordDescriptionProviderId WordDescriptionId { get; }
    Task<WordDescriptionResult?> GetWordDescription(WordWithLanguage word, CancellationToken cancellationToken);
}