using VocabularySheet.Common;
using VocabularySheet.Common.Extensions;
using VocabularySheet.ML.Client;

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

internal class MlDataSets
{
    private readonly IMlDatasetsFolder _folder;

    public MlDataSets(IMlDatasetsFolder folder)
    {
        _folder = folder;
    }
    
    
    public HashSet<string> Get300EnWords()
    {
        var file = _folder.GetFilePath("en1000words.json");
        var list = Json.Camel.Deserialize<EnWordsPopular1000>(file.Content);

        return list?.Words.OrderBy(x => x.Rank).Take(300).Select(w => w.EnglishWord.ToLowerInvariant()).ToHashSet() ?? [];
    }
    
    private string[] GetFilesContents(Dictionary<string, AppFolderEntry> folders, string path, HashSet<string> stopWords)
    {
        string[] files = _folder.GetFilesPath(folders[path].Path).SelectMany(x => x.Value.Content.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries))
            .Select(x => string.Join(" ", x
                    .RemoveEscapes()
                    .RemovePunctuations()
                    .RemoveNumbers()
                    .RemoveEscapes()
                    .RemoveZeroSymbol()
                    .ToLowerInvariant()
                    .ReplaceDoubleSpaces()
                    .Split(" ")
                    .Where(w => !stopWords.Contains(w))
                    .Where(w => w.Length > 2)
                )
            ).OrderRandom().ToArray();

        return files;
    }
    
    public List<MlArticleRecord> GetArticleDataSet()
    {
        var stopWords = Get300EnWords();
        Dictionary<string, AppFolderEntry> folders = _folder.GetFoldersPath("document-classification");
        var files = new MlArticlesFiles
        {
            Business = GetFilesContents(folders, "business", stopWords),
            Entertainment = GetFilesContents(folders, "entertainment", stopWords),
            Food = GetFilesContents(folders, "food", stopWords),
            Graphics = GetFilesContents(folders, "graphics", stopWords),
            Historical = GetFilesContents(folders, "historical", stopWords),
            Medical = GetFilesContents(folders, "medical", stopWords),
            Politics = GetFilesContents(folders, "politics", stopWords),
            Sport = GetFilesContents(folders, "sport", stopWords),
            Technologie = GetFilesContents(folders, "technologie", stopWords),
            Religion = GetFilesContents(folders, "religion", stopWords),
        };

        return
        [
            ..files.Business.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Business
            }),
            ..files.Entertainment.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Entertainment
            }),
            ..files.Food.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Food
            }),
            ..files.Graphics.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Graphics
            }),
            ..files.Historical.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Historical
            }),
            ..files.Medical.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Medical
            }),
            ..files.Politics.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Politics
            }),
            ..files.Sport.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Sport
            }),
            ..files.Technologie.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Technologie
            }),
            ..files.Religion.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Religion
            }),
        ];
    }

}