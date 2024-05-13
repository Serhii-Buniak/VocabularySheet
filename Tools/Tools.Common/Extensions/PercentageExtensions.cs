namespace Tools.Common.Extensions;

public interface IHasPercentage
{
    int Percentage { get; set; }
}

public static class PercentageExtensions
{
    public static List<T> AdjustPercentageTo100<T>(this List<T> list) where T : class, IHasPercentage
    {
        int sum = list.Sum(item => item.Percentage);
        if (sum == 100)
            return list; // No need for adjustment

        // Distribute the remaining difference proportionally among the elements of the list
        double factor = 100.0 / sum;
        foreach (var item in list)
        {
            item.Percentage = (int)Math.Round(item.Percentage * factor);
        }

        // Adjust the last item to make sure the sum is exactly 100
        list.Last().Percentage += 100 - list.Sum(item => item.Percentage);

        return list;
    }
    
}