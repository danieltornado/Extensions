using FluentAssertions;

namespace Extensions.Tests;

[TestFixture]
public sealed class StringExtensionsTests
{
    [Test]
    [TestCase("123")]
    [TestCase("  123  ")]
    [TestCase("")]
    [TestCase("   ")]
    [TestCase(null)]
    public void IsNullOrEmpty_ShouldBe_Success(string? testValue)
    {
        testValue.IsNullOrEmpty().Should().Be(string.IsNullOrEmpty(testValue));
    }
    
    [Test]
    [TestCase("123")]
    [TestCase("  123  ")]
    [TestCase("")]
    [TestCase("   ")]
    [TestCase(null)]
    public void IsNullOrWhiteSpace_ShouldBe_Success(string? testValue)
    {
        testValue.IsNullOrWhiteSpace().Should().Be(string.IsNullOrWhiteSpace(testValue));
    }
}

