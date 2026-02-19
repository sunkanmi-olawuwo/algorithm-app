using System.Reflection;
using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class ProductOfArrayExceptSelfTests
{
    private ProductOfArrayExceptSelf _algorithm = null!;

    [SetUp]
    public void Setup() => _algorithm = new ProductOfArrayExceptSelf();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Name, Is.EqualTo("Product of Array Except Self"));

    [Test]
    public void Category_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Category, Is.EqualTo("Array"));

    [Test]
    public void ValidateInput_WithValidArray_ReturnsTrue()
    {
        int[] input = [1, 2, 3, 4];
        bool result = _algorithm.ValidateInput(input);
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateInput_WithEmptyArray_ReturnsFalse()
    {
        int[] input = System.Array.Empty<int>();
        bool result = _algorithm.ValidateInput(input);
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateInput_WithNonArrayInput_ReturnsFalse()
    {
        string input = "not an array";
        bool result = _algorithm.ValidateInput(input);
        Assert.That(result, Is.False);
    }

    [Test]
    public void GenerateSampleInput_ReturnsArrayOfSpecifiedSize()
    {
        int size = 8;
        object sample = _algorithm.GenerateSampleInput(size);
        Assert.Multiple(() =>
        {
            Assert.That(sample, Is.TypeOf<int[]>());
            Assert.That(((int[])sample).Length, Is.EqualTo(size));
        });
    }

    [Test]
    public void ExecuteAsync_BasicExample_ReturnsExpectedProductArray()
    {
        int[] input = [1, 2, 3, 4];
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] productArray = GetProductArray(result);
        Assert.That(productArray, Is.EqualTo(new[] { 24, 12, 8, 6 }));
    }

    [Test]
    public void ExecuteAsync_WithZero_ReturnsExpectedProductArray()
    {
        int[] input = [1, 0, 3, 4];
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] productArray = GetProductArray(result);
        // Total product excluding the zero index is 1*3*4 = 12, others become 0 when a zero exists
        Assert.That(productArray, Is.EqualTo(new[] { 0, 12, 0, 0 }));
    }

    [Test]
    public void ExecuteAsync_WithTwoZeros_AllZeros()
    {
        int[] input = [0, 1, 0, 2];
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] productArray = GetProductArray(result);
        Assert.That(productArray, Is.EqualTo(new[] { 0, 0, 0, 0 }));
    }

    [Test]
    public void ExecuteAsync_WithNegativeNumbers_HandlesCorrectly()
    {
        int[] input = [-1, 2, -3, 4];
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] productArray = GetProductArray(result);
        // Products except self: [2*-3*4, -1*-3*4, -1*2*4, -1*2*-3] => [-24, 12, -8, 6]
        Assert.That(productArray, Is.EqualTo(new[] { -24, 12, -8, 6 }));
    }

    [Test]
    public void ExecuteAsync_WithSingleElementArray_ReturnsOne()
    {
        int[] input = [5];
        AlgorithmResult result = _algorithm.ExecuteAsync(input);
        int[] productArray = GetProductArray(result);
        // By convention, product except self for single element can be 1
        Assert.That(productArray, Is.EqualTo(new[] { 1 }));
    }

    [Test]
    public void ExecuteAsync_WithInvalidInput_ThrowsArgumentException()
    {
        string input = "invalid";
        Assert.Throws<ArgumentException>(() => _algorithm.ExecuteAsync(input));
    }

    private int[] GetProductArray(AlgorithmResult result)
    {
        PropertyInfo[]? outputProps = result.Output?.GetType().GetProperties();
        PropertyInfo? arrayProp = outputProps?.FirstOrDefault(p => p.Name == "ProductArray");
        return (int[])(arrayProp?.GetValue(result.Output) ?? System.Array.Empty<int>());
    }
}
