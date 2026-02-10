using System.Reflection;
using AlgorithmApp.Algorithms.TwoPointers;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.TwoPointers;

[TestFixture]
public class ValidPalindromeTests
{
    private ValidPalindrome _algorithm = null!;

    [SetUp]
    public void Setup() => _algorithm = new ValidPalindrome();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Name, Is.EqualTo("Valid Palindrome"));

    [Test]
    public void Description_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Description, Does.Contain("palindrome"));

    [Test]
    public void TimeComplexity_ReturnsCorrectValue() =>
        Assert.That(_algorithm.TimeComplexity, Is.EqualTo("O(n)"));

    [Test]
    public void SpaceComplexity_ReturnsCorrectValue() =>
        Assert.That(_algorithm.SpaceComplexity, Is.EqualTo("O(1)"));

    [Test]
    public void Hint_MentionsTwoPointers() =>
        Assert.That(_algorithm.Hint, Does.Contain("two pointers"));

    [Test]
    public void ValidateInput_WithValidString_ReturnsTrue()
    {
        // Arrange
        string input = "racecar";

        // Act
        bool result = _algorithm.ValidateInput(input);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateInput_WithEmptyString_ReturnsFalse()
    {
        // Arrange
        string input = string.Empty;

        // Act
        bool result = _algorithm.ValidateInput(input);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateInput_WithNonString_ReturnsFalse()
    {
        // Arrange
        object input = 12345;

        // Act
        bool result = _algorithm.ValidateInput(input);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GenerateSampleInput_ReturnsPalindromeString()
    {
        // Act
        object result = _algorithm.GenerateSampleInput(10);

        // Assert
        Assert.That(result, Is.TypeOf<string>());
        Assert.That((string)result, Is.Not.Empty);
    }

    [Test]
    public void ExecuteAsync_WithSimplePalindrome_ReturnsTrue()
    {
        // Arrange
        string input = "racecar";

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        bool isPalindrome = GetIsPalindrome(result);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(isPalindrome, Is.True);
            Assert.That(result.AlgorithmName, Is.EqualTo(_algorithm.Name));
            Assert.That(result.Steps, Is.Not.Empty);
        });
    }

    [Test]
    public void ExecuteAsync_WithNonPalindrome_ReturnsFalse()
    {
        // Arrange
        string input = "hello";

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        bool isPalindrome = GetIsPalindrome(result);

        // Assert
        Assert.That(isPalindrome, Is.False);
    }

    [Test]
    public void ExecuteAsync_IgnoresNonAlphanumericCharactersAndCase()
    {
        // Arrange
        string input = "A man, a plan, a canal: Panama";

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        bool isPalindrome = GetIsPalindrome(result);

        // Assert
        Assert.That(isPalindrome, Is.True);
    }

    [Test]
    public void ExecuteAsync_WithSingleCharacter_ReturnsTrue()
    {
        // Arrange
        string input = "a";

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        bool isPalindrome = GetIsPalindrome(result);

        // Assert
        Assert.That(isPalindrome, Is.True);
    }

    [Test]
    public void ExecuteAsync_WithTwoDifferentCharacters_ReturnsFalse()
    {
        // Arrange
        string input = "ab";

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        bool isPalindrome = GetIsPalindrome(result);

        // Assert
        Assert.That(isPalindrome, Is.False);
    }

    [Test]
    public void ExecuteAsync_WithOnlyNonAlphanumericCharacters_TreatedAsPalindrome()
    {
        // Arrange
        string input = ".,!!";

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        bool isPalindrome = GetIsPalindrome(result);

        // Assert
        Assert.That(isPalindrome, Is.True);
    }

    [Test]
    public void ExecuteAsync_WithNullInput_ThrowsArgumentException()
    {
        // Arrange
        object? input = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input!));
    }

    private static bool GetIsPalindrome(AlgorithmResult result)
    {
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? valueProp = outputProps?.FirstOrDefault(p => p.Name == "IsPalindrome");
        return (bool)(valueProp?.GetValue(result.Output) ?? false);
    }
}
