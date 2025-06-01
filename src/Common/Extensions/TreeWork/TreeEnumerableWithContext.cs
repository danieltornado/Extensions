using System.Collections;

namespace Extensions.TreeWork;

internal sealed class TreeEnumerableWithContext<T> : IEnumerable<TreeNodeContext<T>>
{
    private readonly IEnumerable<T> _collection;
    private readonly Func<T, IEnumerable<T>> _getChildren;

    public TreeEnumerableWithContext(IEnumerable<T> collection, Func<T, IEnumerable<T>> getChildren)
    {
        _collection = collection;
        _getChildren = getChildren;
    }

    public IEnumerator<TreeNodeContext<T>> GetEnumerator()
    {
        return new TreeEnumeratorWithContext<T>(_collection, _getChildren);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}