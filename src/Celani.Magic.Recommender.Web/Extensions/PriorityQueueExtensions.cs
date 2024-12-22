namespace Celani.Magic.Recommender.Web.Extensions;

public static class PriorityQueueExtensions
{
    public static IEnumerable<(T1, T2)> DequeueN<T1, T2>(this PriorityQueue<T1, T2> items, int n)
    {
        for (var i = 0; i < n; i++)
        {
            if (!items.TryDequeue(out var value, out var score))
                break;

            yield return (value, score);
        }
    }
}
