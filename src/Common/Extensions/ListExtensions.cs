using JetBrains.Annotations;

namespace Extensions;

[PublicAPI]
public static class ListExtensions
{
    /// <summary>
    /// Creates an empty <c>not null</c> collection, if <paramref name="source"/> is <see langword="null"/>
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> EmptyListIfNull<T>(this List<T>? source)
    {
        return source ?? new List<T>(0);
    }
}