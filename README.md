# Extensions
Extensions

# TreeWork
Linear (without using recursion) traversal of the tree: AsTree-method and AsTreeWithContext-method receive an IEnumerable object with a special traversal iterator, and a delegate is used as the second parameter, returning nested elements for each node

# Sample:

``` c#
class TreeItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TreeItem> Childs { get; set; }

    public override string ToString()
    {
        return Id.ToString() + " " + Name;
    }
}

static void Main(string[] args)
{
    var heads = new List<TreeItem>
    {
        new TreeItem
        {
            Id = 1, Name = "David", Childs = new List<TreeItem>
            {
                new TreeItem
                {
                    Id = 2, Name = "Joey", Childs = new List<TreeItem>
                    {
                        new TreeItem { Id = 3, Name = "Chandler" },
                        new TreeItem { Id = 4, Name = "Richard" }
                    }
                },
                new TreeItem { Id = 5, Name = "Ross" }
            }
        },
        new TreeItem
        {
            Id = 6, Name = "Marta", Childs = new List<TreeItem>
            {
                new TreeItem
                {
                    Id = 7, Name = "Rachel", Childs = new List<TreeItem>
                    {
                        new TreeItem { Id = 8, Name = "Monica" }
                    }
                },
                new TreeItem { Id = 9, Name = "Phoebe" }
            }
        }
    };

    foreach (var item in heads.AsTree(head => head.Childs))
    {
        Console.WriteLine(string.Empty.PadLeft(item.Level, ' ') + item.Value);
    }
}

//Output:
//
//1 David
// 2 Joey
//  3 Chandler
//  4 Richard
// 5 Ross
//6 Marta
// 7 Rachel
//  8 Monica
// 9 Phoebe
```
