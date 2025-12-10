using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class EncodeAndDecodeStringTests
{
    private EncodeAndDecodeString _algorithm = null!;

    [SetUp]
    public void Setup() => _algorithm = new EncodeAndDecodeString();

    [Test]
    public void Name_ReturnsCorrectValue() =>
   // Assert
        Assert.That(_algorithm.Name, Is.EqualTo("Encode and Decode String"));

    [Test]
    public void Description_ReturnsCorrectValue()
    {
        // Assert
        Assert.That(_algorithm.Description, Does.Contain("Encodes and decodes strings"));
Assert.That(_algorithm.Description, Does.Contain("algorithm"));
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
        Assert.That(_algorithm.SpaceComplexity, Is.EqualTo("O(m + n)"));

    [Test]
    public void Hint_ContainsEncodingReference() =>
        // Assert
        Assert.Multiple(() =>
        {
      Assert.That(_algorithm.Hint, Does.Contain("encoding"));
        Assert.That(_algorithm.Hint, Does.Contain("length"));
            Assert.That(_algorithm.Hint, Does.Contain("#"));
 });

    [Test]
    public void ValidateInput_WithValidStringArray_ReturnsTrue()
    {
        // Arrange
        string[] input = ["hello", "world"];

        // Act
        bool result = _algorithm.ValidateInput(input);

        // Assert
   Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateInput_WithSingleString_ReturnsTrue()
    {
 // Arrange
  string[] input = ["test"];

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
    public void ExecuteAsync_WithSimpleStrings_EncodesAndDecodesCorrectly()
    {
        // Arrange
        string[] input = ["hello", "world"];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
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
            Assert.That(result.AlgorithmName, Is.EqualTo(_algorithm.Name));
        });
    }

    [Test]
    public void ExecuteAsync_WithSingleString_EncodesAndDecodesCorrectly()
    {
        // Arrange
        string[] input = ["test"];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

        // Assert
        Assert.Multiple(() =>
  {
            Assert.That(output.Decoded.Length, Is.EqualTo(1));
        Assert.That(output.Decoded[0], Is.EqualTo("test"));
        });
    }

    [Test]
    public void ExecuteAsync_WithEmptyStrings_EncodesAndDecodesCorrectly()
    {
        // Arrange
     string[] input = ["", "test", ""];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
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
    public void ExecuteAsync_WithStringsContainingHashSymbol_EncodesAndDecodesCorrectly()
    {
 // Arrange
        string[] input = ["hello#world", "test#123", "no#hash#here"];

    // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
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
    public void ExecuteAsync_WithStringsContainingNumbers_EncodesAndDecodesCorrectly()
    {
        // Arrange
        string[] input = ["123", "456789", "0"];

        // Act
     AlgorithmResult result = _algorithm.ExecuteAsync(input);
dynamic output = result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output.Decoded.Length, Is.EqualTo(3));
         Assert.That(output.Decoded[0], Is.EqualTo("123"));
            Assert.That(output.Decoded[1], Is.EqualTo("456789"));
      Assert.That(output.Decoded[2], Is.EqualTo("0"));
        });
    }

    [Test]
    public void ExecuteAsync_WithSpecialCharacters_EncodesAndDecodesCorrectly()
 {
        // Arrange
        string[] input = ["hello@world", "test!123", "special$chars%"];

      // Act
   AlgorithmResult result = _algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

        // Assert
        Assert.Multiple(() =>
    {
            Assert.That(output.Decoded.Length, Is.EqualTo(3));
  Assert.That(output.Decoded[0], Is.EqualTo("hello@world"));
            Assert.That(output.Decoded[1], Is.EqualTo("test!123"));
          Assert.That(output.Decoded[2], Is.EqualTo("special$chars%"));
      });
    }

    [Test]
 public void ExecuteAsync_WithLongStrings_EncodesAndDecodesCorrectly()
    {
        // Arrange
    string longString = new string('a', 100);
     string[] input = [longString, "short", longString];

    // Act
     AlgorithmResult result = _algorithm.ExecuteAsync(input);
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
    public void ExecuteAsync_WithVaryingLengthStrings_EncodesAndDecodesCorrectly()
    {
        // Arrange
        string[] input = ["a", "ab", "abc", "abcd", "abcde"];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
    dynamic output = result.Output!;

        // Assert
     Assert.Multiple(() =>
      {
      Assert.That(output.Decoded.Length, Is.EqualTo(5));
            Assert.That(output.Decoded[0], Is.EqualTo("a"));
          Assert.That(output.Decoded[1], Is.EqualTo("ab"));
 Assert.That(output.Decoded[2], Is.EqualTo("abc"));
            Assert.That(output.Decoded[3], Is.EqualTo("abcd"));
         Assert.That(output.Decoded[4], Is.EqualTo("abcde"));
        });
    }

    [Test]
    public void ExecuteAsync_WithIdenticalStrings_EncodesAndDecodesCorrectly()
    {
     // Arrange
        string[] input = ["same", "same", "same"];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
     dynamic output = result.Output!;

    // Assert
  Assert.Multiple(() =>
        {
      Assert.That(output.Decoded.Length, Is.EqualTo(3));
            Assert.That(output.Decoded[0], Is.EqualTo("same"));
            Assert.That(output.Decoded[1], Is.EqualTo("same"));
            Assert.That(output.Decoded[2], Is.EqualTo("same"));
      });
    }

    [Test]
    public void ExecuteAsync_WithSampleInput_EncodesAndDecodesCorrectly()
    {
        // Arrange
        string[] input = ["listen", "silent", "enlist", "inlets", "google", "gogole"];

      // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
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
  public void ExecuteAsync_WithWhitespaceStrings_EncodesAndDecodesCorrectly()
    {
    // Arrange
     string[] input = [" ", "  ", "hello world", " test "];

      // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

      // Assert
        Assert.Multiple(() =>
  {
            Assert.That(output.Decoded.Length, Is.EqualTo(4));
  Assert.That(output.Decoded[0], Is.EqualTo(" "));
    Assert.That(output.Decoded[1], Is.EqualTo("  "));
   Assert.That(output.Decoded[2], Is.EqualTo("hello world"));
  Assert.That(output.Decoded[3], Is.EqualTo(" test "));
        });
    }

    [Test]
    public void ExecuteAsync_WithUnicodeCharacters_EncodesAndDecodesCorrectly()
    {
        // Arrange
        string[] input = ["hello", "世界", "🌍🌎🌏", "тест"];

    // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
  dynamic output = result.Output!;

        // Assert
        Assert.Multiple(() =>
     {
            Assert.That(output.Decoded.Length, Is.EqualTo(4));
          Assert.That(output.Decoded[0], Is.EqualTo("hello"));
  Assert.That(output.Decoded[1], Is.EqualTo("世界"));
            Assert.That(output.Decoded[2], Is.EqualTo("🌍🌎🌏"));
            Assert.That(output.Decoded[3], Is.EqualTo("тест"));
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
        string[] input = ["test", "data"];

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
        string[] input = ["hello", "world"];

      // Act
      AlgorithmResult result = _algorithm.ExecuteAsync(input);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Steps.Any(s => s.Contains("Starting with")), Is.True);
            Assert.That(result.Steps.Any(s => s.Contains("Encoded string:")), Is.True);
 Assert.That(result.Steps.Any(s => s.Contains("Decoding:")), Is.True);
            Assert.That(result.Steps.Any(s => s.Contains("Encoded string length:")), Is.True);
        });
    }

    [Test]
    public void ExecuteAsync_StepsTrackDecodingProcess()
    {
   // Arrange
        string[] input = ["test", "data"];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);

        // Assert
        Assert.Multiple(() =>
  {
        Assert.That(result.Steps.Any(s => s.Contains("looking for '#'")), Is.True);
            Assert.That(result.Steps.Any(s => s.Contains("Fetching the length")), Is.True);
   });
    }

    [Test]
    public void ExecuteAsync_EncodedStringFormatIsCorrect()
    {
  // Arrange
   string[] input = ["abc", "de"];

      // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
     dynamic output = result.Output!;
        string encoded = output.Encoded;

// Assert
        Assert.Multiple(() =>
   {
        // Format should be: 3#abc2#de
            Assert.That(encoded, Does.StartWith("3#abc"));
       Assert.That(encoded, Does.Contain("2#de"));
  Assert.That(encoded.Length, Is.EqualTo(9)); // "3#abc2#de" = 10 chars
 });
    }

    [Test]
    public void ExecuteAsync_PreservesStringOrder()
    {
        // Arrange
        string[] input = ["first", "second", "third"];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

    // Assert
        Assert.Multiple(() =>
        {
    Assert.That(output.Decoded[0], Is.EqualTo("first"));
 Assert.That(output.Decoded[1], Is.EqualTo("second"));
            Assert.That(output.Decoded[2], Is.EqualTo("third"));
        });
    }

    [Test]
    public void ExecuteAsync_WithManyStrings_EncodesAndDecodesCorrectly()
    {
        // Arrange
    string[] input = Enumerable.Range(1, 20).Select(i => $"string{i}").ToArray();

  // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output.Decoded.Length, Is.EqualTo(20));
for (int i = 0; i < 20; i++)
  {
         Assert.That(output.Decoded[i], Is.EqualTo($"string{i + 1}"));
       }
});
    }

    [Test]
    public void ExecuteAsync_WithDoubleDigitLengths_EncodesAndDecodesCorrectly()
    {
        // Arrange
        string tenChars = "abcdefghij"; // 10 characters
      string hundredChars = new string('x', 100);
        string[] input = [tenChars, hundredChars];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
      dynamic output = result.Output!;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(output.Decoded.Length, Is.EqualTo(2));
  Assert.That(output.Decoded[0], Is.EqualTo(tenChars));
   Assert.That(output.Decoded[1], Is.EqualTo(hundredChars));
        });
    }

    [Test]
    public void ExecuteAsync_DecodedArrayMatchesOriginalInput()
    {
        // Arrange
        string[] input = ["random", "test", "strings", "for", "verification"];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;
        string[] decoded = output.Decoded;

        // Assert
        Assert.That(decoded, Is.EqualTo(input));
    }

[Test]
    public void ExecuteAsync_WithCombinationOfEdgeCases_EncodesAndDecodesCorrectly()
    {
 // Arrange
      string[] input = ["", "a", "##", "test#with#hashes", new string('b', 50)];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

   // Assert
        Assert.Multiple(() =>
      {
            Assert.That(output.Decoded.Length, Is.EqualTo(5));
  Assert.That(output.Decoded[0], Is.EqualTo(""));
            Assert.That(output.Decoded[1], Is.EqualTo("a"));
        Assert.That(output.Decoded[2], Is.EqualTo("##"));
Assert.That(output.Decoded[3], Is.EqualTo("test#with#hashes"));
 Assert.That(output.Decoded[4], Is.EqualTo(new string('b', 50)));
        });
    }

    [Test]
    public void ExecuteAsync_InputAndOutputCountMatch()
    {
      // Arrange
    string[] input = ["one", "two", "three", "four", "five"];

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        dynamic output = result.Output!;

        // Assert
  Assert.That(output.Decoded.Length, Is.EqualTo(input.Length));
    }
}
