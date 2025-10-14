using AlgorithmApp.Algorithms.Array;
using static AlgorithmApp.Core.AppModels;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class ContainsDuplicateTests
{
    private ContainsDuplicate _algorithm;
    
    [SetUp]
    public void Setup()
    {
        _algorithm = new ContainsDuplicate();
    }
    
    [Test]
    public void Name_ReturnsCorrectValue()
    {
        // Assert
        Assert.That(_algorithm.Name, Is.EqualTo("Contains Duplicate"));
    }
    
    [Test]
    public void Category_ReturnsCorrectValue()
    {
        // Assert
        Assert.That(_algorithm.Category, Is.EqualTo("Array"));
    }
    
    [Test]
    public void ValidateInput_WithValidArray_ReturnsTrue()
    {
        // Arrange
        var input = new[] { 1, 2, 3, 4, 5 };
        
        // Act
        var result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void ValidateInput_WithEmptyArray_ReturnsFalse()
    {
        // Arrange
        var input = new int[0];
        
        // Act
        var result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateInput_WithNonArrayInput_ReturnsFalse()
    {
        // Arrange
        var input = "not an array";
        
        // Act
        var result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void GenerateSampleInput_ReturnsArrayOfSpecifiedSize()
    {
        // Arrange
        int size = 10;
        
        // Act
        var result = _algorithm.GenerateSampleInput(size);
        
        // Assert
        Assert.That(result, Is.TypeOf<int[]>());
        Assert.That(((int[])result).Length, Is.EqualTo(size));
    }

    [Test]
    public void ExecuteAsync_WithDuplicates_ReturnsTrue()
    {
        // Arrange
        var input = new[] { 1, 2, 3, 4, 5, 3 };
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        var containsDuplicate = GetOutputValue(result);
        // Assert
        Assert.That(containsDuplicate, Is.EqualTo(true));
    }

    [Test]
    public void ExecuteAsync_WithoutDuplicates_ReturnsFalse()
    {
        // Arrange
        var input = new[] { 1, 2, 3, 4, 5 };
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        var containsDuplicate = GetOutputValue(result);
        // Assert
        Assert.That(containsDuplicate, Is.EqualTo(false));
    }


    private bool GetOutputValue(AlgorithmResult result)
    {
        var outputProps = result.Output?.GetType().GetProperties();
        var valueProp = outputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
        return (bool)(valueProp?.GetValue(result.Output) ?? false);
    }
}