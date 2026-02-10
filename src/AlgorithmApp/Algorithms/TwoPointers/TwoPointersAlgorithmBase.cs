using System.Security.Cryptography;
using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.TwoPointers;


internal abstract class TwoPointersAlgorithmBase : IAlgorithm
{
    public abstract string Name { get; }
    public string Category => "Two Pointers";
    public abstract string Description { get; }
    public abstract string TimeComplexity { get; }
    public abstract string SpaceComplexity { get; }
    public abstract string Hint { get; }

    public virtual bool ValidateInput(object input) => input is int[] { Length: > 0 };

    public virtual object GenerateSampleInput(int size) =>
        Enumerable.Range(0, size)
            .Select(_ => RandomNumberGenerator.GetInt32(-1000, 1001))
            .ToArray();

    public abstract AlgorithmResult ExecuteAsync(object input);
}

