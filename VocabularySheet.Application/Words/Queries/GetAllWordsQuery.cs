using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Commons.Mappings;
using VocabularySheet.Domain;

namespace VocabularySheet.Application.Words.Queries;

public static class GetAllWords
{
    public class Query : IRequest<IEnumerable<WordReadDto>>
    {
        public class Handler : IRequestHandler<Query, IEnumerable<WordReadDto>>
        {
            private readonly IWordsRepository _repository;

            public Handler(IWordsRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<WordReadDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                IEnumerable<Word> words = await _repository.GetAllAsync(cancellationToken);

                IEnumerable<WordReadDto> wordReads = words.ToWordsRead();

                return wordReads;
            }
        }
    }

    public class Validation : AbstractValidator<Query>
    {
        public Validation()
        {

        }
    }
}

