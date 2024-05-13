using ML.Predictor;

namespace Application.Common.ArticlePrediction;

public static class ArticlePrediction
{
    public class Query : IRequest<ArticleProbabilityResult>
    {
        public required string Word { get; init; }
        
        public class Handler : IRequestHandler<Query, ArticleProbabilityResult>
        {
            private readonly IWordClassificationService _classificationService;

            public Handler(IWordClassificationService classificationService)
            {
                _classificationService = classificationService;
            }

            public Task<ArticleProbabilityResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = _classificationService.GetProbability(request.Word);
                return Task.FromResult(result);
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
