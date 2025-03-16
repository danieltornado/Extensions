using JetBrains.Annotations;

namespace Extensions;

[PublicAPI]
public static class ArrayExtensions
{
    public static T[] EmptyArrayIfNull<T>(this T[]? source)
    {
        return source ?? Array.Empty<T>();
    }
}