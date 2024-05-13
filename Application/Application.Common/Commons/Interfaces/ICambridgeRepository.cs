using Domain.Localization;
using Domain.WebSources;

namespace Application.Common.Commons.Interfaces;

public interface ICambridgeRepository
{
    Task<PublicCambridgeEntry?> Get(string word, WordLanguage language, CancellationToken cancellationToken);
}