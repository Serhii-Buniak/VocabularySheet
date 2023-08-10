using MediatR;
using Microsoft.EntityFrameworkCore;
using VocabularySheet.Application.Commons;
using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Domain;

namespace VocabularySheet.Application.Words.Queries;

public class GetAllWordsQuery : IRequest<IEnumerable<WordReadDto>>
{
    public class GetAllWordsQueryHandler : IRequestHandler<GetAllWordsQuery, IEnumerable<WordReadDto>>
    {
        private readonly IWordsRepository _repository;
        private readonly IMapperService _mapper;

        public GetAllWordsQueryHandler(IWordsRepository repository, IMapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordReadDto>> Handle(GetAllWordsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Word> words = await _repository.GetAllAsync(cancellationToken);

            var wordReads = _mapper.Map<IEnumerable<WordReadDto>>(words);

            return wordReads;
        }
    }
}
