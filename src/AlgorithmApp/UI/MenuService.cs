using AlgorithmApp.Core;
using static AlgorithmApp.Core.AppEnum;
using static AlgorithmApp.Core.AppModels;

namespace AlgorithmApp.UI;

public class MenuService(Core.IService.IAlgorithmFactory algorithmFactory) : IMenuService
{
    public MenuChoice ShowMainMenu()
    {
        Console.WriteLine("\n=== Algorithm Learning Application ===");
        Console.WriteLine("1. Run Algorithm");
        Console.WriteLine("2. View Documentation");
        Console.WriteLine("3. Compare Algorithms");
        Console.WriteLine("4. Exit");
        Console.Write("Select an option: ");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            return choice switch
            {
                1 => MenuChoice.RunAlgorithm,
                2 => MenuChoice.ViewDocumentation,
                3 => MenuChoice.CompareAlgorithms,
                4 => MenuChoice.Exit,
                _ => MenuChoice.Exit
            };
        }

        return MenuChoice.Exit;
    }

    public string SelectAlgorithm()
    {
        Console.WriteLine("\nAvailable Algorithms:");
        var algorithms = algorithmFactory.GetAllAlgorithms().ToList();

        for (int i = 0; i < algorithms.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {algorithms[i].Name} ({algorithms[i].Category})");
        }

        Console.Write("Select algorithm: ");
        if (int.TryParse(Console.ReadLine(), out int choice) &&
            choice > 0 && choice <= algorithms.Count)
        {
            return algorithms[choice - 1].Name;
        }

        return string.Empty;
    }

    public void ShowDocumentation()
    {
        Console.WriteLine("\n=== Algorithm Documentation ===");

        foreach (var category in algorithmFactory.GetCategories())
        {
            Console.WriteLine($"\n{category} Algorithms:");
            Console.WriteLine(new string('-', 50));

            foreach (var algorithm in algorithmFactory.GetAlgorithmsByCategory(category))
            {
                Console.WriteLine($"\n{algorithm.Name}");
                Console.WriteLine($"  Description: {algorithm.Description}");
                Console.WriteLine($"  Time Complexity: {algorithm.TimeComplexity}");
                Console.WriteLine($"  Space Complexity: {algorithm.SpaceComplexity}");
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public void DisplayResults(AlgorithmResult result)
    {
        Console.WriteLine($"\n=== {result.AlgorithmName} Results ===");

        if (result.Steps.Count is > 0 and <= 20)
        {
            Console.WriteLine("\nExecution Steps:");
            foreach (var step in result.Steps)
                Console.WriteLine($"  {step}");
        }
        else if (result.Steps.Count > 20)
        {
            Console.WriteLine($"\nTotal steps: {result.Steps.Count} (showing first and last 5)");
            foreach (var step in result.Steps.Take(5))
                Console.WriteLine($"  {step}");
            Console.WriteLine("  ...");
            foreach (var step in result.Steps.TakeLast(5))
                Console.WriteLine($"  {step}");
        }

        // Display output
        Console.WriteLine("\nOutput:");
        if (result.Output != null)
        {
            var outputProps = result.Output.GetType().GetProperties();
            foreach (var prop in outputProps)
            {
                var value = prop.GetValue(result.Output);
                Console.WriteLine($"  {prop.Name}: {value}");
            }
        }
        else
        {
            Console.WriteLine("  (No output)");
        }

        // Display performance metrics if available
        if (result.PerformanceMetrics != null)
        {
            Console.WriteLine("\nPerformance Metrics:");
            Console.WriteLine($"  Execution Time: {result.PerformanceMetrics.ExecutionTime.TotalMilliseconds:F2} ms");
            Console.WriteLine($"  Memory Used: {FormatMemorySize(result.PerformanceMetrics.MemoryUsed)}");
            
            if (result.PerformanceMetrics.Comparisons > 0)
                Console.WriteLine($"  Comparisons: {result.PerformanceMetrics.Comparisons:N0}");
            
            if (result.PerformanceMetrics.Swaps > 0)
                Console.WriteLine($"  Swaps: {result.PerformanceMetrics.Swaps:N0}");
            
            if (result.PerformanceMetrics.Iterations > 0)
                Console.WriteLine($"  Iterations: {result.PerformanceMetrics.Iterations:N0}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
    
    private string FormatMemorySize(long bytes)
    {
        string[] units = { "B", "KB", "MB", "GB" };
        int unitIndex = 0;
        double size = bytes;
        
        while (size >= 1024 && unitIndex < units.Length - 1)
        {
            size /= 1024;
            unitIndex++;
        }
        
        return $"{size:F2} {units[unitIndex]}";
    }
    
    public IEnumerable<string> SelectMultipleAlgorithms()
    {
        Console.WriteLine("\nSelect algorithms to compare (by category):");
        var categories = algorithmFactory.GetCategories().ToList();
        
        for (int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categories[i]}");
        }
        
        Console.Write("Select category (or 0 to compare all): ");
        if (!int.TryParse(Console.ReadLine(), out int categoryChoice))
            return Enumerable.Empty<string>();
            
        List<Core.IService.IAlgorithm> algorithms;
        
        if (categoryChoice == 0)
        {
            algorithms = algorithmFactory.GetAllAlgorithms().ToList();
        }
        else if (categoryChoice > 0 && categoryChoice <= categories.Count)
        {
            var selectedCategory = categories[categoryChoice - 1];
            algorithms = algorithmFactory.GetAlgorithmsByCategory(selectedCategory).ToList();
        }
        else
        {
            return Enumerable.Empty<string>();
        }
        
        // Display algorithms and let user select multiple
        Console.WriteLine("\nAvailable Algorithms:");
        for (int i = 0; i < algorithms.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {algorithms[i].Name}");
        }
        
        Console.WriteLine("\nEnter algorithm numbers separated by commas (or 0 to select all):");
        var input = Console.ReadLine() ?? "";
        
        if (input == "0")
        {
            return algorithms.Select(a => a.Name);
        }
        
        var selectedIndices = input.Split(',')
            .Select(s => s.Trim())
            .Where(s => int.TryParse(s, out _))
            .Select(int.Parse)
            .Where(i => i > 0 && i <= algorithms.Count)
            .ToList();
        
        return selectedIndices.Select(i => algorithms[i - 1].Name);
    }
    
    public void DisplayComparisonResults(ComparisonResult result)
    {
        Console.WriteLine("\n=== Algorithm Comparison Results ===");
        
        if (result.Results.Count == 0)
        {
            Console.WriteLine("No valid comparison results available.");
            return;
        }
        
        Console.WriteLine($"\nFastest Algorithm: {result.FastestAlgorithm}");
        Console.WriteLine($"Most Memory Efficient: {result.MostMemoryEfficient}\n");
        
        Console.WriteLine("Detailed Results:");
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"{"Algorithm",-30} {"Time (ms)",-15} {"Memory",-15}");
        Console.WriteLine(new string('-', 80));
        
        foreach (var (name, metrics) in result.Results.OrderBy(r => r.Value.ExecutionTime))
        {
            Console.WriteLine($"{name,-30} {metrics.ExecutionTime.TotalMilliseconds,15:F2} {FormatMemorySize(metrics.MemoryUsed),15}");
        }
        
        Console.WriteLine(new string('-', 80));
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
