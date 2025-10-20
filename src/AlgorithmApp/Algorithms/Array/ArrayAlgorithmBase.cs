using System.Security.Cryptography;
using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;


internal abstract class ArrayAlgorithmBase : IAlgorithm
{
    public abstract string Name { get; }
    public string Category => "Array";
    public abstract string Description { get; }
    public abstract string TimeComplexity { get; }
    public abstract string SpaceComplexity { get; }
    public abstract string Hint { get; }

    public virtual bool ValidateInput(object input) => input is int[] array && array.Length > 0;

    public virtual object GenerateSampleInput(int size) =>
        Enumerable.Range(0, size)
            .Select(_ => RandomNumberGenerator.GetInt32(-1000, 1001))
            .ToArray();

    public abstract AlgorithmResult ExecuteAsync(object input);
}

