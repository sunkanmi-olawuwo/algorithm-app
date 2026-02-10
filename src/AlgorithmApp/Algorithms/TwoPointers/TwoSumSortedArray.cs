using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.TwoPointers;

internal class TwoSumSortedArray : TwoPointersAlgorithmBase
{
    public override string Name => "Two Sum (Sorted Array)";
    public override string Description => "Finds two numbers in a sorted array that add up to a specific target.";
    public override string TimeComplexity => "O(n)";
    public override string SpaceComplexity => "O(1)";
    public override string Hint => "Use two pointers to find the two numbers.";

    public override bool ValidateInput(object input) => input is int[] { Length: > 1 };

    public override object GenerateSampleInput(int size) => new Tuple<int[], int>([1, 2, 3, 4, 5], 5);

    public override AlgorithmResult ExecuteAsync(object input)
    {
        if (input is not Tuple<int[], int>(var arr, var target))
        {
            throw new ArgumentException("Input must be a tuple of (int[] array, int target).");
        }

        int left = 0;
        int right = arr.Length - 1;
        while (left < right)
        {    
            int sum = arr[left] + arr[right];
            if (sum == target)
            {
                return new AlgorithmResult
                {
                    AlgorithmName = Name,
                    Input = input,
                    Output = new { Indices = new[] { left, right }, Values = new[] { arr[left], arr[right] } },
                    Steps =
                    [
                        $"Found numbers: {arr[left]} and {arr[right]} that add up to {target}."
                    ]
                };
            }

            if (sum < target)
            {
                left++;
            }
            else
            {
                right--;
            }
        }
        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { Message = "No two numbers found that add up to the target." },
            Steps =
            [
                "No valid pair found after checking all possibilities."
            ]
        };
    }
}
