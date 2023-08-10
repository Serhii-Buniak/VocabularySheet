using AutoMapper;
using VocabularySheet.Domain;

namespace VocabularySheet.Infrastructure.Csv.Models;

public class WordCsvProfile : Profile
{
    public WordCsvProfile()
    {
        CreateMap<WordCsv, Word>();
    }
}