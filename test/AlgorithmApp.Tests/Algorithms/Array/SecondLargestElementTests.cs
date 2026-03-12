using System.Reflection;
using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class SecondLargestElementTests
{
    private SecondLargestElement _algorithm = null!;

    [SetUp]
    public void Setup() => _algorithm = new SecondLargestElement();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Name, Is.EqualTo("Second Largest Element in an Array"));

    [Test]
    public void Category_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Category, Is.EqualTo("Array"));

    [Test]
    public void ValidateInput_WithValidArray_ReturnsTrue()
    {
        int[] input = [3, 1, 2];
        bool result = _algorithm.ValidateInput(input);
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateInput_WithEmptyArray_ReturnsFalse()
    {
        int[] input = [];
        bool result = _algorithm.ValidateInput(input);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateInput_WithNonArrayInput_ReturnsFalse()
    {
        string input = "invalid";
        bool result = _algorithm.ValidateInput(input);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ExecuteAsync_WithDistinctValues_ReturnsSecondLargest()
    {
        int[] input = [12, 35, 1, 10, 34, 1];

        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int secondLargest = GetSecondLargestValue(result);

        Assert.That(secondLargest, Is.EqualTo(34));
    }

    [Test]
    public void ExecuteAsync_WithAllEqualValues_ReturnsMinusOne()
    {
        int[] input = [7, 7, 7, 7];

        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int secondLargest = GetSecondLargestValue(result);

        Assert.That(secondLargest, Is.EqualTo(-1));
    }

    [Test]
    public void ExecuteAsync_WithSingleElementArray_ReturnsMinusOne()
    {
        int[] input = [42];

        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int secondLargest = GetSecondLargestValue(result);

        Assert.That(secondLargest, Is.EqualTo(-1));
    }

    [Test]
    public void ExecuteAsync_WithNegativeNumbers_ReturnsSecondLargest()
    {
        int[] input = [-10, -3, -20, -5];

        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int secondLargest = GetSecondLargestValue(result);

        Assert.That(secondLargest, Is.EqualTo(-5));
    }

    [Test]
    public void ExecuteAsync_WithInvalidInput_ThrowsArgumentException()
    {
        string input = "invalid";
        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }

    private static int GetSecondLargestValue(AlgorithmResult result)
    {
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? valueProp = outputProps?.FirstOrDefault(p => p.Name == "SecondLargestElement");
        return (int)(valueProp?.GetValue(result.Output) ?? -1);
    }
}
