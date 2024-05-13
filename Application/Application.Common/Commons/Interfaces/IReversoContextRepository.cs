using Domain.Localization;
using Domain.WebSources;

namespace Application.Common.Commons.Interfaces;

public interface IReversoContextRepository
{
    Task<PublicReversoContextEntry?> Get(string word, WordLanguage language, WordLanguage translateLanguage,
        CancellationToken cancellationToken);
}