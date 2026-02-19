using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class LongestConsecutiveSequenceTests
{
    private LongestConsecutiveSequence _algorithm = null!;

    [SetUp]
    public void Setup() => _algorithm = new LongestConsecutiveSequence();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Name, Is.EqualTo("Longest Consecutive Sequence"));

    [Test]
    public void Description_DescribesProblem() =>
        Assert.That(_algorithm.Description, Does.Contain("longest consecutive elements sequence"));

    [Test]
    public void TimeComplexity_ReturnsCorrectValue() =>
        Assert.That(_algorithm.TimeComplexity, Is.EqualTo("O(n)"));

    [Test]
    public void SpaceComplexity_ReturnsCorrectValue() =>
        Assert.That(_algorithm.SpaceComplexity, Is.EqualTo("O(n)"));

    [Test]
    public void Hint_MentionsHashSet() =>
        Assert.That(_algorithm.Hint, Does.Contain("hash set"));

    [Test]
    public void GenerateSampleInput_ReturnsIntArray() 
    {
        // Arrange
        int size = 10;

        // Act
        object result = _algorithm.GenerateSampleInput(size);

        // Assert
        Assert.That(result, Is.TypeOf<int[]>());
    }

    [Test]
    public void ExecuteAsync_WithUnsortedArray_ReturnsCorrectLongestSequenceLength()
    {
        // Arrange
        int[] input = [100, 4, 200, 1, 3, 2];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int length = (int)result.Output!;

        // Assert
        Assert.That(length, Is.EqualTo(4)); // sequence 1,2,3,4
        Assert.That(result.AlgorithmName, Is.EqualTo(_algorithm.Name));
        Assert.That(result.Steps, Is.Not.Empty);
    }

    [Test]
    public void ExecuteAsync_WithDuplicates_IgnoresDuplicatesAndCountsUniqueConsecutive()
    {
        // Arrange
        int[] input = [1, 2, 2, 3];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int length = (int)result.Output!;

        // Assert
        Assert.That(length, Is.EqualTo(3)); // sequence 1,2,3
    }

    [Test]
    public void ExecuteAsync_WithSingleElement_ReturnsOne()
    {
        // Arrange
        int[] input = [7];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int length = (int)result.Output!;

        // Assert
        Assert.That(length, Is.EqualTo(1));
    }

    [Test]
    public void ExecuteAsync_WithEmptyArray_ReturnsZero()
    {
        // Arrange
        int[] input = System.Array.Empty<int>();

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int length = (int)result.Output!;

        // Assert
        Assert.That(length, Is.EqualTo(0));
    }

    [Test]
    public void ExecuteAsync_WithNegativeNumbers_HandlesSequencesCorrectly()
    {
        // Arrange
        int[] input = [-2, -3, -1, 0, 2, 3]; // longest consecutive: -3,-2,-1,0

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int length = (int)result.Output!;

        // Assert
        Assert.That(length, Is.EqualTo(4));
    }
}
