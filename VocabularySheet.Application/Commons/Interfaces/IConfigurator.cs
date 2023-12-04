using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Application.Commons.Interfaces;

public interface IConfigurator<T> where T : BaseConfigurationEntity<T>, new()
{
    ConfigType Type { get; }
    Task<T> Get(CancellationToken cancellationToken);
    Task<T> Set(T configuration, CancellationToken cancellationToken);
    Task<T> Set(Func<T, T> func, CancellationToken cancellationToken);
}