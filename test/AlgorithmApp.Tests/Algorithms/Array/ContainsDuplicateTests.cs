using System.Reflection;
using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class ContainsDuplicateTests
{
    private ContainsDuplicate _algorithm;
    
    [SetUp]
    public void Setup() => _algorithm = new ContainsDuplicate();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        // Assert
        Assert.That(_algorithm.Name, Is.EqualTo("Contains Duplicate"));

    [Test]
    public void Category_ReturnsCorrectValue() =>
        // Assert
        Assert.That(_algorithm.Category, Is.EqualTo("Array"));

    [Test]
    public void ValidateInput_WithValidArray_ReturnsTrue()
    {
        // Arrange
        int[] input = new[] { 1, 2, 3, 4, 5 };
        
        // Act
        bool result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void ValidateInput_WithEmptyArray_ReturnsFalse()
    {
        // Arrange
        int[] input = new int[0];
        
        // Act
        bool result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateInput_WithNonArrayInput_ReturnsFalse()
    {
        // Arrange
        string input = "not an array";
        
        // Act
        bool result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void GenerateSampleInput_ReturnsArrayOfSpecifiedSize()
    {
        // Arrange
        int size = 10;
        
        // Act
        object result = _algorithm.GenerateSampleInput(size);
        
        // Assert
        Assert.That(result, Is.TypeOf<int[]>());
        Assert.That(((int[])result).Length, Is.EqualTo(size));
    }

    [Test]
    public void ExecuteAsync_WithDuplicates_ReturnsTrue()
    {
        // Arrange
        int[] input = new[] { 1, 2, 3, 4, 5, 3 };
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        bool containsDuplicate = GetOutputValue(result);
        // Assert
        Assert.That(containsDuplicate, Is.EqualTo(true));
    }

    [Test]
    public void ExecuteAsync_WithoutDuplicates_ReturnsFalse()
    {
        // Arrange
        int[] input = new[] { 1, 2, 3, 4, 5 };
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        bool containsDuplicate = GetOutputValue(result);
        // Assert
        Assert.That(containsDuplicate, Is.EqualTo(false));
    }


    private bool GetOutputValue(AlgorithmResult result)
    {
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? valueProp = outputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
        return (bool)(valueProp?.GetValue(result.Output) ?? false);
    }
}
