using System.Collections;

namespace Extensions.TreeWork;

internal sealed class TreeEnumerator<T> : IEnumerator<TreeNode<T>>
{
    private readonly Func<T, IEnumerable<T>> _getChildren;
    private readonly Stack<IEnumerator<T>> _callstack;
    private TreeNode<T> _current;

    /// <summary>
    /// State of enumerator: 0 - no code after yield, 1 - execute code after yield
    /// </summary>
    private byte _state;

    public TreeEnumerator(IEnumerable<T> collection, Func<T, IEnumerable<T>> getChildren)
    {
        _callstack = new Stack<IEnumerator<T>>();
        _getChildren = getChildren;

        var head = collection.GetEnumerator();
        _callstack.Push(head);

        _state = 0;
    }

    public bool MoveNext()
    {
        switch (_state)
        {
            // При первом выполнении пропускается

            // Code after yield
            case 1:
                // Get childs and push enumerator to stack
                var current = _callstack.Peek();
                var collection = _getChildren(current.Current);

                var child = collection.GetEnumerator();
                _callstack.Push(child);

                // Set no code after yield
                _state = 0;
                break;
        }

        while (_callstack.Count > 0)
        {
            // All magic here

            // Continue
            var current = _callstack.Pop();
            if (current.MoveNext())
            {
                // Return enumerator back to stack
                _callstack.Push(current);

                _state = 1;
                _current.Level = _callstack.Count - 1;
                _current.Value = current.Current;
                return true;

                // Code after yield
            }

            // Set no code after yield
            _state = 0;

            // End of collection
            current.Dispose();
        }

        _current.Level = 0;
        // _current.Value = default;
        return false;
    }

    public TreeNode<T> Current => _current;

    object IEnumerator.Current => Current;

    public void Reset()
    {
        throw new NotSupportedException();
    }

    void IDisposable.Dispose()
    {
        while (_callstack.Count > 0)
        {
            var current = _callstack.Pop();
            current.Dispose();
        }
    }
}