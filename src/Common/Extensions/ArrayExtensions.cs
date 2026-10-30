using JetBrains.Annotations;

namespace Extensions;

[PublicAPI]
public static class ArrayExtensions
{
    extension<T>(T[]? source)
    {
        public T[] EmptyArrayIfNull()
        {
            return source ?? Array.Empty<T>();
        }
    }
}