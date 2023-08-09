using AutoMapper;
using VocabularySheet.Domain;

namespace VocabularySheet.Infrastructure.Data.Csv.Models;

public class WordCsvProfile : Profile
{
    public WordCsvProfile()
    {
        CreateMap<WordCsv, Word>();
    }
}