namespace Domain.WordModels;

public enum Category
{
    Unknown = 0,
    Red = 1,
    Green = 2,
    Yellow = 3,
    Orange = 4,
    Purple = 5,
    Pink = 6,
}

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