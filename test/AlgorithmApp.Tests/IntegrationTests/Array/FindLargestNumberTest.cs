using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;
using static AlgorithmApp.Core.IService;

namespace AlgorithmApp.Tests.IntegrationTests.Array;

public class FindLargestNumberTest
{
    private AlgorithmFactory _algorithmFactory;
    private List<IAlgorithm> _algorithms;

    [SetUp]
    public void Setup()
    {
        _algorithms = new List<IAlgorithm>
        {
            new FindLargestNumber()
        };

        _algorithmFactory = new AlgorithmFactory(_algorithms);
    }

    [Test]
    public void FindLargestNumber_ExecutionWithRealData()
    {
        // Arrange
        var algorithm = _algorithmFactory.GetAlgorithm("Find Largest Number");
        int[] input = { 1, 42, 8, 15, 33, 7, 10 };

        // Act
        var result = algorithm.ExecuteAsync(input);

        // Assert
        var outputProps = result.Output?.GetType().GetProperties();
        var valueProp = outputProps?.FirstOrDefault(p => p.Name == "Value");
        var indexProp = outputProps?.FirstOrDefault(p => p.Name == "Index");

        Assert.That(valueProp != null, Is.True, "Output does not contain a Value property");
        Assert.That(indexProp != null, Is.True, "Output does not contain an Index property");

        int value = valueProp != null ? Convert.ToInt32(valueProp.GetValue(result.Output)) : 0;
        int index = indexProp != null ? Convert.ToInt32(indexProp.GetValue(result.Output)) : -1;

        Assert.That(value, Is.EqualTo(42));
        Assert.That(index, Is.EqualTo(1));
    }
}
