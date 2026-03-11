using System.Reflection;
using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class MoveZeroesToEndTests
{
    private MoveZeroesToEnd _algorithm;

    [SetUp]
    public void Setup() => _algorithm = new MoveZeroesToEnd();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Name, Is.EqualTo("Move Zeroes to End"));

    [Test]
    public void Category_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Category, Is.EqualTo("Array"));

    [Test]
    public void ValidateInput_WithValidArray_ReturnsTrue()
    {
        int[] input = [1, 0, 2];
        bool result = _algorithm.ValidateInput(input);
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateInput_WithEmptyArray_ReturnsFalse()
    {
        int[] input = [];
        bool result = _algorithm.ValidateInput(input);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ExecuteAsync_MovesZeroesToEnd()
    {
        int[] input = [0, 1, 0, 3, 12];

        AlgorithmResult result = _algorithm.ExecuteAsync(input);

        int[]? updated = GetOutputArray(result, "UpdatedArray");

        Assert.That(updated, Is.EqualTo(new[] { 1, 3, 12, 0, 0 }));
        Assert.That(result.Steps, Is.Not.Null.And.Not.Empty);
    }

    private int[] GetOutputArray(AlgorithmResult result, string propertyName)
    {
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? arrProp = outputProps?.FirstOrDefault(p => p.Name == propertyName);
        return (int[])(arrProp?.GetValue(result.Output) ?? null)!;
    }
}
