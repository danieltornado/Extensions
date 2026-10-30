using FluentAssertions;
using Xunit;

namespace Extensions.Tests.AsyncEnumerableTests;

public sealed class BatchTests
{
    [Fact]
    public async Task WhenHasSource_Batch_ShouldBeSucceeded()
    {
        // Arrange
        var source = AsyncEnumerable.Range(0, 31);

        // Act
        var result = await source.BatchAsync(batch => batch.Sum(), 10, CancellationToken.None).ToListAsync();

        // Assert
        result.Should().HaveCount(4);

        int[] expectedBatch1 = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
        int[] expectedBatch2 = [10, 11, 12, 13, 14, 15, 16, 17, 18, 19];
        int[] expectedBatch3 = [20, 21, 22, 23, 24, 25, 26, 27, 28, 29];
        int[] expectedBatch4 = [30];
        
        result.Should().BeEquivalentTo([ expectedBatch1.Sum(), expectedBatch2.Sum(), expectedBatch3.Sum(), expectedBatch4.Sum() ]);
    }
}