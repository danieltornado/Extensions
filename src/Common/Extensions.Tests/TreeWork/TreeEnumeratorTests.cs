using Extensions.TreeWork;
using Xunit;

namespace Extensions.Tests.TreeWork;

public sealed class TreeEnumeratorTests
{
    [Fact]
    public void WhenSeveralItems_Dispose_ShouldBeCorrect()
    {
        var items = TreeItemHelper.GenerateForDisposeTest();

        var counter = new Counter();

        var head = new CustomEnumerable(items.Where(a => a.ParentId == 0), counter); // 1 call = 1 disposing
        IEnumerable<TreeItem> GetChildren(TreeItem item) => new CustomEnumerable(items.Where(a => a.ParentId == item.Id).OrderBy(a => a.Id), counter); // 4 elements = 5 calls = 5 disposing (try get subelements at Id=5)

        using (var enumerator = new TreeEnumerator<TreeItem>(head, GetChildren))
        {
            while (enumerator.MoveNext())
            {
            }
        }

        // total 6 calls
        Assert.Equal(6, counter.DisposeCount);
    }

    [Fact]
    public void WhenSeveralItems_Reset_ShouldThrowNotSupportedException()
    {
        var items = TreeItemHelper.GenerateForDisposeTest();

        var counter = new Counter();

        var head = new CustomEnumerable(items.Where(a => a.ParentId == 0), counter); // 1 call = 1 disposing
        IEnumerable<TreeItem> GetChildren(TreeItem item) => new CustomEnumerable(items.Where(a => a.ParentId == item.Id).OrderBy(a => a.Id), counter); // 4 elements = 5 calls = 5 disposing (try get subelements at Id=5)

        using (var enumerator = new TreeEnumerator<TreeItem>(head, GetChildren))
        {
            int i = 0;
            while (enumerator.MoveNext())
            {
                // i = 0 - current=head
                // i = 1 - current=sub 1 head 1
                // i = 2 - current=sub 1 head 2
                // i = 3 - current=sub 1 head 3
                // i = 4 - current=sub 1 head 4

                if (i == 4)
                {
                    // enumerator has stack with 5 elements
                    Assert.Throws<NotSupportedException>(() => enumerator.Reset());
                    break;
                }

                i++;
            }
        }
    }
}