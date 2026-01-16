using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;

internal class LongestConsecutiveSequence : ArrayAlgorithmBase
{
    public override string Name => "Longest Consecutive Sequence";
    public override string Description => "Finds the length of the longest consecutive elements sequence in an unsorted array of integers.";
    public override string TimeComplexity => "O(n)";
    public override string SpaceComplexity => "O(n)";
    public override string Hint => "Use a hash set to track elements and find the longest sequence.";

    public override object GenerateSampleInput(int size) => new[] { 2, 20, 4, 10, 3, 4, 5 };

    public override AlgorithmResult ExecuteAsync(object input)
    {
        int[] nums = (int[])input;
        var numSet = new HashSet<int>(nums);

        int longestStreak = 0;

        foreach (int num in numSet.Where(num => !numSet.Contains(num - 1)))
        {
            int currentStreak = 1;
            while (numSet.Contains(num + currentStreak))
            {
                currentStreak++;
            }

            longestStreak = Math.Max(longestStreak, currentStreak);
        }

        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = nums,
            Output = longestStreak,
            Steps =
            [
                "Create a hash set from the input array.",
                "Iterate through each number in the set.",
                "For each number, check if it's the start of a sequence.",
                "If it is, count the length of the sequence.",
                "Update the longest streak if the current streak is longer."
            ]
        };
    }
}
