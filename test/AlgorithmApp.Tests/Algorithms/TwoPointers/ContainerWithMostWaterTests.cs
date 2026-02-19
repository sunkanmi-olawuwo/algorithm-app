using AlgorithmApp.Algorithms.TwoPointers;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.TwoPointers;

[TestFixture]
public class ContainerWithMostWaterTests
{
    private ContainerWithMostWater _algorithm = null!;

    [SetUp]
    public void Setup() => _algorithm = new ContainerWithMostWater();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Name, Is.EqualTo("Container With Most Water"));

    [Test]
    public void Description_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Description, Does.Contain("most water"));

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
    public void ValidateInput_WithValidArray_ReturnsTrue()
    {
        int[] input = [1];

        bool result = _algorithm.ValidateInput(input);

        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateInput_WithEmptyArray_ReturnsFalse()
    {
        int[] input = global::System.Array.Empty<int>();

        bool result = _algorithm.ValidateInput(input);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateInput_WithNonArrayInput_ReturnsFalse()
    {
        object input = "not an array";

        bool result = _algorithm.ValidateInput(input);

        Assert.That(result, Is.False);
    }

    [Test]
    public void GenerateSampleInput_ReturnsIntArray()
    {
        object result = _algorithm.GenerateSampleInput(5);

        Assert.That(result, Is.TypeOf<int[]>());
    }

    [Test]
    public void ExecuteAsync_WithKnownInput_ReturnsExpectedArea()
    {
        int[] input = [1, 8, 6, 2, 5, 4, 8, 3, 7];

        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int output = (int)result.Output!;

        Assert.Multiple(() =>
        {
            Assert.That(result.AlgorithmName, Is.EqualTo(_algorithm.Name));
            Assert.That(output, Is.EqualTo(49));
        });
    }

    [Test]
    public void ExecuteAsync_WithSmallInput_ReturnsExpectedArea()
    {
        int[] input = [1, 1];

        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int output = (int)result.Output!;

        Assert.That(output, Is.EqualTo(1));
    }

    [Test]
    public void ExecuteAsync_WithInvalidInput_ThrowsArgumentException()
    {
        object input = new();

        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }
}
