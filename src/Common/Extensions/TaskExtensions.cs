using JetBrains.Annotations;

namespace Extensions;

[PublicAPI]
public static class TaskExtensions
{
    extension<T>(Task<IEnumerable<T>> task)
    {
        /// <summary>
        /// Tries to convert to <see cref="Array"/> asynchronously
        /// </summary>
        /// <returns></returns>
        public Task<T[]> AsArrayAsync()
        {
            return task.ContinueWith(e => e.ThrowIfNeeded().AsArray());
        }

        /// <summary>
        /// Tries to convert to <see cref="List{T}"/> asynchronously
        /// </summary>
        /// <returns></returns>
        public Task<List<T>> AsListAsync()
        {
            return task.ContinueWith(e => e.ThrowIfNeeded().AsList());
        }

        /// <summary>
        /// Tries to convert to <see cref="IReadOnlyList{T}"/> asynchronously
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyList<T>> AsReadOnlyListAsync()
        {
            return task.ContinueWith(e => e.ThrowIfNeeded().AsReadOnlyList());
        }

        /// <summary>
        /// Tries to convert to <see cref="IReadOnlyCollection{T}"/> asynchronously
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyCollection<T>> AsReadOnlyCollectionAsync()
        {
            return task.ContinueWith(e => e.ThrowIfNeeded().AsReadOnlyCollection());
        }

        private IEnumerable<T> ThrowIfNeeded()
        {
            if (task.Exception != null)
            {
                if (task.Exception.InnerException != null)
                    throw task.Exception.InnerException;

                if (task.Exception.InnerExceptions.Count > 0)
                    throw task.Exception.InnerExceptions[0];

                throw task.Exception;
            }

            if (task.Status == TaskStatus.Canceled)
                throw new TaskCanceledException(task);

            return task.Result;
        }
    }
}