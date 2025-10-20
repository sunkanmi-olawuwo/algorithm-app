using Spectre.Console.Extensions;
using static AlgorithmApp.Core.AppModels;

namespace AlgorithmApp.Algorithms.Array;

/*
 * ============================================================================
 * ARRAY ALGORITHM SCRATCH PAD
 * ============================================================================
 * This is your practice area for implementing array algorithms.
 * Use this file to build muscle memory for common patterns.
 * 
 * HOW TO USE:
 * 1. Pick an algorithm to practice (suggestions below)
 * 2. Modify the ExecuteAsync method to implement it
 * 3. Update the Name, Description, and complexity properties
 * 4. Run the application to test your implementation
 * 5. Check the Steps output to debug your logic
 * ============================================================================
 */

public class ArrayAlgorithmScratchPad : ArrayAlgorithmBase
{
    // Update these properties based on what you're implementing
    public override string Name => "Array Scratch Pad - Group Anagrams";
    public override string Description => "Practice area for array algorithms. Currently implementing: [describe it here]";
    public override string TimeComplexity => "O(?) - analyze and update";
    public override string SpaceComplexity => "O(?) - analyze and update";
    public override string Hint => "Add your own hint here to remember the approach.";

    public override object GenerateSampleInput(int size)
    {
        // Input: strs = ["act","pots","tops","cat","stop","hat"]
        string[] sampleInput = new string[] { "listen", "silent", "enlist", "inlets", "google", "gogole" };
        return sampleInput;
    }

    public override bool ValidateInput(object input) => input is string[] { Length: > 0 };

    public override AlgorithmResult ExecuteAsync(object input)
    {
        if (!ValidateInput(input))
        {
            throw new ArgumentException("Invalid input. Expected non-empty integer array.");
        }

        string[] strs = (string[])input;
        var steps = new List<string>
        {
            "Running the Algorithm via the scratch pad",
            $"Input strings: [{string.Join(", ", strs)}]"
        };

        // ========================================================================
        // YOUR IMPLEMENTATION STARTS HERE
        // ========================================================================

        var res = new Dictionary<string, List<string>>();
        foreach (string s in strs)
        {
            int[] count = new int[26];
            foreach (char c in s)
            {
                count[c - 'a']++;
            }

            string key = string.Join(",", count);
            if (!res.ContainsKey(key))
            {
                res[key] = [];
            }

            res[key].Add(s);
        } 
        
        var output = res.Values.ToList();
        output.Dump();


        // ========================================================================
        // YOUR IMPLEMENTATION ENDS HERE
        // ========================================================================
        var result = new { Output = output };
        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { Result = result }, // Modify based on what you're returning
            Steps = steps
        };
    }

}
