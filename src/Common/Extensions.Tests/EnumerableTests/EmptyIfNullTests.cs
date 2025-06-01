using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Extensions.Tests.EnumerableTests;

[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
public sealed class EmptyIfNullTests
{
    [Theory]
    [MemberData(nameof(GetDataSource))]
    public void EmptyIfNull_ShouldBe_Success(IEnumerable<string>? testData)
    {
        testData.EmptyIfNull().Should().NotBeNull();

        if (testData != null)
        {
            testData.EmptyIfNull().Should().BeSameAs(testData);
        }
    }
    
    public static IEnumerable<object?[]> GetDataSource()
    {
        yield return
        [
            Enumerable.Repeat(string.Empty, 3)
        ];

        yield return
        [
            new List<string>
            {
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty
            }
        ];

        yield return
        [
            new[]
            {
                string.Empty,
                string.Empty
            }
        ];

        yield return
        [
            null
        ];
    }
}