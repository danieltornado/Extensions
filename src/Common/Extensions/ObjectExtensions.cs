using JetBrains.Annotations;

namespace Extensions;

[PublicAPI]
public static class ObjectExtensions
{
    /// <summary>
    /// Checks that the value is one of the parameters
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="values"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <remarks>Uses the Default Comparer</remarks>
    public static bool OneOf<T>(this T source, T value, params T[] values)
    {
        var collection = Enumerable.Repeat(value, 1).Concat(values.EmptyIfNull());
        return collection.Contains(source);
    }

    /// <summary>
    /// Checks that the value falls within the range (inclusive)
    /// </summary>
    /// <param name="source"></param>
    /// <param name="lower"></param>
    /// <param name="upper"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool Between<T>(this T source, T lower, T upper)
        where T : IComparable<T>
    {
        return source.CompareTo(lower) >= 0 && source.CompareTo(upper) <= 0;
    }
}