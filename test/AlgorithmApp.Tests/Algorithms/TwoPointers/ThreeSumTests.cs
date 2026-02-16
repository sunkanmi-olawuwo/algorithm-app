using AlgorithmApp.Algorithms.TwoPointers;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.TwoPointers;

[TestFixture]
public class ThreeSumTests
{
    private ThreeSum _algorithm = null!;

    [SetUp]
    public void Setup() => _algorithm = new ThreeSum();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Name, Is.EqualTo("Three Sum"));

    [Test]
    public void Description_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Description, Does.Contain("triplets"));

    [Test]
    public void TimeComplexity_ReturnsCorrectValue() =>
        Assert.That(_algorithm.TimeComplexity, Is.EqualTo("O(n^2)"));

    [Test]
    public void SpaceComplexity_ReturnsCorrectValue() =>
        Assert.That(_algorithm.SpaceComplexity, Is.EqualTo("O(1)"));

    [Test]
    public void Hint_MentionsSortingAndTwoPointers() =>
        Assert.That(_algorithm.Hint, Does.Contain("two pointers"));

    [Test]
    public void ValidateInput_WithValidArray_ReturnsTrue()
    {
        int[] input = { -1, 0, 1 };

        bool result = _algorithm.ValidateInput(input);

        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateInput_WithSingleElementArray_ReturnsFalse()
    {
        int[] input = { 1 };

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
    public void ExecuteAsync_WithKnownInput_ReturnsExpectedTriplets()
    {
        int[] input = { -1, 0, 1, 2, -1, -4 };

        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<int>>)result.Output!;

        Assert.Multiple(() =>
        {
            Assert.That(result.AlgorithmName, Is.EqualTo(_algorithm.Name));
            Assert.That(output, Has.Count.EqualTo(2));
            Assert.That(output, Has.One.EquivalentTo(new List<int> { -1, -1, 2 }));
            Assert.That(output, Has.One.EquivalentTo(new List<int> { -1, 0, 1 }));
        });
    }

    [Test]
    public void ExecuteAsync_WithNoTriplets_ReturnsEmptyList()
    {
        int[] input = { 1, 2, 3 };

        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        var output = (List<List<int>>)result.Output!;

        Assert.That(output, Is.Empty);
    }

    [Test]
    public void ExecuteAsync_WithInvalidInput_ThrowsArgumentException()
    {
        object input = new object();

        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }
}
