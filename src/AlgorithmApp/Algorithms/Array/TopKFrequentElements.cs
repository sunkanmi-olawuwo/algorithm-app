using System.Security.Cryptography;
using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;
internal class TopKFrequentElements : ArrayAlgorithmBase
{
    public override string Name => "Top K Frequent Elements";

    public override string Description => "Find the top K most frequent elements in an array.";

    public override string TimeComplexity => "O(n)";

    public override string SpaceComplexity => "O(n)";

    public override string Hint => "Use the bucket sort algorithm to create n buckets, grouping numbers based on their frequencies from 1 to n. Then, pick the top k numbers from the buckets, starting from n down to 1";
    public override bool ValidateInput(object input) =>
      input is Tuple<int[], int> tuple &&
      tuple.Item1.Length >= 2;

    public override object GenerateSampleInput(int size)
    {
        int[] array = Enumerable.Range(0, size)
            .Select(_ => RandomNumberGenerator.GetInt32(-10, 11))
            .ToArray();
        int k = Math.Max(1, size / 10);

        return Tuple.Create(array, k);

    }

    public override AlgorithmResult ExecuteAsync(object input)
    {
        var steps = new List<string>();
        
        if (!ValidateInput(input))
        {
            throw new ArgumentException("Invalid input. Expected non-empty integer array.");
        }

        (int[]? nums, int k) = (Tuple<int[], int>)input;
        steps.Add($"Input: array = [{string.Join(", ", nums)}], k = {k}");

        var count = new Dictionary<int, int>();
        foreach (int n in nums)
        {
            count[n] = count.TryGetValue(n, out int c) ? c + 1 : 1;
        }
        steps.Add($"Frequency map created: {string.Join(", ", count.Select(kvp => $"{kvp.Key}:{kvp.Value}"))}");

        var freq = new List<int>[nums.Length + 1];
        for (int i = 0; i < freq.Length; i++)
        {
            freq[i] = new List<int>();
        }

        foreach (KeyValuePair<int, int> kvp in count)
        {
            freq[kvp.Value].Add(kvp.Key);
        }
        steps.Add($"Bucket sort: grouped numbers by frequency (1 to {nums.Length})");

        int take = Math.Min(k, count.Count);
        int[] res = new int[take];
        int idx = 0;

        for (int f = freq.Length - 1; f >= 1 && idx < take; f--)
        {
            List<int> bucket = freq[f];
            for (int i = 0; i < bucket.Count && idx < take; i++)
            {
                res[idx++] = bucket[i];
                steps.Add($"Added element {bucket[i]} with frequency {f} to result");
            }
        }

        steps.Add($"Top {k} frequent elements: [{string.Join(", ", res)}]");

        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = res,
            Steps = [.. steps]
        };
    }
}
