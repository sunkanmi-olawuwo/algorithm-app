using static AlgorithmApp.Core.AppModels;

namespace AlgorithmApp.Core;

public class IService
{
    public interface IAlgorithm
    {
        string Name { get; }
        string Category { get; }
        string Description { get; }
        string TimeComplexity { get; }
        string SpaceComplexity { get; }
        AlgorithmResult ExecuteAsync(object input);
        object GenerateSampleInput(int size);
        bool ValidateInput(object input);
    }

    public interface IAlgorithmRunner
    {
        void RunSelectedAlgorithm();
    }

    public interface IAlgorithmFactory
    {
        IAlgorithm GetAlgorithm(string name);
        IEnumerable<IAlgorithm> GetAlgorithmsByCategory(string category);
        IEnumerable<string> GetCategories();
        IEnumerable<IAlgorithm> GetAllAlgorithms();
    }
}
