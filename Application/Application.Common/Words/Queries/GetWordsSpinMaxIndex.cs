using Application.Common.Commons.Interfaces;
using Domain.WordModels;

namespace Application.Common.Words.Queries;

public static class GetWordsSpinMaxIndex
{
    public class Query : IRequest<int>
    {
        public Category? Category { get; set; }
        
        public class Handler : IRequestHandler<Query, int>
        {
            private readonly IWordsRepository _repository;

            public Handler(IWordsRepository repository)
            {
                _repository = repository;
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.Category.HasValue)
                {
                    return await _repository.CountAsync(request.Category.Value, cancellationToken);
                }
                else
                {
                    return await _repository.CountAsync(cancellationToken);
                }
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

