using Application.Common.Commons.Dtos;
using Application.Common.Commons.Interfaces;
using Domain.Localization;
using Domain.WordModels;

namespace Application.Common.Words.Queries;

public static class GetSpinWords
{
    public record Query : IRequest<IEnumerable<WordModel>>
    {
        public int FromIndex { get; set; }
        public int ToIndex { get; set; }
        public bool IsOriginalMode { get; set; }
        public bool IsTranslationMode { get; set; }
        public Category? SelectedCategory { get; set; }

        public bool IsValid() => FromIndex > 0;
        
        public class Handler : IRequestHandler<Query, IEnumerable<WordModel>>
        {
            private readonly IWordsRepository _repository;
            private readonly IConfigurator<LocalizationConfig> _configuration;

            public Handler(IWordsRepository repository, IConfigurator<LocalizationConfig> configuration)
            {
                _repository = repository;
                _configuration = configuration;
            }

            public async Task<IEnumerable<WordModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                int needSkip = request.FromIndex - 1;
                int needTake = request.ToIndex - needSkip;

                var languages = await _configuration.Get(cancellationToken);
                
                IEnumerable<Word> words;
                if (request.SelectedCategory.HasValue)
                {
                    words = await _repository.TakeAsync(needTake, needSkip, request.SelectedCategory.Value, cancellationToken);
                }
                else
                {
                    words = await _repository.TakeAsync(needTake, needSkip, cancellationToken);
                }
                
                
                List<WordModel> result = new();

                int index = request.FromIndex;
                foreach (Word word in words)
                {
                    if (request.IsOriginalMode)
                    {
                        result.Add(new WordModel
                        {
                            Id = word.Id,
                            Index = index,
                            Original = word.Original,
                            Translation = word.Translation,
                            Description = word.Description,
                            OrignalLanguage = languages.OriginLang,
                            TranslationLanguage = languages.TranslateLang,
                            ArticleType = word.ArticleType,
                            Category = word.Category ?? Category.Unknown
                        });
                    }          
                    
                    if (request.IsTranslationMode)
                    {
                        result.Add(new WordModel
                        {
                            Id = word.Id,
                            Index = index,
                            Original = word.Translation,
                            Translation = word.Original,
                            Description = word.Description,
                            OrignalLanguage = languages.TranslateLang,
                            TranslationLanguage = languages.OriginLang,
                            ArticleType = word.ArticleType,
                            Category = word.Category ?? Category.Unknown
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

            RuleFor(q => q.FromIndex)
                .GreaterThanOrEqualTo(min)
                .LessThanOrEqualTo(q => q.FromIndex);

            RuleFor(q => q.ToIndex)
                .GreaterThanOrEqualTo(q => q.FromIndex)
                .LessThanOrEqualTo(p => p.SelectedCategory.HasValue 
                    ? repository.Count(p.SelectedCategory.Value) 
                    : repository.Count());

        }
    }
}

