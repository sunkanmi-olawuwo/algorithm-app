namespace AlgorithmApp.Core;

internal interface IAlgorithm
{
    string Name { get; }
    string Category { get; }
    string Description { get; }
    string TimeComplexity { get; }
    string SpaceComplexity { get; }
    string Hint { get; }
    AlgorithmResult ExecuteAsync(object input);
    object GenerateSampleInput(int size);
    bool ValidateInput(object input);
}

internal interface IAlgorithmRunner
{
    void RunSelectedAlgorithm();
}

internal interface IAlgorithmFactory
{
    IAlgorithm GetAlgorithm(string name);
    IEnumerable<IAlgorithm> GetAlgorithmsByCategory(string category);
    IEnumerable<string> GetCategories();
    IEnumerable<IAlgorithm> GetAllAlgorithms();
}
