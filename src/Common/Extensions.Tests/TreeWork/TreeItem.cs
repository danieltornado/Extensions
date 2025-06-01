namespace Extensions.Tests.TreeWork;

public sealed class TreeItem : ICloneable
{
    public int Id { get; set; }
    
    public int ParentId { get; set; }
    
    public string? Name { get; set; }
    
    public int Level { get; set; }

    public List<TreeItem> Childs = new();

    public override string ToString()
    {
        return $"{{{Id}: {Name}}}";
    }

    public object Clone()
    {
        return new TreeItem { Id = Id, ParentId = Id, Name = Name };
    }

    public string ToString(int level)
    {
        var str = ToString();
        return str.PadLeft(str.Length + level, ' ');
    }
}