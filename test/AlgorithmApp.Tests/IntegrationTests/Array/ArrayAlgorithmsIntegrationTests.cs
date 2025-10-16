using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;
using static AlgorithmApp.Core.IService;

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
            new ValidAnagramDictionary()
        };

        _algorithmFactory = new AlgorithmFactory(_algorithms);
    }

    #region ContainsDuplicate Integration Tests

    [Test]
    public void ContainsDuplicate_WithDuplicates_ReturnsTrue()
    {
        // Arrange
        var algorithm = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        int[] input = { 1, 5, 9, 2, 5, 8 };

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var containsDuplicateProp = outputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
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
        var algorithm = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        int[] input = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var containsDuplicateProp = outputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
        bool containsDuplicate = (bool)(containsDuplicateProp?.GetValue(result.Output) ?? false);

        Assert.That(containsDuplicate, Is.False);
    }

    [Test]
    public void ContainsDuplicate_WithLargeDataset_HandlesEfficiently()
    {
        // Arrange
        var algorithm = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        var input = Enumerable.Range(1, 5000).ToArray();
        input = input.Concat(new[] { 100 }).ToArray(); // Add duplicate

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var containsDuplicateProp = outputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
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
        var algorithm = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        int[] input = { 7, 7, 7, 7, 7 };

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var containsDuplicateProp = outputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
        bool containsDuplicate = (bool)(containsDuplicateProp?.GetValue(result.Output) ?? false);

        Assert.That(containsDuplicate, Is.True);
    }

    #endregion

    #region ValidAnagramArray Integration Tests

    [Test]
    public void ValidAnagramArray_WithValidAnagrams_ReturnsTrue()
    {
        // Arrange
        var algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var input = Tuple.Create("listen", "silent");

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
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
        var algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var input = Tuple.Create("hello", "world");

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.That(isAnagram, Is.False);
    }

    [Test]
    public void ValidAnagramArray_WithDifferentLengths_ReturnsFalse()
    {
        // Arrange
        var algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var input = Tuple.Create("short", "muchlonger");

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
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
        var algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var input = Tuple.Create("conversation", "conservation");

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.That(isAnagram, Is.True);
    }

    [Test]
    public void ValidAnagramArray_WithMixedCase_HandlesCorrectly()
    {
        // Arrange
        var algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var input = Tuple.Create("Anagram", "Nagaram");

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.That(isAnagram, Is.True);
    }

    #endregion

    #region ValidAnagramDictionary Integration Tests

    [Test]
    public void ValidAnagramDictionary_WithValidAnagrams_ReturnsTrue()
    {
        // Arrange
        var algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");
        var input = Tuple.Create("anagram", "nagaram");

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
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
        var algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");
        var input = Tuple.Create("rat", "car");

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.That(isAnagram, Is.False);
    }

    [Test]
    public void ValidAnagramDictionary_WithRepeatedCharacters_HandlesCorrectly()
    {
        // Arrange
        var algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");
        var input = Tuple.Create("aabbcc", "abcabc");

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.That(isAnagram, Is.True);
    }

    [Test]
    public void ValidAnagramDictionary_WithLongStrings_PerformsWell()
    {
        // Arrange
        var algorithm = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");
        var str1 = new string('a', 500) + new string('b', 500);
        var str2 = new string('b', 500) + new string('a', 500);
        var input = Tuple.Create(str1, str2);

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var isAnagramProp = outputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
        bool isAnagram = (bool)(isAnagramProp?.GetValue(result.Output) ?? false);

        Assert.Multiple(() =>
        {
            Assert.That(isAnagram, Is.True);
            Assert.That(result.Steps, Is.Not.Empty);
        });
    }

    #endregion

    #region Cross-Algorithm Integration Tests

    [Test]
    public void AlgorithmFactory_CanRetrieveAllArrayAlgorithms()
    {
        // Act
        var arrayAlgorithms = _algorithmFactory.GetAlgorithmsByCategory("Array");

        // Assert
        Assert.Multiple(() =>
        {
            var algorithms = arrayAlgorithms as IAlgorithm[] ?? arrayAlgorithms.ToArray();
            Assert.That(algorithms, Is.Not.Empty);
            Assert.That(algorithms.Count(), Is.EqualTo(3));
            Assert.That(algorithms.Select(a => a.Name), 
                Contains.Item("Contains Duplicate"));
            Assert.That(algorithms.Select(a => a.Name), 
                Contains.Item("Valid Anagram (Array)"));
            Assert.That(algorithms.Select(a => a.Name), 
                Contains.Item("Valid Anagram (Dictionary)"));
        });
    }

    [Test]
    public void AllArrayAlgorithms_HaveRequiredProperties()
    {
        // Act
        var allAlgorithms = _algorithmFactory.GetAllAlgorithms();

        // Assert
        foreach (var algorithm in allAlgorithms)
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
        var allAlgorithms = _algorithmFactory.GetAllAlgorithms();

        // Act & Assert
        foreach (var algorithm in allAlgorithms)
        {
            var sampleInput = algorithm.GenerateSampleInput(10);
            Assert.That(sampleInput, Is.Not.Null, 
                $"Algorithm '{algorithm.Name}' failed to generate sample input");
        }
    }

    [Test]
    public void AllArrayAlgorithms_ValidateInputCorrectly()
    {
        // Arrange & Act
        var containsDuplicate = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        var validAnagramArray = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var validAnagramDict = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");

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
        });
    }

    [Test]
    public void MultipleAlgorithms_ExecuteSequentially_WorkCorrectly()
    {
        // Arrange
        var containsDuplicate = _algorithmFactory.GetAlgorithm("Contains Duplicate");
        int[] testArray = { 5, 2, 8, 2, 9, 1 };

        // Act
        var duplicateResult = containsDuplicate.ExecuteAsync(testArray);

        // Assert

        var duplicateOutputProps = duplicateResult.Output?.GetType().GetProperties();
        var containsDuplicateProp = duplicateOutputProps?.FirstOrDefault(p => p.Name == "ContainsDuplicate");
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
        var arrayImpl = _algorithmFactory.GetAlgorithm("Valid Anagram (Array)");
        var dictImpl = _algorithmFactory.GetAlgorithm("Valid Anagram (Dictionary)");
        var testCases = new[]
        {
            Tuple.Create("listen", "silent"),
            Tuple.Create("hello", "world"),
            Tuple.Create("anagram", "nagaram"),
            Tuple.Create("rat", "car")
        };

        // Act & Assert
        foreach (var testCase in testCases)
        {
            var arrayResult = arrayImpl.ExecuteAsync(testCase);
            var dictResult = dictImpl.ExecuteAsync(testCase);

            var arrayOutputProps = arrayResult.Output?.GetType().GetProperties();
            var arrayIsAnagramProp = arrayOutputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
            bool arrayIsAnagram = (bool)(arrayIsAnagramProp?.GetValue(arrayResult.Output) ?? false);

            var dictOutputProps = dictResult.Output?.GetType().GetProperties();
            var dictIsAnagramProp = dictOutputProps?.FirstOrDefault(p => p.Name == "IsAnagram");
            bool dictIsAnagram = (bool)(dictIsAnagramProp?.GetValue(dictResult.Output) ?? false);

            Assert.That(arrayIsAnagram, Is.EqualTo(dictIsAnagram),
                $"Results differ for input: {testCase.Item1}, {testCase.Item2}");
        }
    }

    #endregion
}
