using FluentAssertions;

namespace Extensions.Tests;

[TestFixture]
public sealed class EnumerableExtensionsFlattenTests
{
    private class Graph
    {
        public Graph(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public IEnumerable<Graph>? Items { get; set; }
    }
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Flatten_ShouldBe_Success()
    {
        var data = new Graph(0)
        {
            Items = new Graph[]
            {
                new(1)
                {
                    Items = new Graph[]
                    {
                        new(2)
                        {
                            Items = new Graph[]
                            {
                                new(3),
                                new(4)
                            }
                        },
                        new(5)
                        {
                            Items = new Graph[]
                            {
                                new(6),
                                new(7)
                            }
                        }
                    }
                },
                new(8)
                {
                    Items = new Graph[]
                    {
                        new(9)
                        {
                            Items = new Graph[]
                            {
                                new(10),
                                new(11)
                            }
                        },
                        new(12)
                        {
                            Items = new Graph[]
                            {
                                new(13),
                                new(14)
                            }
                        }
                    }
                }
            }
        };

        IEnumerable<int> expected = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

        data.Items.Flatten(e => e.Items).Select(e => e.Value).Should().BeEquivalentTo(expected);
    }
}