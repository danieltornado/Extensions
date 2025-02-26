using System.Diagnostics.CodeAnalysis;

namespace Extensions;

public static class EnumerableExtensions
{
    /// <summary>
    /// Tries to convert to <see cref="Array"/> or creates <see cref="Array"/>
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T[] AsArray<T>(this IEnumerable<T> source)
    {
        return source as T[] ?? source.ToArray();
    }

    /// <summary>
    /// Tries to convert to <see cref="List{T}"/> or creates <see cref="List{T}"/>
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> AsList<T>(this IEnumerable<T> source)
    {
        return source as List<T> ?? source.ToList();
    }

    /// <summary>
    /// Tries to convert to <see cref="IReadOnlyList{T}"/> or creates <see cref="List{T}"/>
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IReadOnlyList<T> AsReadOnlyList<T>(this IEnumerable<T> source)
    {
        return source as IReadOnlyList<T> ?? source.ToList();
    }

    /// <summary>
    /// Tries to convert to <see cref="IReadOnlyCollection{T}"/> or creates <see cref="List{T}"/>
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IReadOnlyCollection<T> AsReadOnlyCollection<T>(this IEnumerable<T> source)
    {
        return source as IReadOnlyCollection<T> ?? source.ToList();
    }

    public static T? MinOrDefault<T>(this IEnumerable<T> source, T? defaultValue)
    {
        return source.DefaultIfEmpty(defaultValue).Min();
    }

    public static T1? MinOrDefault<T, T1>(this IEnumerable<T> source, Func<T, T1?> selector, T1? defaultValue)
    {
        return source.Select(selector).DefaultIfEmpty(defaultValue).Min();
    }

    public static T? MaxOrDefault<T>(this IEnumerable<T> source, T? defaultValue)
    {
        return source.DefaultIfEmpty(defaultValue).Max();
    }

    public static T1? MaxOrDefault<T, T1>(this IEnumerable<T> source, Func<T, T1?> selector, T1? defaultValue)
    {
        return source.Select(selector).DefaultIfEmpty(defaultValue).Max();
    }

    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source)
    {
        return source ?? Enumerable.Empty<T>();
    }

    public static T[] EmptyArrayIfNull<T>(this T[]? source)
    {
        return source ?? Array.Empty<T>();
    }

    public static List<T> EmptyListIfNull<T>(this List<T>? source)
    {
        return source ?? new List<T>(0);
    }

    /// <summary>
    /// Creates flat collection from object's graph
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="getChildren"></param>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "GenericEnumeratorNotDisposed")]
    public static IEnumerable<T> Flatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>?> getChildren)
    {
        var stack = new Stack<IEnumerator<T>>(2);
        stack.Push(source.GetEnumerator());

        while (stack.Count > 0)
        {
            var currentEnumerator = stack.Pop();
            if (currentEnumerator.MoveNext())
            {
                stack.Push(currentEnumerator);

                var currentElement = currentEnumerator.Current;

                yield return currentElement;

                var children = getChildren(currentElement);
                if (children != null)
                {
                    var childrenEnumerator = children.GetEnumerator();
                    stack.Push(childrenEnumerator);
                    continue;
                }
            }

            currentEnumerator.Dispose();
        }
    }
}