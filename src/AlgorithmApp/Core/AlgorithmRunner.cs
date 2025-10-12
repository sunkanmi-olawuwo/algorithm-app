using AlgorithmApp.UI;
using Microsoft.Extensions.Logging;
using static AlgorithmApp.Core.AppModels;
using static AlgorithmApp.Core.IService;

namespace AlgorithmApp.Core;

public class AlgorithmRunner(
    IService.IAlgorithmFactory algorithmFactory,
    IMenuService menuService,
    ILogger<AlgorithmRunner> logger,
    IPerformanceMeasurer performanceMeasurer) : IAlgorithmRunner
{
    public void RunSelectedAlgorithm()
    {
        var algorithmName = menuService.SelectAlgorithm();
        var algorithm = algorithmFactory.GetAlgorithm(algorithmName);

        if (algorithm == null)
        {
            Console.WriteLine("Algorithm not found.");
            return;
        }

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
            var input = algorithm.GenerateSampleInput(size);
            if (!algorithm.ValidateInput(input))
            {
                Console.WriteLine("Generated input is invalid for the selected algorithm.");
                return;
            }
            
            Console.WriteLine("Executing algorithm...");
            
            AlgorithmResult result = null;
            
            var metrics = performanceMeasurer.Measure(() => {
                result = algorithm.ExecuteAsync(input);
            });
            
            // Attach performance metrics to result
            var resultWithMetrics = result with { 
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
