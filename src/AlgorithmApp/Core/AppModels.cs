using System.Collections.ObjectModel;

namespace AlgorithmApp.Core;

internal record ComplexityInfo(string Best = "", string Average = "", string Worst = "")
{
    public override string ToString() =>
        $"Best: {Best}, Average: {Average}, Worst: {Worst}";
}

internal record AlgorithmResult(
    string AlgorithmName = "",
    object? Input = null,
    object? Output = null)
{
    public Collection<string> Steps { get; init; } = [];
    public PerformanceMetrics? PerformanceMetrics { get; init; }
}

internal record PerformanceMetrics(
    TimeSpan ExecutionTime = default,
    long MemoryUsed = 0,
    int Comparisons = 0,
    int Swaps = 0,
    int Iterations = 0);

internal record ComparisonResult(
    string FastestAlgorithm = "", 
    string MostMemoryEfficient = "")
{
    public Dictionary<string, PerformanceMetrics> Results { get; init; } = [];
}
