using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;

public class ValidAnagramArray : ArrayAlgorithmBase
{
    public override string Name => "Valid Anagram (Array)";
    public override string Description => "Determines if two strings are anagrams using an array to count character occurrences.";
    public override string TimeComplexity => "O(n)";
    public override string SpaceComplexity => "O(1)";
    public override string Hint => "Use an array of fixed size (26 for lowercase letters) to count character occurrences.";

    public override bool ValidateInput(object input) =>
        input is Tuple<string, string> tuple &&
        !string.IsNullOrEmpty(tuple.Item1) &&
        !string.IsNullOrEmpty(tuple.Item2);

    public override object GenerateSampleInput(int size)
    {
        var random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        string? str1 = new string(Enumerable.Range(0, size)
            .Select(_ => chars[random.Next(chars.Length)]).ToArray());
        string? str2 = new string(str1.ToCharArray().OrderBy(_ => random.Next()).ToArray());
        return Tuple.Create(str1, str2);
    }

    public override AppModels.AlgorithmResult ExecuteAsync(object input)
    {
        if (!ValidateInput(input))
        {
            throw new ArgumentException("Invalid input. Expected a tuple of two non-empty strings.");
        }

        (string? s, string? t) = (Tuple<string, string>)input;
        var steps = new List<string>
        {
            $"Input strings: s = \"{s}\", t = \"{t}\"",
            "If lengths differ, they cannot be anagrams.",
            $"Length of s: {s.Length}, Length of t: {t.Length}"
        };

        if (s.Length != t.Length)
        {
            steps.Add("Lengths differ. Returning false.");
            return new AppModels.AlgorithmResult
            {
                AlgorithmName = Name,
                Input = input,
                Output = new { IsAnagram = false },
                Steps = steps
            };
        }

        int[]? count = new int[26];
        steps.Add("Using an array of size 26 to count character occurrences (a-z).");
        steps.Add("Converting characters to lowercase and updating counts.");
        
        for (int i = 0; i < s.Length; i++)
        {
            count[char.ToLowerInvariant(s[i]) - 'a']++;
            count[char.ToLowerInvariant(t[i]) - 'a']--;
        }

        steps.Add("Character counts updated for both strings.");
        bool isAnagram = count.All(c => c == 0);
        steps.Add(isAnagram ? "All counts are zero. Strings are anagrams." : "Counts differ. Strings are not anagrams.");
        
        return new AppModels.AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { IsAnagram = isAnagram },
            Steps = steps
        };
    }
}
