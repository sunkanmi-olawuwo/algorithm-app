using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;

public class ValidAnagramDictionary : ArrayAlgorithmBase
{
    public override string Name => "Valid Anagram (Dictionary)";
    public override string Description => "Determines if two strings are anagrams using a dictionary to count character occurrences.";
    public override string TimeComplexity => "O(n)";
    public override string SpaceComplexity => "O(1)";
    public override string Hint => "Use a dictionary to count character occurrences.";

    public override bool ValidateInput(object input)
    {
        return input is Tuple<string, string> tuple &&
               !string.IsNullOrEmpty(tuple.Item1) &&
               !string.IsNullOrEmpty(tuple.Item2);
    }
    public override object GenerateSampleInput(int size)
    {
        var random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        var str1 = new string(Enumerable.Range(0, size)
            .Select(_ => chars[random.Next(chars.Length)]).ToArray());
        var str2 = new string(str1.ToCharArray().OrderBy(_ => random.Next()).ToArray());
        return Tuple.Create(str1, str2);
    }

    public override AppModels.AlgorithmResult ExecuteAsync(object input)
    {
        if (!ValidateInput(input))
            throw new ArgumentException("Invalid input. Expected a tuple of two non-empty strings.");
        var (s, t) = (Tuple<string, string>)input;
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

        var countS = new Dictionary<char, int>();
        var countT = new Dictionary<char, int>();

        for (int i = 0; i < s.Length; i++)
        {
            countS[s[i]] = countS.GetValueOrDefault(s[i], 0) + 1;
            countT[t[i]] = countT.GetValueOrDefault(t[i], 0) + 1;
        }

        steps.Add("Character counts for s: " + string.Join(", ", countS.Select(kv => $"'{kv.Key}': {kv.Value}")));
        steps.Add("Character counts for t: " + string.Join(", ", countT.Select(kv => $"'{kv.Key}': {kv.Value}")));

        // ReSharper disable once UsageOfDefaultStructEquality
        bool isAnagram = countS.Count == countT.Count && !countS.Except(countT).Any();
        steps.Add(isAnagram
            ? "Dictionaries match. Strings are anagrams."
            : "Dictionaries do not match. Strings are not anagrams.");
        return new AppModels.AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { IsAnagram = isAnagram },
            Steps = steps
        };
    }
}