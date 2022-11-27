using FluentAssertions;

namespace Extensions.Tests;

[TestFixture]
public class StringExtensionsTests
{
    [Test]
    public void IsNullOrEmpty_ShouldBe_Success()
    {
        string data1 = "123";
        string? data2 = null;
        string data3 = "";

        data1.IsNullOrEmpty().Should().Be(string.IsNullOrEmpty(data1));
        data2.IsNullOrEmpty().Should().Be(string.IsNullOrEmpty(data2));
        data3.IsNullOrEmpty().Should().Be(string.IsNullOrEmpty(data3));
    }
}

