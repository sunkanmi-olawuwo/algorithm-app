using System.Diagnostics;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Core;

[TestFixture]
public class PerformanceMeasurerTests
{
    private PerformanceMeasurer _measurer;
    
    [SetUp]
    public void Setup()
    {
        _measurer = new PerformanceMeasurer();
    }
    
    [Test]
    public void Measure_ExecutesTheAction()
    {
        // Arrange
        bool wasExecuted = false;
        Action action = () => { wasExecuted = true; };
        
        // Act
        _measurer.Measure(action);
        
        // Assert
        Assert.That(wasExecuted, Is.True);
    }
    
    [Test]
    public void Measure_RecordsExecutionTime()
    {
        // Arrange
        Action action = () => Thread.Sleep(10); // Small delay
        
        // Act
        var result = _measurer.Measure(action);
        
        // Assert
        Assert.That(result.ExecutionTime.TotalMilliseconds, Is.GreaterThan(0));
    }
    
    [Test]
    public void Measure_SlowerAction_TakesMoreTime()
    {
        // Arrange
        Action fastAction = () => Thread.Sleep(10);
        Action slowAction = () => Thread.Sleep(50);
        
        // Act
        var fastResult = _measurer.Measure(fastAction);
        var slowResult = _measurer.Measure(slowAction);
        
        // Assert - with some tolerance for system variability
        Assert.That(slowResult.ExecutionTime, Is.GreaterThan(fastResult.ExecutionTime));
    }
    
    [Test]
    public void Measure_RecordsMemoryUsed()
    {
        // Arrange
        Action memoryIntensiveAction = () => {
            // Allocate a reasonably sized array to ensure some memory usage
            var array = new byte[1024 * 1024]; // 1MB
            // Prevent optimization by doing something with the array
            array[0] = 1;
        };
        
        // Act
        var result = _measurer.Measure(memoryIntensiveAction);
        
        // Assert
        // Note: Memory measurement can be imprecise due to garbage collection,
        // so we just check that it recorded some value
        Assert.That(result.MemoryUsed, Is.Not.EqualTo(0));
    }
    
    [Test]
    public void Measure_HandlesExceptions()
    {
        // Arrange
        Action actionWithException = () => throw new InvalidOperationException("Test exception");
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _measurer.Measure(actionWithException));
    }
}