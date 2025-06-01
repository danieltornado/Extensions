namespace Extensions.Tests.TreeWork;

public static class TreeItemHelper
{
    public static List<TreeItem> GenerateForParentCheck()
    {
        var items = new List<TreeItem>
        {
            new() { Id = 1, ParentId = 0, Name = "Head 1", Level = 0 },
            new() { Id = 2, ParentId = 0, Name = "Head 2", Level = 0 },
            new() { Id = 3, ParentId = 0, Name = "Head 3", Level = 0 },

            new() { Id = 4, ParentId = 3, Name = "Sub 1 head 3", Level = 1 },
            new() { Id = 5, ParentId = 3, Name = "Sub 2 head 3", Level = 1 },
            new() { Id = 6, ParentId = 3, Name = "Sub 3 head 3", Level = 1 },

            new() { Id = 7, ParentId = 6, Name = "Sub 1 sub 3", Level = 2 },
            new() { Id = 8, ParentId = 6, Name = "Sub 2 sub 3", Level = 2 },
            new() { Id = 9, ParentId = 6, Name = "Sub 3 sub 3", Level = 2 },

            new() { Id = 10, ParentId = 9, Name = "Sub 1 sub 3 sub 3", Level = 3 },
            new() { Id = 11, ParentId = 9, Name = "Sub 2 sub 3 sub 3", Level = 3 },
            new() { Id = 12, ParentId = 9, Name = "Sub 3 sub 3 sub 3", Level = 3 },

            new() { Id = 13, ParentId = 0, Name = "Head 4", Level = 0 },

            new() { Id = 14, ParentId = 1, Name = "Sub 4 head 1", Level = 1 },
        };

        return items;
    }

    public static List<TreeItem> GenerateForDisposeTest()
    {
        var items = new List<TreeItem>
        {
            new() { Id = 1, ParentId = 0, Name = "Head 1" },
            new() { Id = 2, ParentId = 1, Name = "Sub 1 Head 1" },
            new() { Id = 3, ParentId = 2, Name = "Sub 1 Head 2" },
            new() { Id = 4, ParentId = 3, Name = "Sub 1 Head 3" },
            new() { Id = 5, ParentId = 4, Name = "Sub 1 Head 4" },
        };

        return items;
    }
}