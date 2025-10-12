using AlgorithmApp.Algorithms.Array;
using static AlgorithmApp.Core.AppModels;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class FindLargestNumberTests
{
    private FindLargestNumber _algorithm;
    
    [SetUp]
    public void Setup()
    {
        _algorithm = new FindLargestNumber();
    }
    
    [Test]
    public void Name_ReturnsCorrectValue()
    {
        // Assert
        Assert.That(_algorithm.Name, Is.EqualTo("Find Largest Number"));
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
        var input = new int[] { 1, 2, 3, 4, 5 };
        
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
    public void ValidateInput_WithNullInput_ReturnsFalse()
    {
        // Act
        var result = _algorithm.ValidateInput(null);
        
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
    public void ExecuteAsync_WithSingleElementArray_ReturnsCorrectResult()
    {
        // Arrange
        var input = new int[] { 42 };
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.AlgorithmName, Is.EqualTo("Find Largest Number"));
            Assert.That(GetOutputValue(result), Is.EqualTo(42));
            Assert.That(GetOutputIndex(result), Is.EqualTo(0));
        });
    }
    
    [Test]
    public void ExecuteAsync_WithMultipleElements_FindsLargestNumber()
    {
        // Arrange
        var input = new int[] { 10, 5, 7, 99, 42, 13 };
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(GetOutputValue(result), Is.EqualTo(99));
            Assert.That(GetOutputIndex(result), Is.EqualTo(3));
        });
    }
    
    [Test]
    public void ExecuteAsync_WithNegativeNumbers_FindsLargestNumber()
    {
        // Arrange
        var input = new int[] { -10, -5, -20, -1, -7 };
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(GetOutputValue(result), Is.EqualTo(-1));
            Assert.That(GetOutputIndex(result), Is.EqualTo(3));
        });
    }
    
    [Test]
    public void ExecuteAsync_WithDuplicateLargestNumbers_FindsFirstOccurrence()
    {
        // Arrange
        var input = new int[] { 10, 99, 5, 99, 42 };
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(GetOutputValue(result), Is.EqualTo(99));
            Assert.That(GetOutputIndex(result), Is.EqualTo(1)); // First occurrence
        });
    }
    
    [Test]
    public void ExecuteAsync_WithInvalidInput_ThrowsArgumentException()
    {
        // Arrange
        var input = new string[] { "not", "an", "integer", "array" };
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
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
    
    // Helper method to extract the Value property from the dynamic output
    private int GetOutputValue(AlgorithmResult result)
    {
        var outputProps = result.Output?.GetType().GetProperties();
        var valueProp = outputProps?.FirstOrDefault(p => p.Name == "Value");
        return valueProp != null ? Convert.ToInt32(valueProp.GetValue(result.Output)) : 0;
    }
    
    // Helper method to extract the Index property from the dynamic output
    private int GetOutputIndex(AlgorithmResult result)
    {
        var outputProps = result.Output?.GetType().GetProperties();
        var indexProp = outputProps?.FirstOrDefault(p => p.Name == "Index");
        return indexProp != null ? Convert.ToInt32(indexProp.GetValue(result.Output)) : -1;
    }
}