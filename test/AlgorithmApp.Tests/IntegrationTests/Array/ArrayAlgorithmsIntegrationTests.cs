using System.Reflection;
using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.IntegrationTests.Array;

[TestFixture]
public class ArrayAlgorithmsIntegrationTests
{
    private AlgorithmFactory _algorithmFactory = null!;
    private List<IAlgorithm> _algorithms = null!;

    [SetUp]
    public void Setup()
    {
        _algorithms = new List<IAlgorithm>
        {
            new ContainsDuplicate(),
            new ValidAnagramArray(),
            new ValidAnagramDictionary(),
            new TwoNumberSum(),
            new GroupAnagrams(),
            new TopKFrequentElements(),
            new EncodeAndDecodeString()
        };

        _algorithmFactory = new AlgorithmFactory(_algorithms);
    }

    #region ContainsDuplicate Integration Tests

    [Test]
    public void ContainsDuplicate_WithDuplicates_ReturnsTrue()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        int[] input = [1, 5, 9, 2, 5, 8];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? containsDuplicateProp = outputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
        bool containsDuplicate = (bool)(containsDuplicateProp?.GetValue(result.Output) ?? false);

        Assert.Multiple(() =>
        {
            Assert.That(containsDuplicate, Is.True);
            Assert.That(result.Steps, Is.Not.Empty);
            Assert.That(result.AlgorithmName, Is.EqualTo("Contains Duplicate"));
        });
    }

    [Test]
    public void ContainsDuplicate_WithoutDuplicates_ReturnsFalse()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        int[] input = [1, 2, 3, 4, 5, 6, 7, 8, 9];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? containsDuplicateProp = outputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
        bool containsDuplicate = (bool)(containsDuplicateProp?.GetValue(result.Output) ?? false);

        Assert.That(containsDuplicate, Is.False);
    }

    [Test]
    public void ContainsDuplicate_WithLargeDataset_HandlesEfficiently()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        int[] input = Enumerable.Range(1, 5000).ToArray();
        input = input.Concat(new[] { 100 }).ToArray(); // Add duplicate

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? containsDuplicateProp = outputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
        bool containsDuplicate = (bool)(containsDuplicateProp?.GetValue(result.Output) ?? false);

        Assert.Multiple(() =>
        {
            Assert.That(containsDuplicate, Is.True);
            Assert.That(result.Steps, Is.Not.Empty);
        });
    }

    [Test]
    public void ContainsDuplicate_WithAllSameNumbers_ReturnsTrue()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        int[] input = [7, 7, 7, 7, 7];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? containsDuplicateProp = outputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
        bool containsDuplicate = (bool)(containsDuplicateProp?.GetValue(result.Output) ?? false);

        Assert.That(containsDuplicate, Is.True);
    }

    #endregion

    #region ValidAnagramArray Integration Tests

    [Test]
    public void ValidAnagramArray_WithValidAnagrams_ReturnsTrue()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var input = Tuple.Create("listen", "silent");

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.Multiple(() =>
        {
            Assert.That(isAnagram, Is.True);
            Assert.That(result.Steps, Is.Not.Empty);
            Assert.That(result.AlgorithmName, Is.EqualTo("Valid Anagram (Array)"));
        });
    }

    [Test]
    public void ValidAnagramArray_WithNonAnagrams_ReturnsFalse()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var input = Tuple.Create("hello", "world");

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.That(isAnagram, Is.False);
    }

    [Test]
    public void ValidAnagramArray_WithDifferentLengths_ReturnsFalse()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var input = Tuple.Create("short", "muchlonger");

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.Multiple(() =>
        {
            Assert.That(isAnagram, Is.False);
            Assert.That(result.Steps, Does.Contain("Lengths differ. Returning false."));
        });
    }

    [Test]
    public void ValidAnagramArray_WithComplexAnagrams_ReturnsTrue()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var input = Tuple.Create("conversation", "conservation");

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.That(isAnagram, Is.True);
    }

    [Test]
    public void ValidAnagramArray_WithMixedCase_HandlesCorrectly()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var input = Tuple.Create("Anagram", "Nagaram");

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.That(isAnagram, Is.True);
    }

    #endregion

    #region ValidAnagramDictionary Integration Tests

    [Test]
    public void ValidAnagramDictionary_WithValidAnagrams_ReturnsTrue()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");
        var input = Tuple.Create("anagram", "nagaram");

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.Multiple(() =>
        {
            Assert.That(isAnagram, Is.True);
            Assert.That(result.Steps, Is.Not.Empty);
            Assert.That(result.AlgorithmName, Is.EqualTo("Valid Anagram (Dictionary)"));
        });
    }

    [Test]
    public void ValidAnagramDictionary_WithNonAnagrams_ReturnsFalse()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");
        var input = Tuple.Create("rat", "car");

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.That(isAnagram, Is.False);
    }

    [Test]
    public void ValidAnagramDictionary_WithRepeatedCharacters_HandlesCorrectly()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");
        var input = Tuple.Create("aabbcc", "abcabc");

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.That(isAnagram, Is.True);
    }

    [Test]
    public void ValidAnagramDictionary_WithLongStrings_PerformsWell()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");
        string str1 = new string('a', 500) + new string('b', 500);
        string str2 = new string('b', 500) + new string('a', 500);
        var input = Tuple.Create(str1, str2);

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);

        // Assert
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.Multiple(() =>
        {
            Assert.That(isAnagram, Is.True);
            Assert.That(result.Steps, Is.Not.Empty);
        });
    }

    #endregion

    #region GroupAnagrams Integration Tests

    [Test]
    public void GroupAnagrams_WithMultipleGroups_ReturnsCorrectGroups()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Group Anagrams");
        string[] input = ["eat", "tea", "tan", "ate", "nat", "bat"];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Count, Is.EqualTo(3));
            Assert.That(result.Steps, Is.Not.Empty);
            Assert.That(result.AlgorithmName, Is.EqualTo("Group Anagrams"));

            // Verify one group contains "eat", "tea", "ate"
            List<string>? eatGroup = output.FirstOrDefault(g => g.Contains("eat"));
            Assert.That(eatGroup, Is.Not.Null);
            Assert.That(eatGroup!.Count, Is.EqualTo(3));
            Assert.That(eatGroup, Does.Contain("tea"));
            Assert.That(eatGroup, Does.Contain("ate"));
            
            // Verify another group contains "tan", "nat"
            List<string>? tanGroup = output.FirstOrDefault(g => g.Contains("tan"));
            Assert.That(tanGroup, Is.Not.Null);
            Assert.That(tanGroup!.Count, Is.EqualTo(2));
            Assert.That(tanGroup, Does.Contain("nat"));
         
            // Verify bat is alone
            List<string>? batGroup = output.FirstOrDefault(g => g.Contains("bat"));
            Assert.That(batGroup, Is.Not.Null);
            Assert.That(batGroup!.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public void GroupAnagrams_WithSampleInput_GroupsCorrectly()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Group Anagrams");
        string[] input = ["listen", "silent", "enlist", "inlets", "google", "gogole"];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Count, Is.EqualTo(2));
            
            // Find the listen group
            List<string>? listenGroup = output.FirstOrDefault(g => g.Contains("listen"));
            Assert.That(listenGroup, Is.Not.Null);
            Assert.That(listenGroup!.Count, Is.EqualTo(4));
            Assert.That(listenGroup, Does.Contain("silent"));
            Assert.That(listenGroup, Does.Contain("enlist"));
            Assert.That(listenGroup, Does.Contain("inlets"));
            
            // Find the google group
            List<string>? googleGroup = output.FirstOrDefault(g => g.Contains("google"));
            Assert.That(googleGroup, Is.Not.Null);
            Assert.That(googleGroup!.Count, Is.EqualTo(2));
            Assert.That(googleGroup, Does.Contain("gogole"));
        });
    }

    [Test]
    public void GroupAnagrams_WithNoAnagrams_ReturnsIndividualGroups()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Group Anagrams");
        string[] input = ["abc", "def", "ghi"];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Count, Is.EqualTo(3));
            Assert.That(output.All(g => g.Count == 1), Is.True);
        });
    }

    [Test]
    public void GroupAnagrams_WithIdenticalStrings_GroupsTogether()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Group Anagrams");
        string[] input = ["test", "test", "test"];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Count, Is.EqualTo(1));
            Assert.That(output[0].Count, Is.EqualTo(3));
        });
    }

    [Test]
    public void GroupAnagrams_WithLargeDataset_HandlesEfficiently()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Group Anagrams");
        var largeInput = new List<string>();
        for (int i = 0; i < 100; i++)
        {
            largeInput.Add("listen");
            largeInput.Add("silent");
            // Create unique strings using different letter combinations
            largeInput.Add(new string((char)('a' + i % 26), 3));
        }

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(largeInput.ToArray());
        var output = (List<List<string>>)result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Count, Is.GreaterThanOrEqualTo(2)); // At least listen/silent group and some unique groups
            
            List<string>? listenGroup = output.FirstOrDefault(g => g.Contains("listen"));
            Assert.That(listenGroup, Is.Not.Null);
            Assert.That(listenGroup!.Count, Is.EqualTo(200)); // 100 listen + 100 silent
        });
    }

    [Test]
    public void GroupAnagrams_GenerateSampleInput_ReturnsValidStringArray()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Group Anagrams");

        // Act
        object sample = algorithm.GenerateSampleInput(10);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(sample, Is.TypeOf<string[]>());
            string[] stringArray = (string[])sample;
            Assert.That(stringArray.Length, Is.EqualTo(6)); // Sample input has 6 strings
            Assert.That(algorithm.ValidateInput(sample), Is.True);
        });
    }

    #endregion

    #region TwoNumberSum Integration Tests

    [Test]
    public void TwoNumberSum_WithValidPair_ReturnsCorrectIndices()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Two Number Sum");
        var input = Tuple.Create(new[] { 2, 7, 11, 15 }, 9);

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        int[][] output = (int[][])result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output[0][0], Is.EqualTo(0));
            Assert.That(output[0][1], Is.EqualTo(1));
            Assert.That(result.Steps.Any(s => s.Contains("Found pair")), Is.True);
        });
    }

    [Test]
    public void TwoNumberSum_WithNegativeNumbers_ReturnsValidPair()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Two Number Sum");
        var input = Tuple.Create(new[] { -3, -1, 2, 4, 6 }, 3);

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
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
        });
    }

    [Test]
    public void TwoNumberSum_GenerateSampleInput_ReturnsValidTuple()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Two Number Sum");

        // Act
        object sample = algorithm.GenerateSampleInput(10);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(sample, Is.TypeOf<Tuple<int[], int>>());
            var tuple = (Tuple<int[], int>)sample;
            Assert.That(tuple.Item1.Length, Is.EqualTo(10));
            Assert.That(algorithm.ValidateInput(sample), Is.True);
        });
    }

    #endregion

    #region TopKFrequentElements Integration Tests

    [Test]
    public void TopKFrequentElements_WithBasicExample_ReturnsTopK()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Top K Frequent Elements");
        var input = Tuple.Create(new[] { 1, 1, 1, 2, 2, 3 }, 2);

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        int[] output = (int[])result.Output!;

      // Assert
        Assert.Multiple(() =>
        {
         Assert.That(output, Is.Not.Null);
        Assert.That(output.Length, Is.EqualTo(2));
            Assert.That(output, Does.Contain(1)); // 1 appears 3 times
     Assert.That(output, Does.Contain(2)); // 2 appears 2 times
            Assert.That(result.Steps, Is.Not.Empty);
  Assert.That(result.AlgorithmName, Is.EqualTo("Top K Frequent Elements"));
        });
    }

    [Test]
    public void TopKFrequentElements_WithNegativeNumbers_HandlesCorrectly()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Top K Frequent Elements");
  var input = Tuple.Create(new[] { -1, -1, -2, -2, -2, 3 }, 2);

        // Act
   AlgorithmResult result = algorithm.ExecuteAsync(input);
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
    public void TopKFrequentElements_WithAllUniqueElements_ReturnsKElements()
    {
   // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Top K Frequent Elements");
        var input = Tuple.Create(new[] { 1, 2, 3, 4, 5 }, 3);

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
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
    public void TopKFrequentElements_WithKLargerThanUniqueCount_ReturnsAllUnique()
    {
        // Arrange
     IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Top K Frequent Elements");
        var input = Tuple.Create(new[] { 1, 1, 2, 2, 3, 3 }, 5);

      // Act
 AlgorithmResult result = algorithm.ExecuteAsync(input);
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
    public void TopKFrequentElements_WithLargeDataset_HandlesEfficiently()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Top K Frequent Elements");
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
        AlgorithmResult result = algorithm.ExecuteAsync(input);
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
    public void TopKFrequentElements_GenerateSampleInput_ReturnsValidTuple()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Top K Frequent Elements");

        // Act
        object sample = algorithm.GenerateSampleInput(20);

      // Assert
     Assert.Multiple(() =>
      {
            Assert.That(sample, Is.TypeOf<Tuple<int[], int>>());
            var tuple = (Tuple<int[], int>)sample;
            Assert.That(tuple.Item1.Length, Is.EqualTo(20));
    Assert.That(tuple.Item2, Is.GreaterThan(0));
            Assert.That(algorithm.ValidateInput(sample), Is.True);
        });
    }

    [Test]
    public void TopKFrequentElements_WithMixedFrequencies_ReturnsCorrectOrder()
    {
    // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Top K Frequent Elements");
   var input = Tuple.Create(new[] { 4, 4, 4, 4, 3, 3, 3, 2, 2, 1 }, 3);

    // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
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
    public void TopKFrequentElements_WithZeroValues_HandlesCorrectly()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Top K Frequent Elements");
        var input = Tuple.Create(new[] { 0, 0, 0, 1, 1, 2 }, 2);

        // Act
 AlgorithmResult result = algorithm.ExecuteAsync(input);
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

    #endregion

    #region EncodeAndDecodeString Integration Tests

    [Test]
    public void EncodeAndDecodeString_WithSimpleStrings_EncodesAndDecodesCorrectly()
 {
        // Arrange
    IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
      string[] input = ["hello", "world"];

      // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

        // Assert
     Assert.Multiple(() =>
        {
     Assert.That(output.Encoded, Is.Not.Null);
      Assert.That(output.Decoded, Is.Not.Null);
   Assert.That(output.Decoded.Length, Is.EqualTo(2));
            Assert.That(output.Decoded[0], Is.EqualTo("hello"));
  Assert.That(output.Decoded[1], Is.EqualTo("world"));
            Assert.That(result.Steps, Is.Not.Empty);
            Assert.That(result.AlgorithmName, Is.EqualTo("Encode and Decode String"));
        });
    }

    [Test]
    public void EncodeAndDecodeString_WithEmptyStrings_HandlesCorrectly()
{
        // Arrange
    IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
        string[] input = ["", "test", ""];

 // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

     // Assert
        Assert.Multiple(() =>
        {
         Assert.That(output.Decoded.Length, Is.EqualTo(3));
            Assert.That(output.Decoded[0], Is.EqualTo(""));
Assert.That(output.Decoded[1], Is.EqualTo("test"));
            Assert.That(output.Decoded[2], Is.EqualTo(""));
        });
    }

    [Test]
    public void EncodeAndDecodeString_WithHashSymbols_HandlesCorrectly()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
   string[] input = ["hello#world", "test#123", "no#hash#here"];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
     dynamic output = result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
     Assert.That(output.Decoded.Length, Is.EqualTo(3));
            Assert.That(output.Decoded[0], Is.EqualTo("hello#world"));
Assert.That(output.Decoded[1], Is.EqualTo("test#123"));
          Assert.That(output.Decoded[2], Is.EqualTo("no#hash#here"));
        });
    }

    [Test]
    public void EncodeAndDecodeString_WithSpecialCharacters_HandlesCorrectly()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
   string[] input = ["hello@world", "test!123", "special$chars%", "symbols&*()"];

        // Act
    AlgorithmResult result = algorithm.ExecuteAsync(input);
  dynamic output = result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
   Assert.That(output.Decoded.Length, Is.EqualTo(4));
     Assert.That(output.Decoded[0], Is.EqualTo("hello@world"));
      Assert.That(output.Decoded[1], Is.EqualTo("test!123"));
 Assert.That(output.Decoded[2], Is.EqualTo("special$chars%"));
            Assert.That(output.Decoded[3], Is.EqualTo("symbols&*()"));
        });
    }

    [Test]
    public void EncodeAndDecodeString_WithUnicodeCharacters_HandlesCorrectly()
    {
     // Arrange
 IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
        string[] input = ["hello", "??", "??????", "????"];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
dynamic output = result.Output!;

        // Assert
        Assert.Multiple(() =>
    {
         Assert.That(output.Decoded.Length, Is.EqualTo(4));
        Assert.That(output.Decoded[0], Is.EqualTo("hello"));
          Assert.That(output.Decoded[1], Is.EqualTo("??"));
     Assert.That(output.Decoded[2], Is.EqualTo("??????"));
 Assert.That(output.Decoded[3], Is.EqualTo("????"));
      });
    }

    [Test]
    public void EncodeAndDecodeString_WithLongStrings_HandlesEfficiently()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
        string longString = new string('a', 1000);
    string[] input = [longString, "short", longString];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

        // Assert
     Assert.Multiple(() =>
   {
    Assert.That(output.Decoded.Length, Is.EqualTo(3));
      Assert.That(output.Decoded[0], Is.EqualTo(longString));
       Assert.That(output.Decoded[1], Is.EqualTo("short"));
            Assert.That(output.Decoded[2], Is.EqualTo(longString));
        });
    }

    [Test]
    public void EncodeAndDecodeString_WithVaryingLengths_HandlesCorrectly()
    {
      // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
        string[] input = ["a", "ab", "abc", "abcd", "abcdefghij", new string('x', 100)];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
Assert.That(output.Decoded.Length, Is.EqualTo(6));
    Assert.That(output.Decoded[0], Is.EqualTo("a"));
            Assert.That(output.Decoded[1], Is.EqualTo("ab"));
            Assert.That(output.Decoded[2], Is.EqualTo("abc"));
   Assert.That(output.Decoded[3], Is.EqualTo("abcd"));
     Assert.That(output.Decoded[4], Is.EqualTo("abcdefghij"));
  Assert.That(output.Decoded[5], Is.EqualTo(new string('x', 100)));
    });
    }

    [Test]
    public void EncodeAndDecodeString_WithSampleInput_WorksCorrectly()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
        string[] input = ["listen", "silent", "enlist", "inlets", "google", "gogole"];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

   // Assert
        Assert.Multiple(() =>
        {
    Assert.That(output.Decoded.Length, Is.EqualTo(6));
  Assert.That(output.Decoded[0], Is.EqualTo("listen"));
            Assert.That(output.Decoded[1], Is.EqualTo("silent"));
            Assert.That(output.Decoded[2], Is.EqualTo("enlist"));
   Assert.That(output.Decoded[3], Is.EqualTo("inlets"));
      Assert.That(output.Decoded[4], Is.EqualTo("google"));
            Assert.That(output.Decoded[5], Is.EqualTo("gogole"));
      });
    }

    [Test]
    public void EncodeAndDecodeString_WithLargeDataset_HandlesEfficiently()
    {
  // Arrange
    IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
        string[] input = Enumerable.Range(1, 100).Select(i => $"string{i}").ToArray();

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output.Decoded.Length, Is.EqualTo(100));
        for (int i = 0; i < 100; i++)
            {
   Assert.That(output.Decoded[i], Is.EqualTo($"string{i + 1}"));
  }
        });
    }

    [Test]
    public void EncodeAndDecodeString_PreservesOrder()
    {
     // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
        string[] input = ["first", "second", "third", "fourth", "fifth"];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;
        string[] decoded = output.Decoded;

   // Assert
        Assert.That(decoded, Is.EqualTo(input));
    }

    [Test]
    public void EncodeAndDecodeString_WithIdenticalStrings_HandlesCorrectly()
    {
     // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
     string[] input = ["same", "same", "same", "same"];

      // Act
  AlgorithmResult result = algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;
        string[] decoded = output.Decoded;

  // Assert
    Assert.Multiple(() =>
 {
    Assert.That(decoded.Length, Is.EqualTo(4));
            Assert.That(decoded.All(s => s == "same"), Is.True);
        });
    }

    [Test]
    public void EncodeAndDecodeString_WithWhitespace_HandlesCorrectly()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
      string[] input = [" ", "  ", "hello world", " test ", "  multiple  spaces  "];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

      // Assert
        Assert.Multiple(() =>
     {
            Assert.That(output.Decoded.Length, Is.EqualTo(5));
Assert.That(output.Decoded[0], Is.EqualTo(" "));
            Assert.That(output.Decoded[1], Is.EqualTo("  "));
            Assert.That(output.Decoded[2], Is.EqualTo("hello world"));
     Assert.That(output.Decoded[3], Is.EqualTo(" test "));
            Assert.That(output.Decoded[4], Is.EqualTo("  multiple  spaces  "));
        });
    }

    [Test]
    public void EncodeAndDecodeString_EncodedFormatIsCorrect()
    {
  // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
        string[] input = ["abc", "de"];

    // Act
     AlgorithmResult result = algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;
     string encoded = output.Encoded;

  // Assert
    Assert.Multiple(() =>
        {
            // Format should be: 3#abc2#de
      Assert.That(encoded, Does.StartWith("3#abc"));
  Assert.That(encoded, Does.Contain("2#de"));
          Assert.That(encoded, Is.EqualTo("3#abc2#de"));
        });
    }

    [Test]
    public void EncodeAndDecodeString_GenerateSampleInput_ReturnsValidStringArray()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");

   // Act
        object sample = algorithm.GenerateSampleInput(10);

        // Assert
        Assert.Multiple(() =>
        {
    Assert.That(sample, Is.TypeOf<string[]>());
       string[] stringArray = (string[])sample;
        Assert.That(stringArray.Length, Is.EqualTo(6));
            Assert.That(algorithm.ValidateInput(sample), Is.True);
        });
    }

    [Test]
    public void EncodeAndDecodeString_RoundTripIntegrity()
    {
        // Arrange
IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Encode and Decode String");
     string[] testCases = [
    "simple",
            "",
      "#hash#",
 "123",
          "special!@#$%^&*()",
       new string('a', 50),
            "unicode??",
       "spaces  here"
  ];

        // Act
        AlgorithmResult result = algorithm.ExecuteAsync(testCases);
        dynamic output = result.Output!;
        string[] decoded = output.Decoded;

        // Assert
        Assert.That(decoded, Is.EqualTo(testCases));
    }

    #endregion

    #region Cross-Algorithm Integration Tests

    [Test]
    public void AlgorithmFactory_CanRetrieveAllArrayAlgorithms()
    {
        // Act
        IEnumerable<IAlgorithm> arrayAlgorithms = _algorithmFactory.GetAlgorithmsByCategory("Array");

        // Assert
        Assert.Multiple(() =>
    {
IAlgorithm[] algorithms = arrayAlgorithms as IAlgorithm[] ?? arrayAlgorithms.ToArray();
        Assert.That(algorithms, Is.Not.Empty);
            Assert.That(algorithms.Count(), Is.EqualTo(7));
          Assert.That(algorithms.Select(a => a.Name),
     Contains.Item("Contains Duplicate"));
 Assert.That(algorithms.Select(a => a.Name),
    Contains.Item("Valid Anagram (Array)"));
        Assert.That(algorithms.Select(a => a.Name),
     Contains.Item("Valid Anagram (Dictionary)"));
            Assert.That(algorithms.Select(a => a.Name),
     Contains.Item("Two Number Sum"));
          Assert.That(algorithms.Select(a => a.Name),
            Contains.Item("Group Anagrams"));
    Assert.That(algorithms.Select(a => a.Name),
            Contains.Item("Top K Frequent Elements"));
            Assert.That(algorithms.Select(a => a.Name),
   Contains.Item("Encode and Decode String"));
      });
    }

    [Test]
    public void AllArrayAlgorithms_HaveRequiredProperties()
    {
        // Act
        IEnumerable<IAlgorithm> allAlgorithms = _algorithmFactory.GetAllAlgorithms();

        // Assert
        foreach (IAlgorithm? algorithm in allAlgorithms)
        {
            Assert.Multiple(() =>
            {
                Assert.That(algorithm.Name, Is.Not.Null.And.Not.Empty);
                Assert.That(algorithm.Category, Is.Not.Null.And.Not.Empty);
                Assert.That(algorithm.Description, Is.Not.Null.And.Not.Empty);
                Assert.That(algorithm.TimeComplexity, Is.Not.Null.And.Not.Empty);
                Assert.That(algorithm.SpaceComplexity, Is.Not.Null.And.Not.Empty);
                Assert.That(algorithm.Hint, Is.Not.Null.And.Not.Empty);
            });
        }
    }

    [Test]
    public void AllArrayAlgorithms_CanGenerateSampleInput()
    {
        // Arrange
        IEnumerable<IAlgorithm> allAlgorithms = _algorithmFactory.GetAllAlgorithms();

        // Act & Assert
        foreach (IAlgorithm? algorithm in allAlgorithms)
        {
            object sampleInput = algorithm.GenerateSampleInput(10);
            Assert.That(sampleInput, Is.Not.Null,
                $"Algorithm '{algorithm.Name}' failed to generate sample input");
        }
    }

    [Test]
    public void AllArrayAlgorithms_ValidateInputCorrectly()
    {
        // Arrange & Act
        IAlgorithm containsDuplicate = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        IAlgorithm validAnagramArray = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        IAlgorithm validAnagramDict = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");
        IAlgorithm twoNumberSum = _algorithmFactory.GetAlgorithm("Two Number Sum");
        IAlgorithm groupAnagrams = _algorithmFactory.GetAlgorithm("Group Anagrams");

        // Assert
        Assert.Multiple(() =>
        {
            //ContainsDuplicate should accept int arrays
            Assert.That(containsDuplicate.ValidateInput(new[] { 1, 2, 3 }), Is.True);
            Assert.That(containsDuplicate.ValidateInput(new int[0]), Is.False);

            // ValidAnagram algorithms should accept tuples of strings
            Assert.That(validAnagramArray.ValidateInput(Tuple.Create("test", "tset")), Is.True);
            Assert.That(validAnagramDict.ValidateInput(Tuple.Create("test", "tset")), Is.True);
            Assert.That(validAnagramArray.ValidateInput(Tuple.Create("", "test")), Is.False);
            Assert.That(validAnagramDict.ValidateInput(Tuple.Create("", "test")), Is.False);

            // TwoNumberSum should accept tuple of (int[] >=2, int)
            Assert.That(twoNumberSum.ValidateInput(Tuple.Create(new[] { 1, 2 }, 3)), Is.True);
            Assert.That(twoNumberSum.ValidateInput(Tuple.Create(System.Array.Empty<int>(), 3)), Is.False);
            Assert.That(twoNumberSum.ValidateInput(new[] { 1, 2, 3 }), Is.False);

            // GroupAnagrams should accept non-empty string arrays
            Assert.That(groupAnagrams.ValidateInput(new[] { "test", "tset" }), Is.True);
            Assert.That(groupAnagrams.ValidateInput(System.Array.Empty<string>()), Is.False);
            Assert.That(groupAnagrams.ValidateInput("not an array"), Is.False);
        });
    }

    [Test]
    public void MultipleAlgorithms_ExecuteSequentially_WorkCorrectly()
    {
        // Arrange
        IAlgorithm containsDuplicate = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        int[] testArray = [5, 2, 8, 2, 9, 1];

        // Act
        AlgorithmResult duplicateResult = containsDuplicate.ExecuteAsync(testArray);

        // Assert

        PropertyInfo[]? duplicateOutputProps = duplicateResult.Output?.GetType().GetProperties();
        PropertyInfo? containsDuplicateProp = duplicateOutputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
        bool hasDuplicate = (bool)(containsDuplicateProp?.GetValue(duplicateResult.Output) ?? false);

        Assert.Multiple(() =>
        {
            Assert.That(hasDuplicate, Is.True);
            Assert.That(duplicateResult.Steps, Is.Not.Empty);
        });
    }

    [Test]
    public void BothAnagramAlgorithms_ProduceSameResults()
    {
        // Arrange
        IAlgorithm arrayImpl = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        IAlgorithm dictImpl = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");
        Tuple<string, string>[] testCases =
        [
            Tuple.Create("listen", "silent"),
            Tuple.Create("hello", "world"),
            Tuple.Create("anagram", "nagaram"),
            Tuple.Create("rat", "car")
        ];

        // Act & Assert
        foreach (Tuple<string, string>? testCase in testCases)
        {
            AlgorithmResult arrayResult = arrayImpl.ExecuteAsync(testCase);
            AlgorithmResult dictResult = dictImpl.ExecuteAsync(testCase);

            PropertyInfo[]? arrayOutputProps = arrayResult.Output?.GetType()?.GetProperties();
            PropertyInfo? arrayIsAnagramProp = arrayOutputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
            bool arrayIsAnagram = (bool)(arrayIsAnagramProp?.GetValue(arrayResult.Output) ?? false);

            PropertyInfo[]? dictOutputProps = dictResult.Output?.GetType()?.GetProperties();
            PropertyInfo? dictIsAnagramProp = dictOutputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
            bool dictIsAnagram = (bool)(dictIsAnagramProp?.GetValue(dictResult.Output) ?? false);

            Assert.That(arrayIsAnagram, Is.EqualTo(dictIsAnagram),
                $"Results differ for input: {testCase.Item1}, {testCase.Item2}");
        }
    }

    #endregion
}
