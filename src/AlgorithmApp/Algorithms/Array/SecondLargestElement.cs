using System.Collections.ObjectModel;
using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;

internal class SecondLargestElement : ArrayAlgorithmBase
{
    public override string Name => "Second Largest Element in an Array";
    public override string Description => "Finds the second largest element in an array.";
    public override string TimeComplexity => "O(n)";
    public override string SpaceComplexity => "O(1)";
    public override string Hint => "Keep track of the largest and second largest elements while iterating through the array.";

    public override AlgorithmResult ExecuteAsync(object input)
    {
        if(!ValidateInput(input))
        {
            throw new ArgumentException("Invalid input. Expected non-empty integer array.");
        }

        int[] array = (int[])input;
        Collection<string> steps = [
            $"Input array: [{string.Join(", ", array)}]",
            "Initialize largest and secondLargest to int.MinValue.",
            "Iterate through the array:",
            ];

        if (array.Length < 2)
        {
            return new AlgorithmResult
            {
                AlgorithmName = Name,
                Input = input,
                Output = new { SecondLargestElement = -1 },
            };
        }

        int largest = int.MinValue; 
        int secondLargest = int.MinValue;

        foreach (int t in array)
        {
            if (t > largest)
            {
                secondLargest = largest;
                largest = t;
            }
            else if (t > secondLargest && t != largest)
            {
                secondLargest = t;
            }
        }

        steps.Add($"Largest element: {largest}");
        steps.Add($"Second largest element: {secondLargest}");
        
        secondLargest = secondLargest == int.MinValue ? -1 : secondLargest;

        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { SecondLargestElement = secondLargest },
        };
    }
}
