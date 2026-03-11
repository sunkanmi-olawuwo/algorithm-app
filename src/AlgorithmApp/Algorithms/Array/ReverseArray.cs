using System.Collections.ObjectModel;
using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;

internal class ReverseArray : ArrayAlgorithmBase
{
    public override string Name => "Reverse Array";
    public override string Description => "Reverses the elements of an array.";
    public override string TimeComplexity => "O(n)";
    public override string SpaceComplexity => "O(1)";
    public override string Hint => "Swap elements from both ends towards the center.";

    public override AlgorithmResult ExecuteAsync(object input)
    {
        if (!ValidateInput(input))
        {
            throw new ArgumentException("Invalid input. Expected non-empty integer array.");
        }

        int[] array = (int[])input;

        int left = 0;
        int right = array.Length - 1;
        Collection<string> steps =
        [
            $"Start with left index at {left} and right index at {right}.",
            "Swap elements at left and right indices.",
            "Move left index one step to the right and right index one step to the left.",
            "Repeat until left index is no longer less than right index."
        ];

        while (left < right)
        {
            (array[left], array[right]) = (array[right], array[left]);
            left++;
            right--;
        }

        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { ReversedArray = array },
            Steps = steps,
        };
    }
}
