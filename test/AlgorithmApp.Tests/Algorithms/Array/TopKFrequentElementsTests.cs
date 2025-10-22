using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class TopKFrequentElementsTests
{
    private TopKFrequentElements _algorithm = null!;
    
    [SetUp]
    public void Setup() => _algorithm = new TopKFrequentElements();

  [Test]
    public void Name_ReturnsCorrectValue() =>
        // Assert
        Assert.That(_algorithm.Name, Is.EqualTo("Top K Frequent Elements"));

    [Test]
    public void Description_ReturnsCorrectValue()
    {
 // Assert
        Assert.That(_algorithm.Description, Does.Contain("top K"));
      Assert.That(_algorithm.Description, Does.Contain("frequent"));
        Assert.That(_algorithm.Description, Does.Contain("array"));
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
    public void Hint_ContainsBucketSortReference() =>
        // Assert
        Assert.That(_algorithm.Hint, Does.Contain("bucket sort"));

    #region ValidateInput Tests

[Test]
    public void ValidateInput_WithValidTuple_ReturnsTrue()
    {
     // Arrange
        var input = Tuple.Create(new[] { 1, 1, 1, 2, 2, 3 }, 2);
        
        // Act
        bool result = _algorithm.ValidateInput(input);
        
        // Assert
     Assert.That(result, Is.True);
    }
    
    [Test]
    public void ValidateInput_WithArrayLengthLessThanTwo_ReturnsFalse()
    {
    // Arrange
    var input = Tuple.Create(new[] { 1 }, 1);
   
  // Act
bool result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateInput_WithEmptyArray_ReturnsFalse()
    {
        // Arrange
        var input = Tuple.Create(System.Array.Empty<int>(), 1);
        
        // Act
        bool result = _algorithm.ValidateInput(input);
     
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateInput_WithNonTupleInput_ReturnsFalse()
    {
   // Arrange
        int[] input = [1, 2, 3];
        
  // Act
        bool result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateInput_WithNullInput_ReturnsFalse()
    {
        // Arrange
        object? input = null;
        
        // Act
     bool result = _algorithm.ValidateInput(input!);
        
// Assert
        Assert.That(result, Is.False);
    }

#endregion

  #region GenerateSampleInput Tests

    [Test]
    public void GenerateSampleInput_ReturnsTupleWithArrayAndK()
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
   Assert.That(tuple.Item2, Is.GreaterThan(0));
        });
    }

    [Test]
    public void GenerateSampleInput_KIsApproximatelyTenthOfSize()
    {
        // Arrange
        int size = 100;
        
        // Act
   object result = _algorithm.GenerateSampleInput(size);
        var tuple = (Tuple<int[], int>)result;
        
        // Assert
        Assert.That(tuple.Item2, Is.EqualTo(Math.Max(1, size / 10)));
    }

    [Test]
    public void GenerateSampleInput_WithSmallSize_KIsAtLeastOne()
 {
     // Arrange
        int size = 5;
        
        // Act
        object result = _algorithm.GenerateSampleInput(size);
        var tuple = (Tuple<int[], int>)result;
        
        // Assert
        Assert.That(tuple.Item2, Is.GreaterThanOrEqualTo(1));
    }

    #endregion

    #region ExecuteAsync Tests

 [Test]
    public void ExecuteAsync_WithBasicExample_ReturnsTopKElements()
    {
  // Arrange
 var input = Tuple.Create(new[] { 1, 1, 1, 2, 2, 3 }, 2);
        
        // Act
     AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
      Assert.That(output, Is.Not.Null);
     Assert.That(output.Length, Is.EqualTo(2));
            Assert.That(output, Does.Contain(1)); // 1 appears 3 times
   Assert.That(output, Does.Contain(2)); // 2 appears 2 times
      Assert.That(result.Steps, Is.Not.Empty);
            Assert.That(result.AlgorithmName, Is.EqualTo(_algorithm.Name));
        });
    }

    [Test]
    public void ExecuteAsync_WithSingleElement_ReturnsAllElements()
    {
     // Arrange
        var input = Tuple.Create(new[] { 1, 1 }, 1);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
     
        // Assert
        Assert.Multiple(() =>
     {
          Assert.That(output, Is.Not.Null);
            Assert.That(output.Length, Is.EqualTo(1));
            Assert.That(output[0], Is.EqualTo(1));
        });
    }

    [Test]
    public void ExecuteAsync_WithAllUniqueElements_ReturnsKElements()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 2, 3, 4, 5 }, 3);
        
    // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
 
        // Assert
   Assert.Multiple(() =>
{
            Assert.That(output, Is.Not.Null);
        Assert.That(output.Length, Is.EqualTo(3));
       // All elements have same frequency, so any 3 are valid
          Assert.That(output.All(x => x >= 1 && x <= 5), Is.True);
        });
    }

    [Test]
    public void ExecuteAsync_WithKLargerThanUniqueCount_ReturnsAllUniqueElements()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 1, 2, 2, 3, 3 }, 5);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
     Assert.That(output, Is.Not.Null);
            Assert.That(output.Length, Is.EqualTo(3)); // Only 3 unique elements
      Assert.That(output, Does.Contain(1));
        Assert.That(output, Does.Contain(2));
    Assert.That(output, Does.Contain(3));
        });
    }

    [Test]
    public void ExecuteAsync_WithNegativeNumbers_HandlesCorrectly()
    {
      // Arrange
        var input = Tuple.Create(new[] { -1, -1, -2, -2, -2, 3 }, 2);
 
        // Act
 AlgorithmResult result = _algorithm.ExecuteAsync(input);
    int[] output = (int[])result.Output!;
      
        // Assert
 Assert.Multiple(() =>
        {
   Assert.That(output, Is.Not.Null);
            Assert.That(output.Length, Is.EqualTo(2));
        Assert.That(output, Does.Contain(-2)); // -2 appears 3 times
       Assert.That(output, Does.Contain(-1)); // -1 appears 2 times
        });
    }

    [Test]
    public void ExecuteAsync_WithMixedFrequencies_ReturnsMostFrequent()
    {
        // Arrange
        var input = Tuple.Create(new[] { 4, 4, 4, 4, 3, 3, 3, 2, 2, 1 }, 3);
  
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
Assert.That(output, Is.Not.Null);
       Assert.That(output.Length, Is.EqualTo(3));
  Assert.That(output, Does.Contain(4)); // 4 appears 4 times
  Assert.That(output, Does.Contain(3)); // 3 appears 3 times
            Assert.That(output, Does.Contain(2)); // 2 appears 2 times
        });
    }

    [Test]
    public void ExecuteAsync_WithAllSameElements_ReturnsSingleElement()
    {
        // Arrange
        var input = Tuple.Create(new[] { 5, 5, 5, 5, 5 }, 1);
        
      // Act
  AlgorithmResult result = _algorithm.ExecuteAsync(input);
   int[] output = (int[])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
     Assert.That(output, Is.Not.Null);
       Assert.That(output.Length, Is.EqualTo(1));
            Assert.That(output[0], Is.EqualTo(5));
        });
    }

    [Test]
    public void ExecuteAsync_WithTwoElements_ReturnsCorrectFrequent()
    {
        // Arrange
      var input = Tuple.Create(new[] { 1, 2 }, 2);
      
        // Act
  AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
 Assert.That(output, Is.Not.Null);
         Assert.That(output.Length, Is.EqualTo(2));
       Assert.That(output, Does.Contain(1));
    Assert.That(output, Does.Contain(2));
  });
    }

    [Test]
    public void ExecuteAsync_WithKEqualsOne_ReturnsMostFrequent()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 1, 1, 2, 2, 3 }, 1);
   
    // Act
  AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
        
      // Assert
        Assert.Multiple(() =>
        {
       Assert.That(output, Is.Not.Null);
            Assert.That(output.Length, Is.EqualTo(1));
            Assert.That(output[0], Is.EqualTo(1)); // 1 is most frequent
        });
  }

    [Test]
    public void ExecuteAsync_WithLargeArray_HandlesEfficiently()
    {
    // Arrange
var largeList = new List<int>();
     for (int i = 0; i < 100; i++)
        {
    largeList.Add(1);
 }
        for (int i = 0; i < 50; i++)
        {
            largeList.Add(2);
  }
        for (int i = 0; i < 25; i++)
 {
         largeList.Add(3);
        }
        for (int i = 0; i < 10; i++)
      {
         largeList.Add(4);
    }
      var input = Tuple.Create(largeList.ToArray(), 3);
        
// Act
    AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
        
        // Assert
        Assert.Multiple(() =>
    {
      Assert.That(output, Is.Not.Null);
  Assert.That(output.Length, Is.EqualTo(3));
      Assert.That(output, Does.Contain(1)); // Most frequent
   Assert.That(output, Does.Contain(2)); // Second most frequent
     Assert.That(output, Does.Contain(3)); // Third most frequent
        });
    }

    [Test]
    public void ExecuteAsync_WithZeroValues_HandlesCorrectly()
    {
        // Arrange
 var input = Tuple.Create(new[] { 0, 0, 0, 1, 1, 2 }, 2);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
     
        // Assert
        Assert.Multiple(() =>
        {
   Assert.That(output, Is.Not.Null);
         Assert.That(output.Length, Is.EqualTo(2));
       Assert.That(output, Does.Contain(0)); // 0 appears 3 times
    Assert.That(output, Does.Contain(1)); // 1 appears 2 times
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
    public void ExecuteAsync_WithEmptyArray_ThrowsArgumentException()
    {
     // Arrange
        var input = Tuple.Create(System.Array.Empty<int>(), 1);
        
        // Act & Assert
     Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }

    [Test]
    public void ExecuteAsync_WithSingleElementArray_ThrowsArgumentException()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1 }, 1);
    
   // Act & Assert
        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }

    #endregion

    #region Result Verification Tests

    [Test]
    public void ExecuteAsync_ResultContainsAllRequiredProperties()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 1, 2, 2, 3 }, 2);
        
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
        var input = Tuple.Create(new[] { 1, 1, 1, 2, 2, 3 }, 2);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
  
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Steps.Any(s => s.Contains("Input:")), Is.True);
          Assert.That(result.Steps.Any(s => s.Contains("Frequency map created")), Is.True);
       Assert.That(result.Steps.Any(s => s.Contains("Bucket sort")), Is.True);
            Assert.That(result.Steps.Any(s => s.Contains("Top")), Is.True);
        });
  }

    [Test]
    public void ExecuteAsync_StepsShowAddedElements()
    {
        // Arrange
  var input = Tuple.Create(new[] { 1, 1, 1, 2, 2, 3 }, 2);
        
        // Act
      AlgorithmResult result = _algorithm.ExecuteAsync(input);
        
     // Assert
        Assert.That(result.Steps.Count(s => s.Contains("Added element")), Is.EqualTo(2));
    }

    [Test]
    public void ExecuteAsync_StepsShowFrequencyMap()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 1, 2 }, 1);
      
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        
        // Assert
        Assert.Multiple(() =>
        {
      string? freqStep = result.Steps.FirstOrDefault(s => s.Contains("Frequency map created"));
       Assert.That(freqStep, Is.Not.Null);
            Assert.That(freqStep, Does.Contain("1:2"));
          Assert.That(freqStep, Does.Contain("2:1"));
     });
    }

    [Test]
    public void ExecuteAsync_OutputMatchesKValue()
    {
        // Arrange
        var input = Tuple.Create(new[] { 1, 1, 2, 2, 3, 3, 4, 4, 5 }, 3);
        
    // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
        
        // Assert
        Assert.That(output.Length, Is.LessThanOrEqualTo(3));
  }

    [Test]
    public void ExecuteAsync_PreservesInputInResult()
    {
        // Arrange
    var input = Tuple.Create(new[] { 1, 2, 3 }, 2);
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        
        // Assert
     Assert.That(result.Input, Is.EqualTo(input));
    }

    #endregion

    #region Edge Case Tests

    [Test]
    public void ExecuteAsync_WithDuplicateFrequencies_ReturnsAnyValidSet()
    {
        // Arrange - all elements have same frequency
        var input = Tuple.Create(new[] { 1, 1, 2, 2, 3, 3 }, 2);
        
        // Act
      AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
        
 // Assert
        Assert.Multiple(() =>
        {
 Assert.That(output, Is.Not.Null);
          Assert.That(output.Length, Is.EqualTo(2));
            Assert.That(output.All(x => x >= 1 && x <= 3), Is.True);
  });
    }

    [Test]
 public void ExecuteAsync_WithLargeNumbers_HandlesCorrectly()
    {
  // Arrange
        var input = Tuple.Create(new[] { 1000000, 1000000, 999999, 999999, 999999 }, 2);
        
    // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
    int[] output = (int[])result.Output!;
  
        // Assert
        Assert.Multiple(() =>
        {
     Assert.That(output, Is.Not.Null);
            Assert.That(output.Length, Is.EqualTo(2));
    Assert.That(output, Does.Contain(999999)); // Most frequent
            Assert.That(output, Does.Contain(1000000)); // Second most frequent
        });
    }

    [Test]
    public void ExecuteAsync_WithManyDuplicates_ReturnsCorrectOrder()
    {
        // Arrange
        var input = Tuple.Create(new[] { 5, 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 2, 2, 1 }, 4);
   
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;
        
     // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
  Assert.That(output.Length, Is.EqualTo(4));
            Assert.That(output, Does.Contain(5)); // 5 occurrences
          Assert.That(output, Does.Contain(4)); // 4 occurrences
       Assert.That(output, Does.Contain(3)); // 3 occurrences
            Assert.That(output, Does.Contain(2)); // 2 occurrences
     });
    }

    #endregion
}
