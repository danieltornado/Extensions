using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Extensions;

[PublicAPI]
public static class AsyncEnumerableExtensions
{
    extension<T>(IAsyncEnumerable<T> source)
    {
        /// <summary>
        /// Builds batches from <paramref name="source"/>
        /// </summary>
        /// <param name="selector">Batch-element factory</param>
        /// <param name="batchSize">Max of elements in the batch</param>
        /// <param name="ct"></param>
        /// <typeparam name="TBatch"></typeparam>
        /// <returns>Collection of batches</returns>
        public async IAsyncEnumerable<TBatch> BatchAsync<TBatch>(Func<T[], TBatch> selector, int batchSize, [EnumeratorCancellation] CancellationToken ct)
        {
            await using var sourceEnumerator = source.GetAsyncEnumerator(ct);
            
            if (await sourceEnumerator.MoveNextAsync())
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
                } while (await sourceEnumerator.MoveNextAsync());

                if (index > 0)
                {
                    Array.Resize(ref batch, index);
                    yield return selector(batch);
                }
            }
        }
    }
}