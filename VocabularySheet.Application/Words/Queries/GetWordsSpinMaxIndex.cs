namespace VocabularySheet.Application.Words.Queries;

public static class GetWordsSpinMaxIndex
{
    public class Query : IRequest<int>
    {
        public class Handler : IRequestHandler<Query, int>
        {
            private readonly IWordsRepository _repository;

            public Handler(IWordsRepository repository)
            {
                _repository = repository;
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.CountAsync(cancellationToken);
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

