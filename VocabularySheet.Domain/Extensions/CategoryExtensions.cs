namespace VocabularySheet.Domain.Extensions;

public static class CategoryExtensions
{
    public static Category MapToCategoryEnum(this string category)
    {
        category = category.Trim().ToLower();

        return category switch
        {
            "red" => Category.Red,
            "green" => Category.Green,
            "yellow" => Category.Yellow,
            "orange" => Category.Orange,
            "purple" => Category.Purple,
            "pink" => Category.Pink,
            _ => Category.Unknown
        };
    }
}