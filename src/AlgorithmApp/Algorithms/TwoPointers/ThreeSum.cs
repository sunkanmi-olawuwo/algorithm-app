using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.TwoPointers;

internal class ThreeSum : TwoPointersAlgorithmBase
{
    public override string Name => "Three Sum";
    public override string Description => "Finds all unique triplets in the array which gives the sum of zero.";
    public override string TimeComplexity => "O(n^2)";
    public override string SpaceComplexity => "O(1)";
    public override string Hint => "Use sorting and two pointers.";
    public override bool ValidateInput(object input) => input is int[] { Length: > 1 };
    public override object GenerateSampleInput(int size) => new[] { -1, 0, 1, 2, -1, -4 };

    public override AlgorithmResult ExecuteAsync(object input)
    {
        if (input is not int[] nums)
        {
            throw new ArgumentException("Input must be an array of integers.", nameof(input));
        }

        var response = new List<List<int>>();

        System.Array.Sort(nums);
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] > 0)
            {
                break;
            }

            if (i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }

            int l = i + 1;
            int r = nums.Length - 1;

            while (l < r)
            {
                int sum = nums[i] + nums[l] + nums[r];
                if (sum < 0)
                {
                    l++;
                }
                else if (sum > 0)
                {
                    r--;
                }
                else
                {
                    response.Add([nums[i], nums[l], nums[r]]);
                    l++;
                    r--;
                    while (l < r && nums[l] == nums[l - 1])
                    {
                        l++;
                    }
                }
            }
        }

        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = response
        };
    }
}
