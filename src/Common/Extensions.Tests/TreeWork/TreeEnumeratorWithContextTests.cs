using Extensions.TreeWork;
using Xunit;

namespace Extensions.Tests.TreeWork;

public sealed class TreeEnumeratorWithContextTests
{
    [Fact]
    public void WhenSeveralItems_Enumerator_ShouldShowSameTree()
    {
        var items = TreeItemHelper.GenerateForParentCheck();

        // classic method
        var recursiveTree = GetSubLevel(0, items);

        // no recursive method
        var noRecursiveTree = new List<TreeItem>();

        using (var enumerator = new TreeEnumeratorWithContext<TreeItem>(items.Where(a => a.ParentId == 0), item => items.Where(a => a.ParentId == item.Id).OrderBy(a => a.Id)))
        {
            while (enumerator.MoveNext())
            {
                var treeItem = enumerator.Current.Value;

                if (enumerator.Current.Parent != null)
                {
                    enumerator.Current.Parent.Childs.Add(treeItem);
                }
                else
                {
                    noRecursiveTree.Add(treeItem);
                }
            }
        }

        CompareTrees(recursiveTree, noRecursiveTree);
    }

    [Fact]
    public void WhenSeveralItems_EnumeratorWithContext_ShouldShowSameTree()
    {
        var items = TreeItemHelper.GenerateForParentCheck();

        // classic method
        var recursiveTree = GetSubLevel(0, items);

        // no recursive method
        var noRecursiveTree = new List<TreeItem>();

        foreach (TreeNodeContext<TreeItem> node in items.Where(a => a.ParentId == 0).AsTreeWithContext(item => items.Where(a => a.ParentId == item.Id).OrderBy(a => a.Id)))
        {
            var treeItem = node.Value;

            if (node.Parent != null)
            {
                node.Parent.Childs.Add(treeItem);
            }
            else
            {
                noRecursiveTree.Add(treeItem);
            }
        }

        CompareTrees(recursiveTree, noRecursiveTree);
    }

    private void CompareTrees(List<TreeItem> firstTree, List<TreeItem> secondTree, int level = 0)
    {
        Assert.Equal(firstTree.Count, secondTree.Count);

        for (int i = 0; i < firstTree.Count; i++)
        {
            var first = firstTree[i];
            var second = secondTree[i];

            Assert.Equal(first.Id, second.Id); // xUnit

            System.Diagnostics.Debug.WriteLine(first.ToString(level));

            CompareTrees(first.Childs, second.Childs, level + 1);
        }
    }

    private List<TreeItem> GetSubLevel(int parentId, List<TreeItem> originalData)
    {
        var result = new List<TreeItem>();
        foreach (var treeItem in originalData.Where(a => a.ParentId == parentId).OrderBy(a => a.Id))
        {
            var newItem = (TreeItem)treeItem.Clone();
            result.Add(newItem);
            newItem.Childs = GetSubLevel(treeItem.Id, originalData);
        }

        return result;
    }
}