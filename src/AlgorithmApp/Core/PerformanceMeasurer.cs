using System.Diagnostics;
using static AlgorithmApp.Core.AppModels;

namespace AlgorithmApp.Core;

public interface IPerformanceMeasurer
{
    PerformanceMetrics Measure(Action action);
}

public class PerformanceMeasurer : IPerformanceMeasurer
{
    public PerformanceMetrics Measure(Action action)
    {
        // Record the starting memory
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
            
        long startMemory = GC.GetTotalMemory(true);
            
        // Start the timer
        var stopwatch = Stopwatch.StartNew();
            
        // Execute the algorithm
        action();
            
        // Stop the timer
        stopwatch.Stop();
            
        // Get memory used
        long endMemory = GC.GetTotalMemory(false);
        long memoryUsed = endMemory - startMemory;
            
        return new PerformanceMetrics(
            ExecutionTime: stopwatch.Elapsed, 
            MemoryUsed: memoryUsed
        );
    }
}
