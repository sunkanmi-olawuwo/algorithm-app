using static AlgorithmApp.Core.AppModels;
using static AlgorithmApp.Core.IService;

namespace AlgorithmApp.Algorithms.Array;


public abstract class ArrayAlgorithmBase : IAlgorithm
{
    public abstract string Name { get; }
    public string Category => "Array";
    public abstract string Description { get; }
    public abstract string TimeComplexity { get; }
    public abstract string SpaceComplexity { get; }
    public abstract string Hint { get; }

    public virtual bool ValidateInput(object input) => input is int[] array && array.Length > 0;

    public virtual object GenerateSampleInput(int size)
    {
        var random = new Random();
        return Enumerable.Range(0, size)
            .Select(_ => random.Next(-1000, 1001))
            .ToArray();
    }

    public abstract AlgorithmResult ExecuteAsync(object input);
}

