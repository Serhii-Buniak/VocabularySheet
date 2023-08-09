using Microsoft.EntityFrameworkCore;
using VocabularySheet.Domain;

namespace VocabularySheet.Application.Commons.Interfaces;

public interface IAppDbContext
{
    DbSet<Word> Words { get; }
}
