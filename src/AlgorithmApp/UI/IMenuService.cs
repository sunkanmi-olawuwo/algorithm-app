using static AlgorithmApp.Core.AppEnum;
using static AlgorithmApp.Core.AppModels;

namespace AlgorithmApp.UI;

public interface IMenuService
{
    MenuChoice ShowMainMenu();
    string SelectAlgorithm();
    IEnumerable<string> SelectMultipleAlgorithms();
    void ShowDocumentation();
    void DisplayResults(AlgorithmResult result);
    void DisplayComparisonResults(ComparisonResult result);
}
