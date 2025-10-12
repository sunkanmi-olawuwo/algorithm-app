using AlgorithmApp.UI;
using Microsoft.Extensions.Logging;
using static AlgorithmApp.Core.AppEnum;
using static AlgorithmApp.Core.IService;

namespace AlgorithmApp.Core
{
    public class Application(
        IMenuService menuService,
        IAlgorithmRunner algorithmRunner,
        IAlgorithmComparer algorithmComparer,
        ILogger<Application> logger)
    {
        public void Run()
        {
            while (true)
            {
                var choice = menuService.ShowMainMenu();

                if (choice == MenuChoice.Exit)
                    break;

                ProcessChoice(choice);
            }

            logger.LogInformation("Application terminated");
        }

        private void ProcessChoice(MenuChoice choice)
        {
            try
            {
                switch (choice)
                {
                    case MenuChoice.RunAlgorithm:
                        algorithmRunner.RunSelectedAlgorithm();
                        break;
                    case MenuChoice.ViewDocumentation:
                        menuService.ShowDocumentation();
                        break;
                    case MenuChoice.CompareAlgorithms:
                        CompareAlgorithms();
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing choice {Choice}", choice);
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        
        private void CompareAlgorithms()
        {
            var selectedAlgorithms = menuService.SelectMultipleAlgorithms().ToList();
            
            if (!selectedAlgorithms.Any())
            {
                Console.WriteLine("No algorithms selected for comparison.");
                return;
            }
            
            Console.Write("Enter input size for comparison: ");
            if (!int.TryParse(Console.ReadLine(), out int size) || size <= 0)
            {
                Console.WriteLine("Invalid size. Using default size of 1000.");
                size = 1000;
            }
            
            Console.WriteLine($"Comparing {selectedAlgorithms.Count} algorithms with input size {size}...");
            
            var result = algorithmComparer.CompareAlgorithms(selectedAlgorithms, size);
            menuService.DisplayComparisonResults(result);
        }
    }
}
