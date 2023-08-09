using VocabularySheet.Application.Commons.Interfaces;

namespace VocabularySheet.Application.Commons;

public abstract class EntityRequestHandler
{
    protected IAppDbContext Context { get; }
    protected IMapperService Mapper { get; }

    public EntityRequestHandler(IAppDbContext context, IMapperService mapper)
    {
        Context = context;
        Mapper = mapper;
    }
}
