using System.Text.RegularExpressions;
using Catalyst;
using Catalyst.Models;
using ML.Predictor;
using Tools.Common.Extensions;
using Tools.Parsers;

namespace ML.Trainer;

internal record EnWordsPopular1000()
{
    public required List<EnWordPopular> Words
    {
        get;
        set;
    }
}
internal record EnWordPopular()
{
    public required int Rank
    {
        get;
        set;
    }
    
    public required string EnglishWord
    {
        get;
        set;
    }
}

internal record CountryRecord
{
    public required CountryNameRecord Name
    {
        get;
        init;
    }
}

internal record CountryNameRecord
{
    public required string Common
    {
        get;
        init;
    }
    
    public required string Official
    {
        get;
        init;
    }

    public HashSet<string> Names => [Common, Official];
}

internal record LanguageNameRecord
{
    public required string Full
    {
        get;
        init;
    }
}

internal class MlDataSets
{
    private readonly IMlDatasetsFolder _folder;

    public MlDataSets(IMlDatasetsFolder folder)
    {
        _folder = folder;
    }
    
    
    public HashSet<string> GetEnStopWords()
    {
        var file = _folder.GetFilePath("en1000words.json");
        var stopFile = _folder.GetFilePath("stop_words_english.json");
        var stop2File = _folder.GetFilePath("stop_words_english_2.json");
        
        var list = JsonParser.Camel.Deserialize<EnWordsPopular1000>(file.Content);
        var stop = JsonParser.Camel.Deserialize<List<string>>(stopFile.Content) ?? [];
        var stop2 = JsonParser.Camel.Deserialize<List<string>>(stop2File.Content) ?? [];

        var words = list?.Words.OrderBy(x => x.Rank).Take(300).Select(w => w.EnglishWord).ToHashSet() ?? [];

        HashSet<string> result = [..words, ..stop, ..stop2];

        return result.Select(x => x.ToLowerInvariant()).ToHashSet();
    }
    
    private string[] GetFilesContents(Dictionary<string, AppFolderEntry> folders, string path, HashSet<string> stopWords)
    {
        string[] files = _folder.GetFilesPath(folders[path].Path).SelectMany(x => x.Value.Content.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries))
            .Select(x => string.Join(" ", x
                    .RemoveEmails()
                    .RemoveHtmlTags()
                    .RemovePunctuations(" ")
                    .RemoveNumbers(" ")
                    .RemoveZeroSymbol(" ")
                    .KeepOnlyLettersAndSpaces(" ")
                    .ToLowerInvariant()
                    .ReplaceDoubleSpaces()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Where(w => !stopWords.Contains(w))
                    .Select(w => w.Trim().Lemmatize())
                    .Where(w => !stopWords.Contains(w))
                    .Where(w => w.Length > 2)
                )
            ).Distinct().ToArray();

        return (files).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
    }
    
    public async Task<List<MlArticleRecord>> GetArticleDataSet()
    {
        await Task.CompletedTask;
        var stopWords = GetEnStopWords();
        Dictionary<string, AppFolderEntry> folders = _folder.GetFoldersPath("document-classification");
        var files = new MlArticlesFiles
        {
            Sport =  GetFilesContents(folders, "sport", stopWords),
            Science =  GetFilesContents(folders, "science", stopWords),
            Religion =  GetFilesContents(folders, "religion", stopWords),
            Politics =  GetFilesContents(folders, "politics", stopWords),
            Medical =  GetFilesContents(folders, "medical", stopWords),
            Historical =  GetFilesContents(folders, "historical", stopWords),
            Fantasy =  GetFilesContents(folders, "fantasy", stopWords),
            Economic =  GetFilesContents(folders, "economic", stopWords),
            Digital =  GetFilesContents(folders, "digital", stopWords),
            Culinary =  GetFilesContents(folders, "culinary", stopWords),
        };

        return
        [
            ..files.Sport.Select(x => new MlArticleRecord
            {
                Features = x,
                Label = (int)ArticleType.Sport
            }),
            ..files.Science.Select(x => new MlArticleRecord
            {
                Features = x,
                Label = (int)ArticleType.Science
            }),
            ..files.Religion.Select(x => new MlArticleRecord
            {
                Features = x,
                Label = (int)ArticleType.Religion
            }),
            ..files.Politics.Select(x => new MlArticleRecord
            {
                Features = x,
                Label = (int)ArticleType.Politics
            }),
            ..files.Medical.Select(x => new MlArticleRecord
            {
                Features = x,
                Label = (int)ArticleType.Medical
            }),
            ..files.Historical.Select(x => new MlArticleRecord
            {
                Features = x,
                Label = (int)ArticleType.Historical
            }),
            ..files.Fantasy.Select(x => new MlArticleRecord
            {
                Features = x,
                Label = (int)ArticleType.Fantasy
            }),
            ..files.Economic.Select(x => new MlArticleRecord
            {
                Features = x,
                Label = (int)ArticleType.Economic
            }),
            ..files.Digital.Select(x => new MlArticleRecord
            {
                Features = x,
                Label = (int)ArticleType.Digital
            }),
            ..files.Culinary.Select(x => new MlArticleRecord
            {
                Features = x,
                Label = (int)ArticleType.Culinary
            }),
        ];
    }

}

public static class StringDataSetsExtensions
{
    static StringDataSetsExtensions()
    {
        English.Register();
    }

    private record Wrap
    {
        public required string Input
        {
            get;
            set;
        }
    };
    
    public static async Task<string[]> AdjustWordsCount(this string[] input)
    {
        var inputRef = input.Select(x => new Wrap
        {
            Input = x
        }).ToList();
        
        if (input.Length == 0)
        {
            return input;
        }

        // Find all distinct words in all strings
        var allWords = input.SelectMany(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .GroupBy(x => x)
            .Select(x => new
            {
                Word = x.Key,
                Count = x.Count()
            }).ToList();

        List<Task> tasks = [];
        
        foreach (string wordToRemove in allWords.Where(x => x.Count > 20).Select(x => x.Word))
        {
            tasks.Add(Task.Run(() =>
            {
                var needToRemove = inputRef.Where(s => s.Input.Split(" ").Contains(wordToRemove)).Skip(20).ToList();
                foreach (var toRemove in needToRemove)
                {
                    toRemove.Input = string.Join(" ", toRemove.Input.Split(" ").Where(x => x != wordToRemove));
                }
            }));
        }
        await Task.WhenAll(tasks);
        return inputRef.Select(x => x.Input).ToArray();
    }
    
    public static string Lemmatize(this string text, Mosaik.Core.Language language = Mosaik.Core.Language.English)
    {
        var lemetizer = LemmatizerStore.Get(language);
        var token = new SingleToken(text, language);   
        return lemetizer.GetLemma(token);
    }
    
    public static string RemoveEmails(this string input)
    {
        // Regular expression pattern to match email addresses
        string pattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b";
        return Regex.Replace(input, pattern, " ");
    }
    
    public static string RemoveHtmlTags(this string input)
    {
        // Regular expression pattern to match HTML tags
        string pattern = @"<[^>]+>|&nbsp;";
        return Regex.Replace(input, pattern, " ");
    }
}
