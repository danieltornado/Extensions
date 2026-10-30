using JetBrains.Annotations;

namespace Extensions;

[PublicAPI]
public static class ObjectExtensions
{
    extension<T>(T source)
    {
        /// <summary>
        /// Checks that the value is one of the parameters
        /// </summary>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Uses the Default Comparer</remarks>
        public bool OneOf(T value, params T[] values)
        {
            var collection = Enumerable.Repeat(value, 1).Concat(values.EmptyIfNull());
            return collection.Contains(source);
        }

        

        /// <summary>
        /// Creates <see cref="Task{TResult}"/> with a result
        /// </summary>
        /// <returns></returns>
        public Task<T> AsTask()
        {
            return Task.FromResult(source);
        }
    }

    extension<T>(T source) where T : IComparable<T>
    {
        /// <summary>
        /// Checks that the value falls within the range (inclusive)
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public bool Between(T lower, T upper)
        {
            return source.CompareTo(lower) >= 0 && source.CompareTo(upper) <= 0;
        }
    }
}