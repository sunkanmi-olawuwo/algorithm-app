using AlgorithmApp.UI;
using Microsoft.Extensions.Logging;

namespace AlgorithmApp.Core;

internal class AlgorithmRunner(
    IAlgorithmFactory algorithmFactory,
    IMenuService menuService,
    ILogger<AlgorithmRunner> logger,
    IPerformanceMeasurer performanceMeasurer) : IAlgorithmRunner
{
    public void RunSelectedAlgorithm()
    {
        string algorithmName = menuService.SelectAlgorithm();
        IAlgorithm algorithm = algorithmFactory.GetAlgorithm(algorithmName);

        Console.WriteLine($"\n{algorithm.Name}");
        Console.WriteLine($"Description: {algorithm.Description}");
        Console.WriteLine($"Time Complexity: {algorithm.TimeComplexity}");
        Console.WriteLine($"Space Complexity: {algorithm.SpaceComplexity}");

        try
        {
            Console.Write("Enter input size (e.g., 10): ");
            if (!int.TryParse(Console.ReadLine(), out int size) || size <= 0)
            {
                Console.WriteLine("Invalid size. Using default size of 10.");
                size = 10;
            }
            object input = algorithm.GenerateSampleInput(size);
            if (!algorithm.ValidateInput(input))
            {
                Console.WriteLine("Generated input is invalid for the selected algorithm.");
                return;
            }
            
            Console.WriteLine("Executing algorithm...");
            
            AlgorithmResult? result = null;
            
            PerformanceMetrics metrics = performanceMeasurer.Measure(() => result = algorithm.ExecuteAsync(input));
            
            // Attach performance metrics to result
            if (result == null)
            {
                return;
            }

            AlgorithmResult resultWithMetrics = result with { 
                PerformanceMetrics = metrics
            };
            
            menuService.DisplayResults(resultWithMetrics);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing algorithm {AlgorithmName}", algorithmName);
            Console.WriteLine($"An error occurred while executing the algorithm: {ex.Message}");
        }
    }
}
