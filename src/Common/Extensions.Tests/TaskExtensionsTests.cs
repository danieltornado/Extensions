using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Extensions.Tests;

[TestFixture]
[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
public sealed class TaskExtensionsTests
{
    [Test]
    [TestCaseSource(nameof(GetDataSource))]
    public async Task WhenNotNullCollection_AsArrayAsync_ShouldBeSuccessful(IEnumerable<string> enumerable, List<string> list, string[] array)
    {
        // arrange
        // act
        var enumerableActual = await enumerable.AsTask().AsArrayAsync();
        var listActual = await list.AsTask<IEnumerable<string>>().AsArrayAsync();
        var arrayActual = await array.AsTask<IEnumerable<string>>().AsArrayAsync();

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
    public async Task WhenNotNullCollection_AsListAsync_ShouldBeSuccessful(IEnumerable<string> enumerable, List<string> list, string[] array)
    {
        // arrange
        // act
        var enumerableActual = await enumerable.AsTask().AsListAsync();
        var listActual = await list.AsTask<IEnumerable<string>>().AsListAsync();
        var arrayActual = await array.AsTask<IEnumerable<string>>().AsListAsync();

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
    public async Task WhenNotNullCollection_AsReadOnlyListAsync_ShouldBeSuccessful(IEnumerable<string> enumerable, List<string> list, string[] array)
    {
        // arrange
        // act
        var enumerableActual = await enumerable.AsTask().AsReadOnlyListAsync();
        var listActual = await list.AsTask<IEnumerable<string>>().AsReadOnlyListAsync();
        var arrayActual = await array.AsTask<IEnumerable<string>>().AsReadOnlyListAsync();

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
    public async Task WhenNotNullCollection_AsReadOnlyCollectionAsync_ShouldBeSuccessful(IEnumerable<string> enumerable, List<string> list, string[] array)
    {
        // arrange
        // act
        var enumerableActual = await enumerable.AsTask().AsReadOnlyCollectionAsync();
        var listActual = await list.AsTask<IEnumerable<string>>().AsReadOnlyCollectionAsync();
        var arrayActual = await array.AsTask<IEnumerable<string>>().AsReadOnlyCollectionAsync();

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

    private class TestException : Exception
    {
    }

    [Test]
    public async Task WhenTaskHasException_AsArrayAsync_ShouldThrowTestException()
    {
        // arrange
        var innerException = new TestException();

        var taskCompletionSource = new TaskCompletionSource<IEnumerable<string>>();
        taskCompletionSource.SetException(innerException);

        var task = taskCompletionSource.Task;

        // act
        var act = () => task.AsArrayAsync();

        // assert
        await act.Should().ThrowAsync<TestException>();
    }

    [Test]
    public async Task WhenTaskIsCancelled_AsArrayAsync_ShouldThrowTestException()
    {
        // arrange
        var taskCompletionSource = new TaskCompletionSource<IEnumerable<string>>();
        taskCompletionSource.SetCanceled();

        var task = taskCompletionSource.Task;

        // act
        var act = () => task.AsArrayAsync();

        // assert
        await act.Should().ThrowAsync<TaskCanceledException>();
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