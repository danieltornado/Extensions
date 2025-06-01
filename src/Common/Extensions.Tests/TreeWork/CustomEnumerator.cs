using System.Collections;

namespace Extensions.Tests.TreeWork;

public class CustomEnumerator : IEnumerator<TreeItem>
{
    private readonly IEnumerator<TreeItem> _externalEnumerator;
    private readonly Counter _counter;

    public CustomEnumerator(IEnumerable<TreeItem> externalCollection, Counter counter)
    {
        _externalEnumerator = externalCollection.GetEnumerator();
        _counter = counter;
    }

    public bool MoveNext()
    {
        return _externalEnumerator.MoveNext();
    }

    public void Reset()
    {
        _externalEnumerator.Reset();
    }

    public TreeItem Current => _externalEnumerator.Current;

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _externalEnumerator.Dispose();
        _counter.DisposeCount++;
    }
}