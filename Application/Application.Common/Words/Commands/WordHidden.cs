using Application.Common.Commons.Interfaces;
using Domain.WordModels;

namespace Application.Common.Words.Commands;

public static class WordHidden
{
    public class SetHidden : IRequest
    {
        public required long Id { get; init; }
        
        public class Handler : IRequestHandler<SetHidden>
        {
            private readonly IWordsRepository _wordsRepository;

            public Handler(IWordsRepository wordsRepository)
            {
                _wordsRepository = wordsRepository;
            }

            public async Task Handle(SetHidden request, CancellationToken cancellationToken)
            {
                await _wordsRepository.SetHidden(request.Id, cancellationToken);
            }
        }
    }
    
    public class SetNotHidden : IRequest
    {
        public int FromIndex { get; set; }
        public int ToIndex { get; set; }
        public Category? SelectedCategory { get; set; }
        
        public class Handler : IRequestHandler<SetNotHidden>
        {
            private readonly IWordsRepository _wordsRepository;

            public Handler(IWordsRepository wordsRepository)
            {
                _wordsRepository = wordsRepository;
            }

            public async Task Handle(SetNotHidden request, CancellationToken cancellationToken)
            {
                int needSkip = request.FromIndex - 1;
                int needTake = request.ToIndex - needSkip;

                if (request.SelectedCategory.HasValue)
                {
                    await _wordsRepository.SetNotHidden(needTake, needSkip, request.SelectedCategory.Value, cancellationToken);
                }
                else
                {
                    await _wordsRepository.SetNotHidden(needTake, needSkip, cancellationToken);
                }
            }
        }
    }
    
    public class SetHiddenValidation : AbstractValidator<SetHidden>
    {
        public SetHiddenValidation()
        {
        }
    }
    
    public class SetNotHiddenValidation : AbstractValidator<SetNotHidden>
    {
        public SetNotHiddenValidation()
        {
        }
    }
}