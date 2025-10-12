using static AlgorithmApp.Core.IService;

namespace AlgorithmApp.Core;

public class AlgorithmFactory(IEnumerable<IService.IAlgorithm> algorithms) : IAlgorithmFactory
{
    public IAlgorithm GetAlgorithm(string name) =>
        algorithms.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase))!;

    public IEnumerable<IAlgorithm> GetAlgorithmsByCategory(string category) =>
        algorithms.Where(a => a.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<string> GetCategories() =>
        algorithms.Select(a => a.Category).Distinct();

    public IEnumerable<IAlgorithm> GetAllAlgorithms() => algorithms;
}
