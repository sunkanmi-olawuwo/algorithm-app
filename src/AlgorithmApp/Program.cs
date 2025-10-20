using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;
using AlgorithmApp.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static AlgorithmApp.Core.IService;


namespace AlgorithmApp;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var app = host.Services.GetRequiredService<Application>();
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
                services.AddSingleton<IAlgorithm, ArrayAlgorithmScratchPad>();

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
