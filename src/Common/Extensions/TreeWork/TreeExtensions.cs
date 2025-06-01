namespace Extensions.TreeWork;

public static class TreeExtensions
{
    public static IEnumerable<TreeNode<T>> AsTree<T>(this IEnumerable<T> topElements, Func<T, IEnumerable<T>> getChildren)
    {
        return new TreeEnumerable<T>(topElements, getChildren);
    }

    public static IEnumerable<TreeNodeContext<T>> AsTreeWithContext<T>(this IEnumerable<T> topElements, Func<T, IEnumerable<T>> getChildren)
    {
        return new TreeEnumerableWithContext<T>(topElements, getChildren);
    }
}