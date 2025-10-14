using AlgorithmApp.Core;
using static AlgorithmApp.Core.AppModels;

namespace AlgorithmApp.Algorithms.Array;

public class ContainsDuplicate : ArrayAlgorithmBase
{
    public override string Name  => "Contains Duplicate";
    public override string Description => "Checks if an array contains any duplicates.";
    public override string TimeComplexity => "O(n)";
    public override string SpaceComplexity => "O(n)";
    public override AppModels.AlgorithmResult ExecuteAsync(object input)
    {
        if (!ValidateInput(input))
            throw new ArgumentException("Invalid input. Expected non-empty integer array.");

        var array = (int[])input;
        var steps = new List<string>
        {
            "A HashSet only keeps unique values — duplicates collapse into one entry.",
            "If there were any duplicates in the array, the set’s Count will be less than array.Length.",
            "Create a new HashSet and store the array in it"
        };

        var hashSet = new HashSet<int>(array);
        steps.Add($"HashSet created with {hashSet.Count} unique elements from array of length {array.Length}");
        bool containsDuplicates = hashSet.Count < array.Length;

        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { ContainsDuplicate = containsDuplicates },
            Steps = steps
        };
    }
}