namespace VocabularySheet.Domain.Extensions;

public static class IEnumerableExtension
{
    private static Random RANDOM => new();

    public static List<T> OrderRandom<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(_ => RANDOM.Next()).ToList();
    }      
    
    public static T? Random<T>(this IEnumerable<T> source) where T : class
    {
        var list = source.ToList();
        int count = list.Count();
        if (count == 0)
        {
            return null;
        }

        int index = RANDOM.Next(0, count);
        return list.ElementAt(index);
    }   
    
    public static T? RandomValue<T>(this IEnumerable<T> source) where T : struct
    {
        var list = source.ToList();
        int count = list.Count();
        
        if (count == 0)
        {
            return null;
        }

        int index = RANDOM.Next(0, count);
        return list.ElementAt(index);
    }
    
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> list) where T : class
    {
        return list.Where(t => t != null).Select(t => t!);
    }
    
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> list) where T : struct
    {
        return list.Where(t => t != null).Select(t => t!.Value);
    }
}