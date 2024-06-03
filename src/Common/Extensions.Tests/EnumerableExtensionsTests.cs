using FluentAssertions;

namespace Extensions.Tests;

[TestFixture]
public class EnumerableExtensionsTests
{
    private class TestDataInt
    {
        public TestDataInt(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MinOrDefault_ShouldBe_Success()
    {
        var data1 = new[] { 3, 2, 1 };
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
}