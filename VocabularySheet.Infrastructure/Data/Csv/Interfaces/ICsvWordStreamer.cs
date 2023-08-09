using VocabularySheet.Domain;

namespace VocabularySheet.Infrastructure.Data.Csv.Interfaces;

public interface ICsvWordStreamer
{
    IEnumerable<Word> Read(Stream stream);
}