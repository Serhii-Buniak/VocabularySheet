using AutoMapper;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Domain;

namespace VocabularySheet.Application.Commons.Profiles;

public class WordProfile : Profile
{
    public WordProfile()
    {
        CreateMap<Word, WordReadDto>();
    }
}