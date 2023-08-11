using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Application.Commons.Mappings;
using VocabularySheet.Domain;

namespace VocabularySheet.Application.Words.Queries;

public static class GetSpinWords
{
    public class Query : IRequest<IEnumerable<WordSpinDto>>
    {
        public int FromIndex { get; set; }
        public int ToIndex { get; set; }
        public bool IsOriginalMode { get; set; }
        public bool IsTranslationMode { get; set; }

        public class Handler : IRequestHandler<Query, IEnumerable<WordSpinDto>>
        {
            private readonly IWordsRepository _repository;

            public Handler(IWordsRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<WordSpinDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                int needSkip = request.FromIndex - 1;
                int needTake = request.ToIndex - needSkip;

                IEnumerable<Word> words = await _repository.TakeAsync(needTake, needSkip, cancellationToken);


                List<WordSpinDto> result = new();

                int index = request.FromIndex;
                foreach (Word word in words)
                {
                    if (request.IsOriginalMode)
                    {
                        result.Add(new WordSpinDto
                        {
                            Index = index,
                            Original = word.Original,
                            Translation = word.Translation,
                            Description = word.Description,
                        });
                    }          
                    
                    if (request.IsTranslationMode)
                    {
                        result.Add(new WordSpinDto
                        {
                            Index = index,
                            Original = word.Translation,
                            Translation = word.Original,
                            Description = word.Description,
                        });
                    }

                    index++;
                }

                return result;
            }
        }
    }

    public class Validation : AbstractValidator<Query>
    {
        public Validation(IWordsRepository repository)
        {
            const int min = 1;
            int max = repository.Count();

            RuleFor(q => q.FromIndex)
                .GreaterThanOrEqualTo(min)
                .LessThanOrEqualTo(q => q.FromIndex);

            RuleFor(q => q.ToIndex)
                .GreaterThanOrEqualTo(q => q.FromIndex)
                .LessThanOrEqualTo(max);
        }
    }
}

