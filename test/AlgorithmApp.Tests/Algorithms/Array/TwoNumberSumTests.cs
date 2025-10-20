using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class TwoNumberSumTests
{
    private TwoNumberSum _algorithm = null!;
    
    [SetUp]
    public void Setup() => _algorithm = new TwoNumberSum();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        // Assert
        Assert.That(_algorithm.Name, Is.EqualTo("Two Number Sum"));

    [Test]
    public void Description_ReturnsCorrectValue()
    {
        // Assert
        Assert.That(_algorithm.Description, Does.Contain("array of integers"));
        Assert.That(_algorithm.Description, Does.Contain("target"));
    }
    
    [Test]
    public void Category_ReturnsCorrectValue() =>
        // Assert
        Assert.That(_algorithm.Category, Is.EqualTo("Array"));

    [Test]
    public void TimeComplexity_ReturnsCorrectValue() =>
        // Assert
        Assert.That(_algorithm.TimeComplexity, Is.EqualTo("O(n)"));

    [Test]
    public void SpaceComplexity_ReturnsCorrectValue() =>
        // Assert
        Assert.That(_algorithm.SpaceComplexity, Is.EqualTo("O(n)"));

    [Test]
    public void Hint_ContainsHashMapReference() =>
        // Assert
        Assert.That(_algorithm.Hint, Does.Contain("hashmap"));

    [Test]
    public void ValidateInput_WithValidTuple_ReturnsTrue()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 2, 3 }, 5);
        
        // Act
        bool result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void ValidateInput_WithArrayLengthLessThanTwo_ReturnsFalse()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1 }, 5);
        
        // Act
        bool result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateInput_WithEmptyArray_ReturnsFalse()
    {
        // Arrange
        var input = Tuple.Create(System.Array.Empty<int>(), 5);
        
        // Act
        bool result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateInput_WithNonTupleInput_ReturnsFalse()
    {
        // Arrange
        int[] input = new[] { 1, 2, 3 };
        
        // Act
        bool result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void GenerateSampleInput_ReturnsTupleWithArrayAndTarget()
    {
        // Arrange
        int size = 10;
        
        // Act
        object result = _algorithm.GenerateSampleInput(size);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.TypeOf<Tuple<int[], int>>());
            var tuple = (Tuple<int[], int>)result;
            Assert.That(tuple.Item1.Length, Is.EqualTo(size));
            Assert.That(tuple.Item2, Is.TypeOf<int>());
        });
    }

    [Test]
    public void ExecuteAsync_WithValidPair_ReturnsCorrectIndices()
    {
        // Arrange
        var input = Tuple.Create(new[] { 2, 7, 11, 15 }, 9);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[][] output = (int[][])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Length, Is.EqualTo(1));
            Assert.That(output[0][0], Is.EqualTo(0));
            Assert.That(output[0][1], Is.EqualTo(1));
            Assert.That(result.Steps, Is.Not.Empty);
            Assert.That(result.AlgorithmName, Is.EqualTo(_algorithm.Name));
        });
    }

    [Test]
    public void ExecuteAsync_WithPairAtEnd_ReturnsCorrectIndices()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 2, 3, 4, 5 }, 9);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[][] output = (int[][])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output[0][0], Is.EqualTo(3));
            Assert.That(output[0][1], Is.EqualTo(4));
        });
    }

    [Test]
    public void ExecuteAsync_WithNegativeNumbers_ReturnsValidPair()
    {
        // Arrange
        var input = Tuple.Create(new[] { -3, -1, 2, 4, 6 }, 3);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[][] output = (int[][])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Length, Is.EqualTo(1));
            Assert.That(output[0].Length, Is.EqualTo(2));
            // Verify it's a valid pair that sums to 3
            int sum = input.Item1[output[0][0]] + input.Item1[output[0][1]];
            Assert.That(sum, Is.EqualTo(3));
            // Verify indices are in ascending order
            Assert.That(output[0][0], Is.LessThan(output[0][1]));
            Assert.That(result.Steps, Contains.Item("Target sum: 3"));
        });
    }

    [Test]
    public void ExecuteAsync_WithZeroInArray_HandlesCorrectly()
    {
        // Arrange
        var input = Tuple.Create(new[] { 0, 4, 3, 0 }, 0);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[][] output = (int[][])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output[0][0], Is.EqualTo(0));
            Assert.That(output[0][1], Is.EqualTo(3));
        });
    }

    [Test]
    public void ExecuteAsync_WithDuplicateNumbers_ReturnsValidPair()
    {
        // Arrange
        var input = Tuple.Create(new[] { 3, 3, 11, 15 }, 6);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[][] output = (int[][])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Length, Is.EqualTo(1));
            Assert.That(output[0].Length, Is.EqualTo(2));
            // Verify it's a valid pair that sums to 6
            int sum = input.Item1[output[0][0]] + input.Item1[output[0][1]];
            Assert.That(sum, Is.EqualTo(6));
            // Verify indices are in ascending order
            Assert.That(output[0][0], Is.LessThan(output[0][1]));
        });
    }

    [Test]
    public void ExecuteAsync_WithNoValidPair_ReturnsEmptyArray()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 2, 3, 4 }, 10);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Length, Is.EqualTo(0));
            Assert.That(result.Steps, Is.Not.Empty);
        });
    }

    [Test]
    public void ExecuteAsync_WithTwoElements_ReturnsCorrectIndices()
    {
        // Arrange
        var input = Tuple.Create(new[] { 5, 7 }, 12);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[][] output = (int[][])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output[0][0], Is.EqualTo(0));
            Assert.That(output[0][1], Is.EqualTo(1));
        });
    }

    [Test]
    public void ExecuteAsync_WithLargeNumbers_HandlesCorrectly()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1000, 2000, 3000, 4000 }, 7000);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[][] output = (int[][])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output[0][0], Is.EqualTo(2));
            Assert.That(output[0][1], Is.EqualTo(3));
        });
    }

    [Test]
    public void ExecuteAsync_WithNegativeTarget_ReturnsCorrectIndices()
    {
        // Arrange
        var input = Tuple.Create(new[] { -10, -5, -3, -1 }, -8);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[][] output = (int[][])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output[0][0], Is.EqualTo(1));
            Assert.That(output[0][1], Is.EqualTo(2));
        });
    }

    [Test]
    public void ExecuteAsync_WithInvalidInput_ThrowsArgumentException()
    {
        // Arrange
        string input = "invalid input";
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }

    [Test]
    public void ExecuteAsync_WithArrayLengthOne_ThrowsArgumentException()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1 }, 5);
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }

    [Test]
    public void ExecuteAsync_ResultContainsAllRequiredProperties()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 2, 3 }, 5);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.AlgorithmName, Is.Not.Null.And.Not.Empty);
            Assert.That(result.Input, Is.Not.Null);
            Assert.That(result.Output, Is.Not.Null);
            Assert.That(result.Steps, Is.Not.Null.And.Not.Empty);
        });
    }

    [Test]
    public void ExecuteAsync_StepsContainExpectedInformation()
    {
        // Arrange
        var input = Tuple.Create(new[] { 2, 7, 11, 15 }, 9);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Steps, Does.Contain("Input array: [2, 7, 11, 15]"));
            Assert.That(result.Steps, Does.Contain("Target sum: 9"));
            Assert.That(result.Steps.Any(s => s.Contains("hash")), Is.True);
        });
    }

    [Test]
    public void ExecuteAsync_WithMixedPositiveAndNegative_ReturnsValidPair()
    {
        // Arrange
        var input = Tuple.Create(new[] { -1, 0, 1, 2, -1, -4 }, -2);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[][] output = (int[][])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Length, Is.EqualTo(1));
            Assert.That(output[0].Length, Is.EqualTo(2));
            // Verify it's a valid pair that sums to -2
            int sum = input.Item1[output[0][0]] + input.Item1[output[0][1]];
            Assert.That(sum, Is.EqualTo(-2));
            // Verify indices are in ascending order
            Assert.That(output[0][0], Is.LessThan(output[0][1]));
        });
    }

    [Test]
    public void ExecuteAsync_ReturnsIndicesInAscendingOrder()
    {
        // Arrange
        var input = Tuple.Create(new[] { 3, 2, 4 }, 6);
        
        // Act
       AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[][] output = (int[][])result.Output!;
        
        // Assert
        Assert.That(output[0][0], Is.LessThan(output[0][1]), 
            "First index should be smaller than second index");
    }
}
