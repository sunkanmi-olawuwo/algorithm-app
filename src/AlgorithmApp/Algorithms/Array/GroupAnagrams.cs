using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;
internal class GroupAnagrams : ArrayAlgorithmBase
{
    public override string Name => "Group Anagrams";
    public override string Description => "Given an array of strings strs, group all anagrams together into sub-lists. You may return the output in any order.";
    public override string TimeComplexity => "O(m)";
    public override string SpaceComplexity => "O(m*n)";
    public override string Hint => "Use an array of fixed size (26 for lowercase letters) to count character occurrences.";

    public override bool ValidateInput(object input) => input is string[] { Length: > 0 };

    public override object GenerateSampleInput(int size)
    {
        string[] sampleInput = ["listen", "silent", "enlist", "inlets", "google", "gogole"];
        return sampleInput;
    }

    public override AlgorithmResult ExecuteAsync(object input)
    {
        var steps = new List<string>();
        
        if (!ValidateInput(input))
        {
            throw new ArgumentException("Invalid input. Expected non-empty string array.");
        }

        string[] stringArray = (string[])input;
        steps.Add($"Starting with {stringArray.Length} strings to group");
        
        var dictStore = new Dictionary<string, List<string>>();

        foreach (string str in stringArray)
        {
            int[] charCount = new int[26];
            
            foreach (char c in str)
            {
                charCount[c - 'a']++;
            }

            string key = string.Join(",", charCount);
            steps.Add($"Processing '{str}' with character frequency key: {key}");

            if (!dictStore.TryGetValue(key, out List<string>? group))
            {
                group = [];
                dictStore.Add(key, group);
                steps.Add($"Created new anagram group for key: {key}");
            }

            group.Add(str);
            steps.Add($"Added '{str}' to anagram group");
        }

        var output = dictStore.Values.ToList();
        steps.Add($"Grouped {stringArray.Length} strings into {output.Count} anagram group(s)");
        
        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = output,
            Steps = new System.Collections.ObjectModel.Collection<string>(steps)
        };
    }
}
