namespace Extensions;

public static class EnumerableExtensions
{
    public static T[]? AsArray<T>(this IEnumerable<T>? source)
    {
        if (source == null)
            return null;

        return source as T[] ?? source.ToArray();
    }

    public static async Task<T[]?> AsArrayAsync<T>(this Task<IEnumerable<T>?> task)
    {
        var result = await task.ConfigureAwait(false);
        return result.AsArray();
    }

    public static List<T>? AsList<T>(this IEnumerable<T>? source)
    {
        if (source == null)
            return null;

        return source as List<T> ?? source.ToList();
    }

    public static async Task<List<T>?> AsListAsync<T>(this Task<IEnumerable<T>?> task)
    {
        var result = await task.ConfigureAwait(false);
        return result.AsList();
    }

    public static IReadOnlyList<T>? AsReadOnlyList<T>(this IEnumerable<T>? source)
    {
        if (source == null)
            return null;

        return source as IReadOnlyList<T> ?? source.ToList();
    }

    public static async Task<IReadOnlyList<T>?> AsReadOnlyListAsync<T>(this Task<IEnumerable<T>?> task)
    {
        var result = await task.ConfigureAwait(false);
        return result.AsReadOnlyList();
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

    public static T[] EmptyIfNull<T>(this T[]? source)
    {
        return source ?? Array.Empty<T>();
    }

    public static List<T> EmptyIfNull<T>(this List<T>? source)
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

