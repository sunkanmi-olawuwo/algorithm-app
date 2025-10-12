using System.Text;
using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;
using static AlgorithmApp.Core.IService;

namespace AlgorithmApp.Tests.IntegrationTests;

[TestFixture]
public class IntegrationTests
{
    private AlgorithmFactory _algorithmFactory;
    private PerformanceMeasurer _performanceMeasurer;
    private AlgorithmComparer _algorithmComparer;
    private List<IAlgorithm> _algorithms;
    
    [SetUp]
    public void Setup()
    {
        _algorithms = new List<IAlgorithm>
        {
            new FindLargestNumber()
            // Add other algorithms here as they become available
        };
        
        _algorithmFactory = new AlgorithmFactory(_algorithms);
        _performanceMeasurer = new PerformanceMeasurer();
        _algorithmComparer = new AlgorithmComparer(_algorithmFactory, _performanceMeasurer);
    }
    
    
    [Test]
    public void AlgorithmComparer_ComparesAlgorithms()
    {
        // Arrange
        var algorithmNames = _algorithmFactory.GetAllAlgorithms().Select(a => a.Name).ToList();
        
        // Act
        var result = _algorithmComparer.CompareAlgorithms(algorithmNames, 100);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Results, Has.Count.EqualTo(algorithmNames.Count));
            Assert.That(result.FastestAlgorithm, Is.Not.Empty);
            Assert.That(result.MostMemoryEfficient, Is.Not.Empty);
            
            foreach (var algorithm in algorithmNames)
            {
                Assert.That(result.Results.ContainsKey(algorithm), Is.True);
                Assert.That(result.Results[algorithm].ExecutionTime.TotalMilliseconds, Is.GreaterThan(0));
            }
        });
    }
    
    [Test]
    public void Performance_MeasuringExecutionTime()
    {
        // Arrange
        StringBuilder sb = new();
        Action action = () => {
            for (int i = 0; i < 1000; i++)
            {
                sb.Append(i);
            }
        };
        
        // Act
        var metrics = _performanceMeasurer.Measure(action);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(metrics.ExecutionTime.TotalMilliseconds, Is.GreaterThan(0));
            Assert.That(metrics.MemoryUsed, Is.GreaterThan(0));
        });
    }
}