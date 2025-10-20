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
            new GroupAnagrams()
        };

        _algorithmFactory = new AlgorithmFactory(_algorithms);
    }

    #region ContainsDuplicate Integration Tests

    [Test]
    public void ContainsDuplicate_WithDuplicates_ReturnsTrue()
    {
        // Arrange
        IAlgorithm algorithm = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        int[] input = { 1, 5, 9, 2, 5, 8 };

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
        int[] input = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

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
        int[] input = { 7, 7, 7, 7, 7 };

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
            
            // Verify another group contains "tan", "nat"
            List<string>? tanGroup = output.FirstOrDefault(g => g.Contains("tan"));
            Assert.That(tanGroup, Is.Not.Null);
            Assert.That(tanGroup!.Count, Is.EqualTo(2));
            
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
            Assert.That(algorithms.Count(), Is.EqualTo(5));
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
        int[] testArray = { 5, 2, 8, 2, 9, 1 };

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
        Tuple<string, string>[] testCases = new[]
        {
            Tuple.Create("listen", "silent"),
            Tuple.Create("hello", "world"),
            Tuple.Create("anagram", "nagaram"),
            Tuple.Create("rat", "car")
        };

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
