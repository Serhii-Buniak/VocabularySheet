using AutoMapper;
using VocabularySheet.Application.Commons.Interfaces;

namespace VocabularySheet.Infrastructure.Services;

public class MapperService : Mapper, IMapperService
{
    public MapperService(IConfigurationProvider configuration) : base(configuration)
    {
    }
}
