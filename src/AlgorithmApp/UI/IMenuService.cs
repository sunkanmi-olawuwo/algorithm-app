using AlgorithmApp.Core;

namespace AlgorithmApp.UI;

internal interface IMenuService
{
    AppEnum.MenuChoice ShowMainMenu();
    string SelectAlgorithm();
    IEnumerable<string> SelectMultipleAlgorithms();
    void ShowDocumentation();
    void DisplayResults(AlgorithmResult result);
    void DisplayComparisonResults(ComparisonResult result);
}
