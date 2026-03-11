using System.Reflection;
using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class ReverseArrayTests
{
    private ReverseArray _algorithm;

    [SetUp]
    public void Setup() => _algorithm = new ReverseArray();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Name, Is.EqualTo("Reverse Array"));

    [Test]
    public void Category_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Category, Is.EqualTo("Array"));

    [Test]
    public void ValidateInput_WithValidArray_ReturnsTrue()
    {
        int[] input = [1, 2, 3, 4, 5];
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
    public void ExecuteAsync_ReversesArray()
    {
        int[] input = [1, 2, 3, 4, 5];

        AlgorithmResult result = _algorithm.ExecuteAsync(input);

        int[] reversed = GetOutputArray(result, "ReversedArray");

        Assert.That(reversed, Is.EqualTo(new[] { 5, 4, 3, 2, 1 }));
        Assert.That(result.Steps, Is.Not.Null.And.Not.Empty);
    }

    private int[] GetOutputArray(AlgorithmResult result, string propertyName)
    {
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? arrProp = outputProps?.FirstOrDefault(p => p.Name == propertyName);
        return (int[])(arrProp?.GetValue(result.Output) ?? null)!;
    }
}
