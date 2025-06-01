using System.Collections;

namespace Extensions.TreeWork;

internal sealed class TreeEnumeratorWithContext<T> : IEnumerator<TreeNodeContext<T>>
{
    private readonly TreeEnumerator<T> _enumerator;
    private TreeNodeContext<T> _current;

    private readonly Stack<T> _contextStack;

    /// <summary>
    /// State of enumerator: 0 - no code after yield, 1 - execute code after yield
    /// </summary>
    private byte _state;

    public TreeEnumeratorWithContext(IEnumerable<T> collection, Func<T, IEnumerable<T>> getChildren)
    {
        _enumerator = new TreeEnumerator<T>(collection, getChildren);
        _contextStack = new Stack<T>();

        PushParent(_enumerator.Current.Value);

        _state = 0;
    }

    public bool MoveNext()
    {
        switch (_state)
        {
            case 1:
                // potential parent, add to stack
                PushParent(_enumerator.Current.Value);

                // Set new current level
                _current.Level = _enumerator.Current.Level;

                // no code after yield
                _state = 0;
                break;
        }

        while (_enumerator.MoveNext())
        {
            TreeNode<T> node = _enumerator.Current;
            if (_current.Level >= node.Level)
            {
                // Current level was not changed, stack contain wrong parent
                // Current level is up, stack contain two or more wrong parent
                PopParents(_current.Level - node.Level + 1);
            }

            _state = 1;
            _current.Value = node.Value;
            _current.Parent = _contextStack.Count > 0 ? _contextStack.Peek() : default(T);
            return true;

            // code after yield
        }

        _current.Level = 0;
        // _current.Value = default;
        _current.Parent = default;
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public TreeNodeContext<T> Current => _current;

    object IEnumerator.Current => Current;

    void IDisposable.Dispose()
    {
        ((IDisposable)_enumerator).Dispose();
        _contextStack.Clear();
    }

    private void PushParent(T item)
    {
        _contextStack.Push(item);
    }

    private void PopParents(int count)
    {
        while (count > 0)
        {
            _contextStack.Pop();
            count--;
        }
    }
}