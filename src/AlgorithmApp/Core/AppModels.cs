namespace AlgorithmApp.Core;

public class AppModels
{
    public record ComplexityInfo(string Best = "", string Average = "", string Worst = "")
    {
        public override string ToString() =>
            $"Best: {Best}, Average: {Average}, Worst: {Worst}";
    }

    public record AlgorithmResult(
        string AlgorithmName = "",
        object? Input = null,
        object? Output = null)
    {
        public List<string> Steps { get; init; } = [];
        public PerformanceMetrics? PerformanceMetrics { get; init; }
    }

    public record PerformanceMetrics(
        TimeSpan ExecutionTime = default,
        long MemoryUsed = 0,
        int Comparisons = 0,
        int Swaps = 0,
        int Iterations = 0);

    public record ComparisonResult(
        string FastestAlgorithm = "", 
        string MostMemoryEfficient = "")
    {
        public Dictionary<string, PerformanceMetrics> Results { get; init; } = [];
    }
}
