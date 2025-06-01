using System.Collections;

namespace Extensions.TreeWork;

internal sealed class TreeEnumerable<T> : IEnumerable<TreeNode<T>>
{
    private readonly IEnumerable<T> _collection;
    private readonly Func<T, IEnumerable<T>> _getChildren;

    public TreeEnumerable(IEnumerable<T> collection, Func<T, IEnumerable<T>> getChildren)
    {
        _collection = collection;
        _getChildren = getChildren;
    }

    public IEnumerator<TreeNode<T>> GetEnumerator()
    {
        return new TreeEnumerator<T>(_collection, _getChildren);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}