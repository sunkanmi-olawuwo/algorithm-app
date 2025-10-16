using static AlgorithmApp.Core.AppModels;

namespace AlgorithmApp.Algorithms.Array;

public class FindLargestNumber : ArrayAlgorithmBase
{
    public override string Name => "Find Largest Number";
    public override string Description => "Finds the largest number in an array.";
    public override string TimeComplexity => "O(n)";
    public override string SpaceComplexity => "O(1)";
    public override string Hint => "Iterate through the array, keeping track of the largest number found.";

    public override AlgorithmResult ExecuteAsync(object input)
    {
        if (!ValidateInput(input))
            throw new ArgumentException("Invalid input. Expected non-empty integer array.");

        var array = (int[])input;
        var steps = new List<string>
        {
            $"Starting search for largest number in array of size {array.Length}",
            $"Initial array: [{string.Join(", ", array.Take(Math.Min(10, array.Length)))}]" +
                 (array.Length > 10 ? "..." : "")
        };

        // Initialize with first element
        int largest = array[0];
        int largestIndex = 0;
        steps.Add($"Initialize largest = {largest} (at index 0)");

        // Iterate through remaining elements
        for (int i = 1; i < array.Length; i++)
        {
           
            if (array[i] > largest)
            {
                steps.Add($"Found new largest: {array[i]} at index {i} (previous: {largest})");
                largest = array[i];
                largestIndex = i;
            }
            else if (i < 5 || i == array.Length - 1) // Log first few and last comparison
            {
                steps.Add($"Index {i}: {array[i]} <= {largest}, keeping current largest");
            }
        }

        steps.Add($"\nCompleted: Largest number is {largest} at index {largestIndex}");

        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { Value = largest, Index = largestIndex },
            Steps = steps
        };
    }
}
