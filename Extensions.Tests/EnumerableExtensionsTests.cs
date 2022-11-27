using FluentAssertions;
using FluentAssertions.Execution;

namespace Extensions.Tests;

[TestFixture]
public class EnumerableExtensionsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AsArray_ShouldBe_Success()
    {
        var data1 = Array.Empty<string?>();
        var data2 = new List<string?>(0);
        IEnumerable<string?>? data3 = null;
        IEnumerable<string?> data4 = Enumerable.Repeat(string.Empty, 3);

        var result1 = data1.AsArray();
        var result2 = data2.AsArray();
        var result3 = data3.AsArray();
        var result4 = data4.AsArray();

        using (new AssertionScope())
        {
            result1.Should().NotBeNull();
            result1.Should().BeSameAs(data1);

            result2.Should().NotBeNull();
            result2.Should().NotBeSameAs(data2);

            result3.Should().BeNull();

            result4.Should().NotBeNull();
            result4.Should().NotBeSameAs(data4);
            result4.Should().HaveCount(3);
        }
    }

    [Test]
    public void AsList_ShouldBe_Success()
    {
        var data1 = Array.Empty<string?>();
        var data2 = new List<string?>(0);
        IEnumerable<string?>? data3 = null;
        IEnumerable<string?> data4 = Enumerable.Repeat(string.Empty, 3);

        var result1 = data1.AsList();
        var result2 = data2.AsList();
        var result3 = data3.AsList();
        var result4 = data4.AsList();

        using (new AssertionScope())
        {
            result1.Should().NotBeNull();
            result1.Should().NotBeSameAs(data1);

            result2.Should().NotBeNull();
            result2.Should().BeSameAs(data2);

            result3.Should().BeNull();

            result4.Should().NotBeNull();
            result4.Should().NotBeSameAs(data4);
            result4.Should().HaveCount(3);
        }
    }

    [Test]
    public void AsReadOnlyList_ShouldBe_Success()
    {
        var data1 = Array.Empty<string?>();
        var data2 = new List<string?>(0);
        IEnumerable<string?>? data3 = null;
        IEnumerable<string?> data4 = Enumerable.Repeat(string.Empty, 3);

        var result1 = data1.AsReadOnlyList();
        var result2 = data2.AsReadOnlyList();
        var result3 = data3.AsReadOnlyList();
        var result4 = data4.AsReadOnlyList();

        using (new AssertionScope())
        {
            result1.Should().NotBeNull();
            result1.Should().BeSameAs(data1);

            result2.Should().NotBeNull();
            result2.Should().BeSameAs(data2);

            result3.Should().BeNull();

            result4.Should().NotBeNull();
            result4.Should().NotBeSameAs(data4);
            result4.Should().HaveCount(3);
            result4.Should().BeOfType(typeof(List<string?>));
        }
    }

    [Test]
    public async Task AsArrayAsync_ShouldBe_Success()
    {
        IEnumerable<string?> data1 = Array.Empty<string?>();
        IEnumerable<string?> data2 = new List<string?>(0);
        IEnumerable<string?>? data3 = null;
        IEnumerable<string?> data4 = Enumerable.Repeat(string.Empty, 3);

        var task1 = Task.FromResult(data1);
        var task2 = Task.FromResult(data2);
        var task3 = Task.FromResult(data3);
        var task4 = Task.FromResult(data4);

        var result1 = await task1!.AsArrayAsync();
        var result2 = await task2!.AsArrayAsync();
        var result3 = await task3.AsArrayAsync();
        var result4 = await task4!.AsArrayAsync();

        using (new AssertionScope())
        {
            result1.Should().NotBeNull();
            result1.Should().BeSameAs(data1);

            result2.Should().NotBeNull();
            result2.Should().NotBeSameAs(data2);

            result3.Should().BeNull();

            result4.Should().NotBeNull();
            result4.Should().NotBeSameAs(data4);
            result4.Should().HaveCount(3);
        }
    }

    [Test]
    public async Task AsListAsync_ShouldBe_Success()
    {
        IEnumerable<string?> data1 = Array.Empty<string?>();
        IEnumerable<string?> data2 = new List<string?>(0);
        IEnumerable<string?>? data3 = null;
        IEnumerable<string?> data4 = Enumerable.Repeat(string.Empty, 3);

        var task1 = Task.FromResult(data1);
        var task2 = Task.FromResult(data2);
        var task3 = Task.FromResult(data3);
        var task4 = Task.FromResult(data4);

        var result1 = await task1!.AsListAsync();
        var result2 = await task2!.AsListAsync();
        var result3 = await task3.AsListAsync();
        var result4 = await task4!.AsListAsync();

        using (new AssertionScope())
        {
            result1.Should().NotBeNull();
            result1.Should().NotBeSameAs(data1);

            result2.Should().NotBeNull();
            result2.Should().BeSameAs(data2);

            result3.Should().BeNull();

            result4.Should().NotBeNull();
            result4.Should().NotBeSameAs(data4);
            result4.Should().HaveCount(3);
        }
    }

    [Test]
    public async Task AsReadOnlyListAsync_ShouldBe_Success()
    {
        IEnumerable<string?> data1 = Array.Empty<string?>();
        IEnumerable<string?> data2 = new List<string?>(0);
        IEnumerable<string?>? data3 = null;
        IEnumerable<string?> data4 = Enumerable.Repeat(string.Empty, 3);

        var task1 = Task.FromResult(data1);
        var task2 = Task.FromResult(data2);
        var task3 = Task.FromResult(data3);
        var task4 = Task.FromResult(data4);

        var result1 = await task1!.AsReadOnlyListAsync();
        var result2 = await task2!.AsReadOnlyListAsync();
        var result3 = await task3.AsReadOnlyListAsync();
        var result4 = await task4!.AsReadOnlyListAsync();

        using (new AssertionScope())
        {
            result1.Should().NotBeNull();
            result1.Should().BeSameAs(data1);

            result2.Should().NotBeNull();
            result2.Should().BeSameAs(data2);

            result3.Should().BeNull();

            result4.Should().NotBeNull();
            result4.Should().NotBeSameAs(data4);
            result4.Should().HaveCount(3);
            result4.Should().BeOfType(typeof(List<string?>));
        }
    }

    private class TestDataInt
    {
        public TestDataInt(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }

    [Test]
    public void MinOrDefault_ShouldBe_Success()
    {
        var data1 = new [] { 3, 2, 1 };
        var data2 = Array.Empty<int>();
        var data3 = new TestDataInt[] { new(3), new(2), new(1) };
        var data4 = Array.Empty<TestDataInt>();

        data1.MinOrDefault(0).Should().Be(1);
        data2.MinOrDefault(0).Should().Be(0);
        data3.MinOrDefault(e => e.Value, 0).Should().Be(1);
        data4.MinOrDefault(e => e.Value, 0).Should().Be(0);
    }

    [Test]
    public void MaxOrDefault_ShouldBe_Success()
    {
        var data1 = new[] { 3, 2, 1 };
        var data2 = Array.Empty<int>();
        var data3 = new TestDataInt[] { new(3), new(2), new(1) };
        var data4 = Array.Empty<TestDataInt>();

        data1.MaxOrDefault(0).Should().Be(3);
        data2.MaxOrDefault(0).Should().Be(0);
        data3.MaxOrDefault(e => e.Value, 0).Should().Be(3);
        data4.MaxOrDefault(e => e.Value, 0).Should().Be(0);
    }

    [Test]
    public void EmptyIfNull_ShouldBe_Success()
    {
        IEnumerable<int> data1 = Enumerable.Repeat(0, 3);
        IEnumerable<int>? data2 = null;

        int[] data3 = Array.Empty<int>();
        int[]? data4 = null;

        List<int> data5 = new(0);
        List<int>? data6 = null;

        data1.EmptyIfNull().Should().BeSameAs(data1);
        data2.EmptyIfNull().Should().NotBeNull();

        data3.EmptyIfNull().Should().BeSameAs(data3);
        data4.EmptyIfNull().Should().NotBeNull();

        data5.EmptyIfNull().Should().BeSameAs(data5);
        data6.EmptyIfNull().Should().NotBeNull();
    }

    private class Graph
    {
        public Graph(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public IEnumerable<Graph>? Items { get; set; }
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

        IEnumerable<int> expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

        data.Items.Flatten(e => e.Items).Select(e => e.Value).Should().BeEquivalentTo(expected);
    }
}
