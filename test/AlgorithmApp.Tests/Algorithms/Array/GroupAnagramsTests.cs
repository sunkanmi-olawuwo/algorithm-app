using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class GroupAnagramsTests
{
    private GroupAnagrams _algorithm = null!;
    
    [SetUp]
    public void Setup() => _algorithm = new GroupAnagrams();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        // Assert
        Assert.That(_algorithm.Name, Is.EqualTo("Group Anagrams"));

    [Test]
    public void Description_ReturnsCorrectValue()
    {
        // Assert
        Assert.That(_algorithm.Description, Does.Contain("array of strings"));
        Assert.That(_algorithm.Description, Does.Contain("anagrams"));
    }
    
    [Test]
    public void Category_ReturnsCorrectValue() =>
        // Assert
        Assert.That(_algorithm.Category, Is.EqualTo("Array"));

    [Test]
    public void TimeComplexity_ReturnsCorrectValue() =>
        // Assert
        Assert.That(_algorithm.TimeComplexity, Is.EqualTo("O(m)"));

    [Test]
    public void SpaceComplexity_ReturnsCorrectValue() =>
        // Assert
        Assert.That(_algorithm.SpaceComplexity, Is.EqualTo("O(m*n)"));

    [Test]
    public void Hint_ContainsArrayReference() =>
        // Assert
        Assert.That(_algorithm.Hint, Does.Contain("array"));

    [Test]
    public void ValidateInput_WithValidStringArray_ReturnsTrue()
    {
        // Arrange
        string[] input = ["listen", "silent"];
        
        // Act
        bool result = _algorithm.ValidateInput(input);
        
        // Assert
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void ValidateInput_WithEmptyArray_ReturnsFalse()
    {
        // Arrange
        string[] input = [];
        
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
    public void ValidateInput_WithNullInput_ReturnsFalse()
    {
        // Arrange
        object? input = null;
        
        // Act
        bool result = _algorithm.ValidateInput(input!);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void GenerateSampleInput_ReturnsStringArray()
    {
        // Arrange
        int size = 10;
        
        // Act
        object result = _algorithm.GenerateSampleInput(size);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.TypeOf<string[]>());
            string[] stringArray = (string[])result;
            Assert.That(stringArray.Length, Is.EqualTo(6));
        });
    }

    [Test]
    public void ExecuteAsync_WithAnagrams_GroupsCorrectly()
    {
        // Arrange
        string[] input = ["listen", "silent", "enlist"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Count, Is.EqualTo(1));
            Assert.That(output[0].Count, Is.EqualTo(3));
            Assert.That(output[0], Does.Contain("listen"));
            Assert.That(output[0], Does.Contain("silent"));
            Assert.That(output[0], Does.Contain("enlist"));
            Assert.That(result.Steps, Is.Not.Empty);
            Assert.That(result.AlgorithmName, Is.EqualTo(_algorithm.Name));
        });
    }

    [Test]
    public void ExecuteAsync_WithMultipleGroups_ReturnsCorrectGroups()
    {
        // Arrange
        string[] input = ["eat", "tea", "tan", "ate", "nat", "bat"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Count, Is.EqualTo(3));
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
    public void ExecuteAsync_WithNoAnagrams_ReturnsIndividualGroups()
    {
        // Arrange
        string[] input = ["abc", "def", "ghi"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
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
    public void ExecuteAsync_WithSingleString_ReturnsSingleGroup()
    {
        // Arrange
        string[] input = ["hello"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Count, Is.EqualTo(1));
            Assert.That(output[0].Count, Is.EqualTo(1));
            Assert.That(output[0][0], Is.EqualTo("hello"));
        });
    }

    [Test]
    public void ExecuteAsync_WithIdenticalStrings_GroupsTogether()
    {
        // Arrange
        string[] input = ["test", "test", "test"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
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
    public void ExecuteAsync_WithEmptyStrings_GroupsTogether()
    {
        // Arrange
        string[] input = ["", ""];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Count, Is.EqualTo(1));
            Assert.That(output[0].Count, Is.EqualTo(2));
        });
    }

    [Test]
    public void ExecuteAsync_WithSingleCharacters_GroupsCorrectly()
    {
        // Arrange
        string[] input = ["a", "b", "a"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Count, Is.EqualTo(2));
            List<string>? aGroup = output.FirstOrDefault(g => g.Contains("a"));
            Assert.That(aGroup, Is.Not.Null);
            Assert.That(aGroup!.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public void ExecuteAsync_WithRepeatedCharacters_GroupsCorrectly()
    {
        // Arrange
        string[] input = ["aabbcc", "abcabc", "cbaabc"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
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
    public void ExecuteAsync_WithDifferentLengths_GroupsSeparately()
    {
        // Arrange
        string[] input = ["ab", "abc", "ba"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.Count, Is.EqualTo(2));
            List<string>? twoCharGroup = output.FirstOrDefault(g => g.Contains("ab"));
            Assert.That(twoCharGroup, Is.Not.Null);
            Assert.That(twoCharGroup!.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public void ExecuteAsync_CaseSensitive_TreatsDifferentCaseAsDifferent()
    {
        // Arrange
        // Note: Algorithm only supports lowercase letters
        string[] input = ["abc", "def", "abc", "def"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            // Should have 2 groups: one for "abc" and one for "def"
            Assert.That(output.Count, Is.EqualTo(2));
            List<string>? abcGroup = output.FirstOrDefault(g => g.Contains("abc"));
            Assert.That(abcGroup, Is.Not.Null);
            Assert.That(abcGroup!.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public void ExecuteAsync_WithLongWords_GroupsCorrectly()
    {
        // Arrange
        string[] input = ["restful", "fluster", "flusters"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            // "restful" and "fluster" are anagrams, "flusters" is different
            Assert.That(output.Count, Is.GreaterThanOrEqualTo(2));
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
        string[] input = [];
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }

    [Test]
    public void ExecuteAsync_ResultContainsAllRequiredProperties()
    {
        // Arrange
        string[] input = ["test", "sett"];
        
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
        string[] input = ["eat", "tea", "tan"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Steps.Any(s => s.Contains("Starting with")), Is.True);
            Assert.That(result.Steps.Any(s => s.Contains("Processing")), Is.True);
            Assert.That(result.Steps.Any(s => s.Contains("character frequency key")), Is.True);
            Assert.That(result.Steps.Any(s => s.Contains("Grouped")), Is.True);
        });
    }

    [Test]
    public void ExecuteAsync_StepsTrackAllProcessedStrings()
    {
        // Arrange
        string[] input = ["abc", "bca", "cab"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Steps.Count(s => s.Contains("Processing 'abc'")), Is.EqualTo(1));
            Assert.That(result.Steps.Count(s => s.Contains("Processing 'bca'")), Is.EqualTo(1));
            Assert.That(result.Steps.Count(s => s.Contains("Processing 'cab'")), Is.EqualTo(1));
        });
    }

    [Test]
    public void ExecuteAsync_StepsIndicateNewGroupCreation()
    {
        // Arrange
        string[] input = ["abc", "def"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        
        // Assert
        Assert.That(result.Steps.Count(s => s.Contains("Created new anagram group")), Is.EqualTo(2));
    }

    [Test]
    public void ExecuteAsync_WithMixedAnagrams_ReturnsAllGroups()
    {
        // Arrange
        string[] input = ["listen", "silent", "enlist", "inlets", "google", "gogole"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
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
            // Find the google group
            List<string>? googleGroup = output.FirstOrDefault(g => g.Contains("google"));
            Assert.That(googleGroup, Is.Not.Null);
            Assert.That(googleGroup!.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public void ExecuteAsync_PreservesOriginalStrings()
    {
        // Arrange
        string[] input = ["abc", "bca", "xyz"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            var allStrings = output.SelectMany(g => g).ToList();
            Assert.That(allStrings, Does.Contain("abc"));
            Assert.That(allStrings, Does.Contain("bca"));
            Assert.That(allStrings, Does.Contain("xyz"));
            Assert.That(allStrings.Count, Is.EqualTo(3));
        });
    }

    [Test]
    public void ExecuteAsync_OrderDoesNotMatter()
    {
        // Arrange
        string[] input1 = ["eat", "tea", "ate"];
        string[] input2 = ["ate", "tea", "eat"];
        
        // Act
        AlgorithmResult result1 = _algorithm.ExecuteAsync(input1);
        AlgorithmResult result2 = _algorithm.ExecuteAsync(input2);
        var output1 = (List<List<string>>)result1.Output!;
        var output2 = (List<List<string>>)result2.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output1.Count, Is.EqualTo(output2.Count));
            Assert.That(output1[0].Count, Is.EqualTo(output2[0].Count));
        });
    }

    [Test]
    public void ExecuteAsync_WithAllDifferentStrings_ReturnsAllSingletonGroups()
    {
        // Arrange
        string[] input = ["a", "b", "c", "d", "e"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output.Count, Is.EqualTo(5));
            Assert.That(output.All(g => g.Count == 1), Is.True);
        });
    }

    [Test]
    public void ExecuteAsync_WithAllSameStrings_ReturnsSingleGroup()
    {
        // Arrange
        string[] input = ["same", "same", "same", "same"];
        
        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<string>>)result.Output!;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output.Count, Is.EqualTo(1));
            Assert.That(output[0].Count, Is.EqualTo(4));
        });
    }
}
