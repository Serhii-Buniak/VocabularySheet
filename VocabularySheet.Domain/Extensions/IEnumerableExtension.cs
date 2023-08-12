namespace VocabularySheet.Domain.Extensions;

public static class IEnumerableExtension
{
    private static readonly Random _random = new();

    public static T? Random<T>(this IEnumerable<T> source)
    {
        int count = source.Count();

        if (count == 0)
        {
            return default;
        }

        int index = _random.Next(0, count);
        return source.ElementAt(index);
    }
}
