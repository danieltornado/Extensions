using JetBrains.Annotations;

namespace Extensions;

[PublicAPI]
public static class ListExtensions
{
    extension<T>(List<T>? source)
    {
        /// <summary>
        /// Creates an empty <c>not null</c> collection, if <paramref name="source"/> is <see langword="null"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> EmptyListIfNull()
        {
            return source ?? new List<T>(0);
        }
    }
}