using System.Collections;

namespace Extensions.Tests.TreeWork;

public sealed class CustomEnumerable : IEnumerable<TreeItem>
{
    private readonly IEnumerable<TreeItem> _collection;
    private readonly Counter _counter;

    public CustomEnumerable(IEnumerable<TreeItem> collection, Counter counter)
    {
        _collection = collection;
        _counter = counter;
    }

    public IEnumerator<TreeItem> GetEnumerator()
    {
        return new CustomEnumerator(_collection, _counter);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}