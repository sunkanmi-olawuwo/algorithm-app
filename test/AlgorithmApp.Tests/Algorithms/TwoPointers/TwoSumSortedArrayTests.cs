using System.Reflection;
using AlgorithmApp.Algorithms.TwoPointers;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.TwoPointers;

[TestFixture]
public class TwoSumSortedArrayTests
{
    private TwoSumSortedArray _algorithm = null!;

    [SetUp]
    public void Setup() => _algorithm = new TwoSumSortedArray();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Name, Is.EqualTo("Two Sum (Sorted Array)"));

    [Test]
    public void Description_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Description, Does.Contain("sorted array"));

    [Test]
    public void TimeComplexity_ReturnsCorrectValue() =>
        Assert.That(_algorithm.TimeComplexity, Is.EqualTo("O(n)"));

    [Test]
    public void SpaceComplexity_ReturnsCorrectValue() =>
        Assert.That(_algorithm.SpaceComplexity, Is.EqualTo("O(1)"));

    [Test]
    public void Hint_MentionsTwoPointers() =>
        Assert.That(_algorithm.Hint, Does.Contain("two pointers"));

    [Test]
    public void ValidateInput_WithValidArray_ReturnsTrue()
    {
        // Arrange
        int[] input = [1, 2];

        // Act
        bool result = _algorithm.ValidateInput(input);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateInput_WithSingleElementArray_ReturnsFalse()
    {
        // Arrange
        int[] input = [1];

        // Act
        bool result = _algorithm.ValidateInput(input);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateInput_WithNonArrayInput_ReturnsFalse()
    {
        // Arrange
        object input = "not an array";

        // Act
        bool result = _algorithm.ValidateInput(input);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GenerateSampleInput_ReturnsTupleOfArrayAndTarget()
    {
        // Act
        object result = _algorithm.GenerateSampleInput(5);

        // Assert
        Assert.That(result, Is.TypeOf<Tuple<int[], int>>());
        var tuple = (Tuple<int[], int>)result;
        Assert.That(tuple.Item1, Is.TypeOf<int[]>());
    }

    [Test]
    public void ExecuteAsync_WithValidPair_ReturnsCorrectIndicesAndValues()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 2, 3, 4, 5 }, 5);

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = result.Output!;

        // Assert
        var indicesProp = output.GetType().GetProperty("Indices");
        var valuesProp = output.GetType().GetProperty("Values");

        var indices = (int[])indicesProp!.GetValue(output)!;
        var values = (int[])valuesProp!.GetValue(output)!;

        Assert.Multiple(() =>
        {
            Assert.That(indices.Length, Is.EqualTo(2));
            Assert.That(values.Length, Is.EqualTo(2));
            Assert.That(values.Sum(), Is.EqualTo(5));
            Assert.That(result.AlgorithmName, Is.EqualTo(_algorithm.Name));
            Assert.That(result.Steps, Is.Not.Empty);
        });
    }

    [Test]
    public void ExecuteAsync_WhenNoPairExists_ReturnsMessage()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 2, 3 }, 10);

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = result.Output!;

        var messageProp = output.GetType().GetProperty("Message");
        string message = (string)messageProp!.GetValue(output)!;

        // Assert
        Assert.That(message, Does.Contain("No two numbers"));
    }

    [Test]
    public void ExecuteAsync_WithInvalidInput_ThrowsArgumentException()
    {
        // Arrange
        object input = new[] { 1, 2, 3 };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }
}
