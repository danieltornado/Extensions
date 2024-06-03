using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Extensions.Tests;

[TestFixture]
[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
public sealed class EnumerableExtensionsAsCollectionTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public void AsArray_ShouldBe_Success(IEnumerable<string> testData)
    {
        var result = testData.AsArray();

        using (new AssertionScope())
        {
            result.Should().NotBeNull();

            if (testData is string[] array)
            {
                result.Should().BeSameAs(array);
            }
            else
            {
                result.Should().NotBeSameAs(testData);
            }

            result.Should().HaveCount(testData.Count());
        }
    }

    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public void AsList_ShouldBe_Success(IEnumerable<string> testData)
    {
        var result = testData.AsList();

        using (new AssertionScope())
        {
            result.Should().NotBeNull();

            if (testData is List<string> list)
            {
                result.Should().BeSameAs(list);
            }
            else
            {
                result.Should().NotBeSameAs(testData);
            }

            result.Should().HaveCount(testData.Count());
        }
    }

    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public void AsReadOnlyList_ShouldBe_Success(IEnumerable<string> testData)
    {
        var result = testData.AsReadOnlyList();

        using (new AssertionScope())
        {
            result.Should().NotBeNull();

            if (testData is List<string> list)
            {
                result.Should().BeSameAs(list);
            }
            else if (testData is string[] array)
            {
                result.Should().BeSameAs(array);
            }
            else
            {
                result.Should().NotBeSameAs(testData);
            }

            result.Should().HaveCount(testData.Count());
        }
    }

    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public async Task AsArrayAsync_ShouldBe_Success(IEnumerable<string> testData)
    {
        var task = Task.FromResult(testData);
        var result = await task.AsArrayAsync();

        using (new AssertionScope())
        {
            result.Should().NotBeNull();

            if (testData is string[] array)
            {
                result.Should().BeSameAs(array);
            }
            else
            {
                result.Should().NotBeSameAs(testData);
            }

            result.Should().HaveCount(testData.Count());
        }
    }

    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public async Task AsListAsync_ShouldBe_Success(IEnumerable<string> testData)
    {
        var task = Task.FromResult(testData);
        var result = await task.AsListAsync();

        using (new AssertionScope())
        {
            result.Should().NotBeNull();

            if (testData is List<string> list)
            {
                result.Should().BeSameAs(list);
            }
            else
            {
                result.Should().NotBeSameAs(testData);
            }

            result.Should().HaveCount(testData.Count());
        }
    }

    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public async Task AsReadOnlyListAsync_ShouldBe_Success(IEnumerable<string> testData)
    {
        var task = Task.FromResult(testData);
        var result = await task.AsReadOnlyListAsync();

        using (new AssertionScope())
        {
            result.Should().NotBeNull();

            if (testData is List<string> list)
            {
                result.Should().BeSameAs(list);
            }
            else if (testData is string[] array)
            {
                result.Should().BeSameAs(array);
            }
            else
            {
                result.Should().NotBeSameAs(testData);
            }

            result.Should().HaveCount(testData.Count());
        }
    }

    [Test]
    [TestCaseSource(nameof(GetDataSourceForEmptyIfNull))]
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
    }

    private static IEnumerable<object> GetDataSourceForEmptyIfNull()
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