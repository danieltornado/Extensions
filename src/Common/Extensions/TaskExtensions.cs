namespace Extensions;

public static class TaskExtensions
{
    /// <summary>
    /// Creates <see cref="Task{TResult}"/> with a result
    /// </summary>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Task<T> AsTask<T>(this T value)
    {
        return Task.FromResult(value);
    }

    /// <summary>
    /// Tries to convert to <see cref="Array"/> asynchronously
    /// </summary>
    /// <param name="task"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Task<T[]> AsArrayAsync<T>(this Task<IEnumerable<T>> task)
    {
        return task.ContinueWith(e => e.ThrowIfNeeded().AsArray());
    }

    /// <summary>
    /// Tries to convert to <see cref="List{T}"/> asynchronously
    /// </summary>
    /// <param name="task"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Task<List<T>> AsListAsync<T>(this Task<IEnumerable<T>> task)
    {
        return task.ContinueWith(e => e.ThrowIfNeeded().AsList());
    }

    /// <summary>
    /// Tries to convert to <see cref="IReadOnlyList{T}"/> asynchronously
    /// </summary>
    /// <param name="task"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Task<IReadOnlyList<T>> AsReadOnlyListAsync<T>(this Task<IEnumerable<T>> task)
    {
        return task.ContinueWith(e => e.ThrowIfNeeded().AsReadOnlyList());
    }

    /// <summary>
    /// Tries to convert to <see cref="IReadOnlyCollection{T}"/> asynchronously
    /// </summary>
    /// <param name="task"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Task<IReadOnlyCollection<T>> AsReadOnlyCollectionAsync<T>(this Task<IEnumerable<T>> task)
    {
        return task.ContinueWith(e => e.ThrowIfNeeded().AsReadOnlyCollection());
    }

    private static IEnumerable<T> ThrowIfNeeded<T>(this Task<IEnumerable<T>> task)
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