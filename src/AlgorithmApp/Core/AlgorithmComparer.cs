namespace AlgorithmApp.Core;

internal interface IAlgorithmComparer
{
    ComparisonResult CompareAlgorithms(IEnumerable<string> algorithmNames, int inputSize);
}

internal class AlgorithmComparer : IAlgorithmComparer
{
    private readonly IAlgorithmFactory _algorithmFactory;
    private readonly IPerformanceMeasurer _performanceMeasurer;

    public AlgorithmComparer(
        IAlgorithmFactory algorithmFactory,
        IPerformanceMeasurer performanceMeasurer)
    {
        _algorithmFactory = algorithmFactory;
        _performanceMeasurer = performanceMeasurer;
    }

    public ComparisonResult CompareAlgorithms(IEnumerable<string> algorithmNames, int inputSize)
    {
        var results = new Dictionary<string, PerformanceMetrics>();
            
        foreach (string? name in algorithmNames)
        {
            IAlgorithm? algorithm = _algorithmFactory.GetAlgorithm(name);
            if (algorithm == null)
            {
                continue;
            }

            // Generate input
            object input = algorithm.GenerateSampleInput(inputSize);
                
            // Validate input
            if (!algorithm.ValidateInput(input))
            {
                continue;
            }

            // Measure performance
            AlgorithmResult? result = null;
            PerformanceMetrics metrics = _performanceMeasurer.Measure(() => result = algorithm.ExecuteAsync(input));
                
            results[name] = metrics;
        }
            
        // Find fastest and most memory efficient algorithms
        string fastest = "";
        string mostMemoryEfficient = "";
            
        if (results.Count > 0)
        {
            fastest = results.OrderBy(r => r.Value.ExecutionTime).First().Key;
            mostMemoryEfficient = results.OrderBy(r => r.Value.MemoryUsed).First().Key;
        }
            
        return new ComparisonResult(
            FastestAlgorithm: fastest,
            MostMemoryEfficient: mostMemoryEfficient)
        {
            Results = results
        };
    }
}
