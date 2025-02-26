using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Extensions.Tests.EnumerableTests;

[TestFixture]
[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
public sealed class AsCollectionTests
{
    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public void WhenNotNullCollection_AsArray_ShouldBeSuccessful(IEnumerable<string> enumerable, List<string> list, string[] array)
    {
        // arrange
        // act
        var enumerableActual = enumerable.AsArray();
        var listActual = list.AsArray();
        var arrayActual = array.AsArray();

        // assert
        using (new AssertionScope())
        {
            enumerableActual.Should().NotBeNull();
            enumerableActual.Should().NotBeSameAs(enumerable);
            enumerableActual.Should().HaveCount(enumerable.Count());

            listActual.Should().NotBeNull();
            listActual.Should().NotBeSameAs(list);
            listActual.Should().HaveCount(list.Count);

            arrayActual.Should().NotBeNull();
            arrayActual.Should().BeSameAs(array);
            arrayActual.Should().HaveCount(array.Length);
        }
    }

    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public void WhenNotNullCollection_AsList_ShouldBeSuccessful(IEnumerable<string> enumerable, List<string> list, string[] array)
    {
        // arrange
        // act
        var enumerableActual = enumerable.AsList();
        var listActual = list.AsList();
        var arrayActual = array.AsList();

        // assert
        using (new AssertionScope())
        {
            enumerableActual.Should().NotBeNull();
            enumerableActual.Should().NotBeSameAs(enumerable);
            enumerableActual.Should().HaveCount(enumerable.Count());

            listActual.Should().NotBeNull();
            listActual.Should().BeSameAs(list);
            listActual.Should().HaveCount(list.Count);

            arrayActual.Should().NotBeNull();
            arrayActual.Should().NotBeSameAs(array);
            arrayActual.Should().HaveCount(array.Length);
        }
    }

    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public void WhenNotNullCollection_AsReadOnlyList_ShouldBeSuccessful(IEnumerable<string> enumerable, List<string> list, string[] array)
    {
        // arrange
        // act
        var enumerableActual = enumerable.AsReadOnlyList();
        var listActual = list.AsReadOnlyList();
        var arrayActual = array.AsReadOnlyList();

        // assert
        using (new AssertionScope())
        {
            enumerableActual.Should().NotBeNull();
            enumerableActual.Should().NotBeSameAs(enumerable);
            enumerableActual.Should().HaveCount(enumerable.Count());

            listActual.Should().NotBeNull();
            listActual.Should().BeSameAs(list);
            listActual.Should().HaveCount(list.Count);

            arrayActual.Should().NotBeNull();
            arrayActual.Should().BeSameAs(array);
            arrayActual.Should().HaveCount(array.Length);
        }
    }

    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public void WhenNotNullCollection_AsReadOnlyCollection_ShouldBeSuccessful(IEnumerable<string> enumerable, List<string> list, string[] array)
    {
        // arrange
        // act
        var enumerableActual = enumerable.AsReadOnlyCollection();
        var listActual = list.AsReadOnlyCollection();
        var arrayActual = array.AsReadOnlyCollection();

        // assert
        using (new AssertionScope())
        {
            enumerableActual.Should().NotBeNull();
            enumerableActual.Should().NotBeSameAs(enumerable);
            enumerableActual.Should().HaveCount(enumerable.Count());

            listActual.Should().NotBeNull();
            listActual.Should().BeSameAs(list);
            listActual.Should().HaveCount(list.Count);

            arrayActual.Should().NotBeNull();
            arrayActual.Should().BeSameAs(array);
            arrayActual.Should().HaveCount(array.Length);
        }
    }

    private static IEnumerable<object> GetDataSource()
    {
        yield return new object[]
        {
            Enumerable.Repeat(string.Empty, 3),
            new List<string>
            {
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty
            },
            new[]
            {
                string.Empty,
                string.Empty
            }
        };
    }
}