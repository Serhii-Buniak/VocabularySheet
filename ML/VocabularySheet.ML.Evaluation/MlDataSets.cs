using VocabularySheet.Common;
using VocabularySheet.ML.Client;

namespace VocabularySheet.ML.Evaluation;

internal static class MlDataSets
{
    public static List<MlArticleRecord> Article { get; } = GetArticleDataSet();

    private static string[] GetFilesContents(Dictionary<string, AppFolderEntry> folders, string path)
    {
        return MlDatasetsFolder.GetFilesPath(folders[path].Path).Select(x => x.Value.Content).ToArray();
    }
    
    private static List<MlArticleRecord> GetArticleDataSet()
    {
        Dictionary<string, AppFolderEntry> folders = MlDatasetsFolder.GetFoldersPath("document-classification");
        var files = new MlArticlesFiles
        {
            Business = GetFilesContents(folders, "business"),
            Entertainment = GetFilesContents(folders, "entertainment"),
            Food = GetFilesContents(folders, "food"),
            Graphics = GetFilesContents(folders, "graphics"),
            Historical = GetFilesContents(folders, "historical"),
            Medical = GetFilesContents(folders, "medical"),
            Politics = GetFilesContents(folders, "politics"),
            Space = GetFilesContents(folders, "space"),
            Sport = GetFilesContents(folders, "sport"),
            Technologie = GetFilesContents(folders, "technologie"),
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
            ..files.Space.Select(x => new MlArticleRecord
            {
                Text = x,
                Type = (int)ArticleType.Space
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
        ];
    }

}