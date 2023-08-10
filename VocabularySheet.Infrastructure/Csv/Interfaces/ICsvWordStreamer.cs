using VocabularySheet.Domain;

namespace VocabularySheet.Infrastructure.Csv.Interfaces;

public interface ICsvWordStreamer
{
    Task<IEnumerable<Word>> ReadAsync(Stream stream, CancellationToken cancellationToken);
}