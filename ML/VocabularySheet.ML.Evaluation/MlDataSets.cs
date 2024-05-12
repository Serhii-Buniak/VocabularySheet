using System.Text.RegularExpressions;
using Catalyst;
using Mosaik.Core;
using VocabularySheet.Common;
using VocabularySheet.Common.Extensions;
using VocabularySheet.ML.Client;
using Catalyst.Models;

namespace VocabularySheet.ML.Evaluation;

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
        
        var list = Json.Camel.Deserialize<EnWordsPopular1000>(file.Content);
        var stop = Json.Camel.Deserialize<List<string>>(stopFile.Content) ?? [];
        var stop2 = Json.Camel.Deserialize<List<string>>(stop2File.Content) ?? [];

        var words = list?.Words.OrderBy(x => x.Rank).Take(300).Select(w => w.EnglishWord).ToHashSet() ?? [];

        HashSet<string> result = [..words, ..stop, ..stop2];

        return result.Select(x => x.ToLowerInvariant()).ToHashSet();
    }
    
    private async Task<string[]> GetFilesContents(Dictionary<string, AppFolderEntry> folders, string path, HashSet<string> stopWords)
    {
        string[] files = _folder.GetFilesPath(folders[path].Path).SelectMany(x => x.Value.Content.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries))
            .Select(x => string.Join(" ", x
                    .RemoveEmails()
                    .RemoveHtmlTags()
                    .RemovePunctuations(" ")
                    .RemoveNumbers(" ")
                    .RemoveZeroSymbol(" ")
                    .KeepOnlyLettersAndSpaces()
                    .ToLowerInvariant()
                    .ReplaceDoubleSpaces()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Where(w => !stopWords.Contains(w))
                    .Select(w => w.Trim().Lemmatize())
                    .Where(w => !stopWords.Contains(w))
                    .Where(w => w.Length > 2)
                )
            ).Distinct().ToArray();

        return (await files.AdjustWordsCount()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
    }
    
    public async Task<List<MlArticleRecord>> GetArticleDataSet()
    {
        var stopWords = GetEnStopWords();
        Dictionary<string, AppFolderEntry> folders = _folder.GetFoldersPath("document-classification");
        var files = new MlArticlesFiles
        {
            Sport = await GetFilesContents(folders, "sport", stopWords),
            Science = await GetFilesContents(folders, "science", stopWords),
            Religion = await GetFilesContents(folders, "religion", stopWords),
            Politics = await GetFilesContents(folders, "politics", stopWords),
            Medical = await GetFilesContents(folders, "medical", stopWords),
            Historical = await GetFilesContents(folders, "historical", stopWords),
            Fantasy = await GetFilesContents(folders, "fantasy", stopWords),
            Economic = await GetFilesContents(folders, "economic", stopWords),
            Digital = await GetFilesContents(folders, "digital", stopWords),
            Culinary = await GetFilesContents(folders, "culinary", stopWords),
        };

        return
        [
            ..files.Sport.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Sport
            }),
            ..files.Science.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Science
            }),
            ..files.Religion.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Religion
            }),
            ..files.Politics.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Politics
            }),
            ..files.Medical.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Medical
            }),
            ..files.Historical.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Historical
            }),
            ..files.Fantasy.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Fantasy
            }),
            ..files.Economic.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Economic
            }),
            ..files.Digital.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Digital
            }),
            ..files.Culinary.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Culinary
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
        
        foreach (string wordToRemove in allWords.Where(x => x.Count > 30).Select(x => x.Word))
        {
            tasks.Add(Task.Run(() =>
            {
                var needToRemove = inputRef.Where(s => s.Input.Split(" ").Contains(wordToRemove)).Skip(30).ToList();
                foreach (var toRemove in needToRemove)
                {
                    toRemove.Input = string.Join(" ", toRemove.Input.Split(" ").Where(x => x != wordToRemove));
                }
            }));
        }
        await Task.WhenAll(tasks);
        return inputRef.Select(x => x.Input).ToArray();
    }
    
    public static string Lemmatize(this string text, Language language = Language.English)
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
