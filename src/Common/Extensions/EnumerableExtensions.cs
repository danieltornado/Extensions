using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Extensions;

[PublicAPI]
public static class EnumerableExtensions
{
    extension<T>(IEnumerable<T>? source)
    {
        public IEnumerable<T> EmptyIfNull()
        {
            return source ?? Enumerable.Empty<T>();
        }
    }

    extension<T>(IEnumerable<T> source)
    {
        /// <summary>
        /// Tries to convert to <see cref="Array"/> or creates <see cref="Array"/>
        /// </summary>
        /// <returns></returns>
        public T[] AsArray()
        {
            return source as T[] ?? source.ToArray();
        }

        /// <summary>
        /// Tries to convert to <see cref="List{T}"/> or creates <see cref="List{T}"/>
        /// </summary>
        /// <returns></returns>
        public List<T> AsList()
        {
            return source as List<T> ?? source.ToList();
        }

        /// <summary>
        /// Tries to convert to <see cref="IReadOnlyList{T}"/> or creates <see cref="List{T}"/>
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<T> AsReadOnlyList()
        {
            return source as IReadOnlyList<T> ?? source.ToList();
        }

        /// <summary>
        /// Tries to convert to <see cref="IReadOnlyCollection{T}"/> or creates <see cref="List{T}"/>
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<T> AsReadOnlyCollection()
        {
            return source as IReadOnlyCollection<T> ?? source.ToList();
        }

        public T? MinOrDefault(T? defaultValue)
        {
            return source.DefaultIfEmpty(defaultValue).Min();
        }

        public T1? MinOrDefault<T1>(Func<T, T1?> selector, T1? defaultValue)
        {
            return source.Select(selector).DefaultIfEmpty(defaultValue).Min();
        }

        public T? MaxOrDefault(T? defaultValue)
        {
            return source.DefaultIfEmpty(defaultValue).Max();
        }

        public T1? MaxOrDefault<T1>(Func<T, T1?> selector, T1? defaultValue)
        {
            return source.Select(selector).DefaultIfEmpty(defaultValue).Max();
        }


        /// <summary>
        /// Creates flat collection from object's graph
        /// </summary>
        /// <param name="getChildren"></param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "GenericEnumeratorNotDisposed")]
        public IEnumerable<T> Flatten(Func<T, IEnumerable<T>?> getChildren)
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

        /// <summary>
        /// Builds batches from <paramref name="source"/>
        /// </summary>
        /// <param name="selector">Batch-element factory</param>
        /// <param name="batchSize">Max of elements in the batch</param>
        /// <typeparam name="TBatch">Batch-element</typeparam>
        /// <returns>Collection of batches</returns>
        public IEnumerable<TBatch> Batch<TBatch>(Func<T[], TBatch> selector, int batchSize)
        {
            using var sourceEnumerator = source.GetEnumerator();
            if (sourceEnumerator.MoveNext())
            {
                var batch = new T[batchSize];
                var index = 0;

                do
                {
                    batch[index++] = sourceEnumerator.Current;

                    if (index == batchSize)
                    {
                        yield return selector(batch);

                        index = 0;
                    }
                } while (sourceEnumerator.MoveNext());

                if (index > 0)
                {
                    Array.Resize(ref batch, index);
                    yield return selector(batch);
                }
            }
        }
    }
}