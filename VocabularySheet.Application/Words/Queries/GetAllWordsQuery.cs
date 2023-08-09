using MediatR;
using Microsoft.EntityFrameworkCore;
using VocabularySheet.Application.Commons;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;

namespace VocabularySheet.Application.Words.Queries;

public class GetAllWordsQuery : IRequest<IEnumerable<WordReadDto>>
{
    public class GetAllWordsQueryHandler : EntityRequestHandler, IRequestHandler<GetAllWordsQuery, IEnumerable<WordReadDto>>
    {
        public GetAllWordsQueryHandler(IAppDbContext context, IMapperService mapper) : base(context, mapper)
        {
        }

        async Task<IEnumerable<WordReadDto>> IRequestHandler<GetAllWordsQuery, IEnumerable<WordReadDto>>.Handle(GetAllWordsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Word> words = await Context.Words.ToListAsync(cancellationToken);

            var wordReads = Mapper.Map<IEnumerable<WordReadDto>>(words);

            return wordReads;
        }
    }
}