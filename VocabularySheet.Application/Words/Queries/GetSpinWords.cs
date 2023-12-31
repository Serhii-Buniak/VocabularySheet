﻿using VocabularySheet.Application.Commons.Dtos;
using VocabularySheet.Domain;
using VocabularySheet.Domain.ConfigEntities;

namespace VocabularySheet.Application.Words.Queries;

public static class GetSpinWords
{
    public record Query : IRequest<IEnumerable<WordModel>>
    {
        public int FromIndex { get; set; }
        public int ToIndex { get; set; }
        public bool IsOriginalMode { get; set; }
        public bool IsTranslationMode { get; set; }
        public Category? Category { get; set; }

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
                if (request.Category.HasValue)
                {
                    words = await _repository.TakeAsync(needTake, needSkip, request.Category.Value, cancellationToken);
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
                            TranslationlLanguage = languages.TranslateLang,
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
                            TranslationlLanguage = languages.OriginLang,
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
                .LessThanOrEqualTo(p => p.Category.HasValue 
                    ? repository.Count(p.Category.Value) 
                    : repository.Count());

        }
    }
}

