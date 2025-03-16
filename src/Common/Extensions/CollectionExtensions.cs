using JetBrains.Annotations;

namespace Extensions;

[PublicAPI]
public static class CollectionExtensions
{
    /// <summary>
    /// Safely generates a new <see cref="List{T}"/> with optimized <c>capacity</c> transfer. If <paramref name="collection"/> is <see langword="null"/>, <see langword="null"/> is returned. 
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="selector"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T>? SelectListSafe<TSource, T>(this IReadOnlyCollection<TSource>? collection, Func<TSource, T> selector)
    {
        if (collection == null)
            return null;

        return collection.SelectList(collection.Count, selector);
    }

    /// <summary>
    /// Generates a new <see cref="List{T}"/> with optimized <c>capacity</c> transfer 
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="selector"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> SelectList<TSource, T>(this IReadOnlyCollection<TSource> collection, Func<TSource, T> selector)
    {
        return collection.SelectList(collection.Count, selector);
    }

    // ICollection is not using to avoid creating conditions for using the collection
    // because the interface IReadOnlyCollection is not a subset of the ICollection.

    private static List<T> SelectList<TSource, T>(this IEnumerable<TSource> collection, int capacity, Func<TSource, T> selector)
    {
        var result = new List<T>(capacity);
        foreach (var item in collection)
        {
            result.Add(selector(item));
        }

        return result;
    }
}