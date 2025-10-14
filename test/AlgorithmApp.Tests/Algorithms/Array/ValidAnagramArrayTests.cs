using AlgorithmApp.Algorithms.Array;
using static AlgorithmApp.Core.AppModels;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class ValidAnagramArrayTests
{
    private ValidAnagramArray _algorithm = null!;
    
    [SetUp]
    public void Setup()
    {
        _algorithm = new ValidAnagramArray();
    }
    
    [Test]
    public void Name_ReturnsCorrectValue()
    {
        // Assert
        Assert.That(_algorithm.Name, Is.EqualTo("Valid Anagram (Array)"));
    }
    
    [Test]
    public void Description_ReturnsCorrectValue()
    {
        // Assert
        Assert.That(_algorithm.Description, Does.Contain("array"));
    }
    
    [Test]
    public void Category_ReturnsCorrectValue()
    {
        // Assert
        Assert.That(_algorithm.Category, Is.EqualTo("Array"));
    }
    
    [Test]
    public void TimeComplexity_ReturnsCorrectValue()
    {
        // Assert
        Assert.That(_algorithm.TimeComplexity, Is.EqualTo("O(n)"));
    }
    
    [Test]
    public void SpaceComplexity_ReturnsCorrectValue()
    {
        // Assert
        Assert.That(_algorithm.SpaceComplexity, Is.EqualTo("O(1)"));
    }
    
    [Test]
    public void ValidateInput_WithValidTuple_ReturnsTrue()
    {
        // Arrange
        var input = Tuple.Create("anagram", "nagaram");
        
        // Act
        var result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void ValidateInput_WithNullFirstString_ReturnsFalse()
    {
        // Arrange
        var input = Tuple.Create((string?)null, "test");
        
        // Act
        var result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateInput_WithEmptyFirstString_ReturnsFalse()
    {
        // Arrange
        var input = Tuple.Create("", "test");
        
        // Act
        var result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateInput_WithNullSecondString_ReturnsFalse()
    {
        // Arrange
        var input = Tuple.Create("test", (string?)null);
        
        // Act
        var result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateInput_WithEmptySecondString_ReturnsFalse()
    {
        // Arrange
        var input = Tuple.Create("test", "");
        
        // Act
        var result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateInput_WithNonTupleInput_ReturnsFalse()
    {
        // Arrange
        var input = "not a tuple";
        
        // Act
        var result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void GenerateSampleInput_ReturnsTupleOfStrings()
    {
        // Arrange
        int size = 10;
        
        // Act
        var result = _algorithm.GenerateSampleInput(size);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.TypeOf<Tuple<string, string>>());
            var tuple = (Tuple<string, string>)result;
            Assert.That(tuple.Item1.Length, Is.EqualTo(size));
            Assert.That(tuple.Item2.Length, Is.EqualTo(size));
        });
    }

    [Test]
    public void ExecuteAsync_WithValidAnagrams_ReturnsTrue()
    {
        // Arrange
        var input = Tuple.Create("anagram", "nagaram");
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        var isAnagram = GetOutputValue(result);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(isAnagram, Is.True);
            Assert.That(result.Steps, Is.Not.Empty);
            Assert.That(result.AlgorithmName, Is.EqualTo(_algorithm.Name));
        });
    }

    [Test]
    public void ExecuteAsync_WithNonAnagrams_ReturnsFalse()
    {
        // Arrange
        var input = Tuple.Create("rat", "car");
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        var isAnagram = GetOutputValue(result);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(isAnagram, Is.False);
            Assert.That(result.Steps, Is.Not.Empty);
        });
    }

    [Test]
    public void ExecuteAsync_WithDifferentLengths_ReturnsFalse()
    {
        // Arrange
        var input = Tuple.Create("a", "ab");
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        var isAnagram = GetOutputValue(result);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(isAnagram, Is.False);
            Assert.That(result.Steps, Contains.Item("Lengths differ. Returning false."));
        });
    }

    [Test]
    public void ExecuteAsync_WithSingleCharacterAnagrams_ReturnsTrue()
    {
        // Arrange
        var input = Tuple.Create("a", "a");
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        var isAnagram = GetOutputValue(result);
        
        // Assert
        Assert.That(isAnagram, Is.True);
    }

    [Test]
    public void ExecuteAsync_WithRepeatedCharacters_ReturnsCorrectResult()
    {
        // Arrange
        var input = Tuple.Create("aabbcc", "abcabc");
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        var isAnagram = GetOutputValue(result);
        
        // Assert
        Assert.That(isAnagram, Is.True);
    }

    [Test]
    public void ExecuteAsync_WithDifferentCharacterCounts_ReturnsFalse()
    {
        // Arrange
        var input = Tuple.Create("aab", "abb");
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        var isAnagram = GetOutputValue(result);
        
        // Assert
        Assert.That(isAnagram, Is.False);
    }

    [Test]
    public void ExecuteAsync_WithMixedCaseAnagrams_ReturnsTrue()
    {
        // Arrange
        var input = Tuple.Create("Listen", "Silent");
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        var isAnagram = GetOutputValue(result);
        
        // Assert
        Assert.That(isAnagram, Is.True);
    }

    [Test]
    public void ExecuteAsync_WithComplexAnagrams_ReturnsTrue()
    {
        // Arrange
        var input = Tuple.Create("conversation", "conservation");
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        var isAnagram = GetOutputValue(result);
        
        // Assert
        Assert.That(isAnagram, Is.True);
    }

    [Test]
    public void ExecuteAsync_WithInvalidInput_ThrowsArgumentException()
    {
        // Arrange
        var input = "invalid input";
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }

    [Test]
    public void ExecuteAsync_WithEmptyStrings_ThrowsArgumentException()
    {
        // Arrange
        var input = Tuple.Create("", "");
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }

    [Test]
    public void ExecuteAsync_ResultContainsAllRequiredProperties()
    {
        // Arrange
        var input = Tuple.Create("test", "sett");
        
        // Act
        var result = _algorithm.ExecuteAsync(input);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.AlgorithmName, Is.Not.Null.And.Not.Empty);
            Assert.That(result.Input, Is.Not.Null);
            Assert.That(result.Output, Is.Not.Null);
            Assert.That(result.Steps, Is.Not.Null.And.Not.Empty);
        });
    }

    private static bool GetOutputValue(AlgorithmResult result)
    {
        var outputProps = result.Output?.GetType().GetProperties();
        var valueProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        return (bool)(valueProp?.GetValue(result.Output) ?? false);
    }
}
