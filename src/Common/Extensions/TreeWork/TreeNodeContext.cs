namespace Extensions.TreeWork;

public struct TreeNodeContext<T>
{
    /// <summary>
    /// Gets current level
    /// </summary>
    public int Level { get; internal set; }

    /// <summary>
    /// Gets value
    /// </summary>
    public T Value { get; internal set; }

    /// <summary>
    /// Gets parent
    /// </summary>
    public T? Parent { get; internal set; }
}