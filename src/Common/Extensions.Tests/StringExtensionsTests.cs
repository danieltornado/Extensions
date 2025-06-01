using FluentAssertions;
using Xunit;

namespace Extensions.Tests;

public sealed class StringExtensionsTests
{
    [Theory]
    [InlineData("123")]
    [InlineData("  123  ")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void IsNullOrEmpty_ShouldBe_Success(string? testValue)
    {
        testValue.IsNullOrEmpty().Should().Be(string.IsNullOrEmpty(testValue));
    }

    [Theory]
    [InlineData("123")]
    [InlineData("  123  ")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void IsNullOrWhiteSpace_ShouldBe_Success(string? testValue)
    {
        testValue.IsNullOrWhiteSpace().Should().Be(string.IsNullOrWhiteSpace(testValue));
    }
}