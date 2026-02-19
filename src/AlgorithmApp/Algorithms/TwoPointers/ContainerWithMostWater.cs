using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.TwoPointers;

internal class ContainerWithMostWater : TwoPointersAlgorithmBase
{
    public override string Name => "Container With Most Water";
    public override string Description => "Given n non-negative integers a1, a2, ..., an , where each represents a point at coordinate (i, ai). n vertical lines are drawn such that the two endpoints of line i is at (i, ai) and (i, 0). Find two lines, which together with x-axis forms a container, such that the container contains the most water.";
    public override string TimeComplexity => "O(n)";
    public override string SpaceComplexity => "O(1)";
    public override string Hint => "Use two pointers starting at both ends of the array and move them towards each other.";

    public override AlgorithmResult ExecuteAsync(object input)
    {
        if (input is not int[] nums)
        {
            throw new ArgumentException("Input must be an array of integers.", nameof(input));
        }

        int left = 0;
        int right = nums.Length - 1;
        int res = 0;

        while (left < right)
        {
            int width = right - left;
            int height = Math.Min(nums[left], nums[right]);
            int area = height * width;

            res = Math.Max(res, area);

            if (nums[left] < nums[right])
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
            Output = res
        };
    }
}
