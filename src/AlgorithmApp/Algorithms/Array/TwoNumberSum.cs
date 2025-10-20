using System.Collections.ObjectModel;
using System.Security.Cryptography;
using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;

internal class TwoNumberSum : ArrayAlgorithmBase
{
    public override string Name => "Two Number Sum";
    public override string Description => "Given an array of integers nums and an integer target," +
                                          "\n Return the indices i and j such that nums[i] + nums[j] == target and i != j" +
                                          "\n You may assume that every input has exactly one pair of indices i and j that satisfy the condition." +
                                          "\nReturn the answer with the smaller index first.";
    public override string TimeComplexity => "O(n)";
    public override string SpaceComplexity => "O(n)";
    public override string Hint => "we can iterate through nums with index i. Let difference = target - nums[i] " +
                                   "and check if difference exists in the hash map as we iterate through the array, " +
                                   "else store the current element in the hashmap with its index and continue." +
                                   " We use a hashmap for O(1) lookups";
    public override object GenerateSampleInput(int size)
    {
        int[] array = Enumerable.Range(0, size)
            .Select(_ => RandomNumberGenerator.GetInt32(-1000, 1001))
            .ToArray();
        int targetSum = array[RandomNumberGenerator.GetInt32(size)] + array[RandomNumberGenerator.GetInt32(size)];
        return Tuple.Create(array, targetSum);
    }
    public override bool ValidateInput(object input) =>
        input is Tuple<int[], int> tuple &&
        tuple.Item1.Length >= 2;

    public override AlgorithmResult ExecuteAsync(object input)
    {
        if (!ValidateInput(input))
        {
            throw new ArgumentException("Invalid input. Expected a tuple of an integer array and a target sum.");
        }

        (int[]? array, int targetSum) = (Tuple<int[], int>)input;
        Collection<string> steps =
        [
            $"Input array: [{string.Join(", ", array)}]",
            $"Target sum: {targetSum}",
            "Using a hash map to track seen numbers."
        ];

        var dicStore = new Dictionary<int, int>();
        for (int i = 0; i < array.Length; i++)
        {
            int diff = targetSum - array[i];
            if (dicStore.TryGetValue(diff, out int firstIndex))
            {
                int idx1 = Math.Min(firstIndex, i);
                int idx2 = Math.Max(firstIndex, i);
                steps.Add($"Found pair: nums[{idx1}] + nums[{idx2}] == {targetSum}");
                int[][] output = [[idx1, idx2]];
                return new AlgorithmResult
                {
                    AlgorithmName = Name,
                    Input = input,
                    Output = output,
                    Steps = steps
                };
            }
            dicStore[array[i]] = i;
        }

        steps.Add("No valid pair found.");
        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = System.Array.Empty<int>(),
            Steps = steps
        };
    }
}
