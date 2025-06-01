namespace Extensions.TreeWork;

public struct TreeNode<T>
{
    /// <summary>
    /// Gets current level
    /// </summary>
    public int Level { get; internal set; }

    /// <summary>
    /// Gets value
    /// </summary>
    public T Value { get; internal set; }
}