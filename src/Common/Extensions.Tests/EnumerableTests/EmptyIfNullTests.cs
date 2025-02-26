using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

namespace Extensions.Tests.EnumerableTests;

[TestFixture]
[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
public sealed class EmptyIfNullTests
{
    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public void EmptyIfNull_ShouldBe_Success(IEnumerable<string>? testData)
    {
        testData.EmptyIfNull().Should().NotBeNull();

        if (testData != null)
        {
            testData.EmptyIfNull().Should().BeSameAs(testData);
        }
    }
    
    private static IEnumerable<object> GetDataSource()
    {
        yield return new object[]
        {
            Enumerable.Repeat(string.Empty, 3)
        };

        yield return new object[]
        {
            new List<string>
            {
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty
            }
        };

        yield return new object[]
        {
            new[]
            {
                string.Empty,
                string.Empty
            }
        };

        yield return new object?[]
        {
            null
        };
    }
}