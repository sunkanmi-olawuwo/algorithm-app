using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;
using AlgorithmApp.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace AlgorithmApp;

internal static class Program
{
    public static void Main(string[] args)
    {
        IHost host = CreateHostBuilder(args).Build();
        Application app = host.Services.GetRequiredService<Application>();
        app.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Core services
                services.AddSingleton<Application>();
                services.AddSingleton<IMenuService, MenuService>();
                services.AddSingleton<IAlgorithmRunner, AlgorithmRunner>();
                services.AddSingleton<IPerformanceMeasurer, PerformanceMeasurer>();
                services.AddSingleton<IAlgorithmComparer, AlgorithmComparer>();

                // Register all algorithms
                services.AddSingleton<IAlgorithm, ContainsDuplicate>();
                services.AddSingleton<IAlgorithm, ValidAnagramDictionary>();
                services.AddSingleton<IAlgorithm, ValidAnagramArray>();
                services.AddSingleton<IAlgorithm, TwoNumberSum>();
                services.AddSingleton<IAlgorithm, GroupAnagrams>();
                services.AddSingleton<IAlgorithm, ArrayAlgorithmScratchPad>();
                services.AddSingleton<IAlgorithm, TopKFrequentElements>();
                services.AddSingleton<IAlgorithm, EncodeAndDecodeString>();
                services.AddSingleton<IAlgorithm, ProductOfArrayExceptSelf>();
                services.AddSingleton<IAlgorithm, ValidSudoku>();
                services.AddSingleton<IAlgorithm, LongestConsecutiveSequence>();

                // Algorithm factory
                services.AddSingleton<IAlgorithmFactory, AlgorithmFactory>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Information);
            });
}
