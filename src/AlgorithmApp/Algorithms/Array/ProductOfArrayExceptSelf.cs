using System.Collections.ObjectModel;
using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;

internal class ProductOfArrayExceptSelf : ArrayAlgorithmBase
{
    public override string Name => "Product of Array Except Self";

    public override string Description => "Given an integer array nums, return an array output where output[i] is the product of all the elements of nums except nums[i].";

    public override string TimeComplexity => "O(n)";

    public override string SpaceComplexity => "O(1) extra space and O(n) space for the output array";

    public override string Hint => "We can use the stored prefix and suffix products to compute the result array by iterating through the array and simply multiplying the prefix and suffix products at each index.";

    public override AlgorithmResult ExecuteAsync(object input)
    {
        if (!ValidateInput(input))
        {
            throw new ArgumentException("Invalid input. Expected non-empty integer array.");
        }

        Collection<string> steps =
        [
            "Initialize an output array result of the same length as nums, filled with 1s.",
        ];

        int[] nums = (int[])input;

        int n = nums.Length;
        int[] result = new int[n];
        System.Array.Fill(result, 1);

        steps.Add("Calculate prefix products and store them in result array.");
        for (int i = 1; i < n; i++)
        {
            result[i] = result[i - 1] * nums[i-1];
        }

        steps.Add("Calculate suffix products and multiply them with the corresponding prefix products in result array.");
        int postfix = 1;
        for (int i = n - 1; i >= 0; i--)
        {
            result[i] *= postfix;

            steps.Add($"At index {i}, multiply prefix product {result[i]} with suffix product {postfix} to get {result[i]}.");
            postfix *= nums[i];
        }

        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { ProductArray =  result}
        };

    }
}
