using System.Diagnostics;

namespace AlgorithmApp.Core;

internal interface IPerformanceMeasurer
{
    PerformanceMetrics Measure(Action action);
}

internal class PerformanceMeasurer : IPerformanceMeasurer
{
    public PerformanceMetrics Measure(Action action)
    {
        // Record the starting memory
        // Note: GC operations are necessary for accurate memory measurement in performance testing
        #pragma warning disable S1215 // "GC.Collect" should not be called
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
            
        long startMemory = GC.GetTotalMemory(true);
        #pragma warning restore S1215
            
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
