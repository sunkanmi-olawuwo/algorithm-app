using System.Globalization;
using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.TwoPointers;

internal class ValidPalindrome : TwoPointersAlgorithmBase
{
    public override string Name  => "Valid Palindrome";
    public override string Description  => "Checks if a string is a palindrome using the two-pointer technique.";
    public override string TimeComplexity  => "O(n)";
    public override string SpaceComplexity  => "O(1)";
    public override string Hint  => "Use two pointers to compare characters from both ends.";

    public override bool ValidateInput(object input) => input is string s && !string.IsNullOrEmpty(s);

    public override object GenerateSampleInput(int size) => "Was it a car or a cat I saw?";

    public override AlgorithmResult ExecuteAsync(object input)
    {
        if (input is not string s)
        {
            throw new ArgumentException("Invalid input. Expected a string.");
        }

        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            while (left < right && !IsAlphanumeric(s[left]))
            {
                left++;
            }

            while (left < right && !IsAlphanumeric(s[right]))
            {
                right--;
            }

            if (char.ToLower(s[left], CultureInfo.InvariantCulture) != char.ToLower(s[right], CultureInfo.InvariantCulture))
            {
                return new AlgorithmResult
                {
                    AlgorithmName = Name,
                    Input = input,
                    Output = new { IsPalindrome = false },
                    Steps =
                    [
                        $"Comparing characters: {s[left]} and {s[right]}",
                        "Characters do not match. Not a palindrome."
                    ]
                };
            }
            left++;
            right--;
        }

        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { IsPalindrome = true },
            Steps = ["All character comparisons matched. Is a palindrome."]
        };
    }

    private static bool IsAlphanumeric(char c) => char.IsLetterOrDigit(c);
}
