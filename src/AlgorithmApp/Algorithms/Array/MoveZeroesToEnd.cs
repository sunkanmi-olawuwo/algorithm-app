using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;

internal class MoveZeroesToEnd : ArrayAlgorithmBase
{
    public override string Name => "Move Zeroes to End";

    public override string Description => "Moves all zeroes in an array to the end while maintaining the order of non-zero elements.";

    public override string TimeComplexity => "O(n)";
    public override string SpaceComplexity => "O(1)";

    public override string Hint => "Use two pointers to track the position of non-zero elements.";

    public override AlgorithmResult ExecuteAsync(object input)
    {
        if (!ValidateInput(input))
        {
            throw new ArgumentException("Invalid input. Expected non-empty integer array.");
        }

        int[] array = (int[])input;
        int write = 0;
        Collection<string> steps =
            [
                "Initialize write pointer to 0.",
             "Iterate through the array with a read pointer.",
             "If the current element is non-zero, swap it with the element at the write pointer and increment the write pointer.",
             "Continue until the end of the array is reached."

            ];

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == 0)
            {
                continue;
            }

            (array[write], array[i]) = (array[i], array[write]);
            write++;
        }

        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { UpdatedArray = array },
            Steps = steps
        };

    }
}
