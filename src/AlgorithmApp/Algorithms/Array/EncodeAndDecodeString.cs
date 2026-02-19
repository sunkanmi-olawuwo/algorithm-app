using System.Text;
using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;
internal class EncodeAndDecodeString : ArrayAlgorithmBase
{
    public override string Name => "Encode and Decode String";
    public override string Description => "Encodes and decodes strings using a simple algorithm.";
    public override string TimeComplexity => "O(m)";
    public override string SpaceComplexity => "O(m + n)";
    public override string Hint => "We can use an encoding approach where we start with a number representing the length of the string, followed by a separator character (let's use # for simplicity), and then the string itself. To decode, we read the number until we reach a #, then use that number to read the specified number of characters as the string.";
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

        var encodedString = new StringBuilder();
        foreach (string str in stringArray)
        {
            encodedString.Append(
                System.Globalization.CultureInfo.InvariantCulture,
                $"{str.Length}#{str}"
            );
        }

        steps.Add($"Encoded string: {encodedString}");

        List<string> decodedString = [];
        int i = 0;
        int encodedStringLength = encodedString.ToString().Length;
        while (i < encodedStringLength)
        {
            int j = i;
            steps.Add($"Decoding: looking for '#' starting at index {j}");
            while (encodedString.ToString()[j] != '#' && j < encodedStringLength)
            {
                j++;
                steps.Add($"Found '#' at index {j}");
            }

          
            if (!int.TryParse(encodedString.ToString()[i..j], out int len))
            {
                throw new FormatException("Invalid length.");
            }
            steps.Add($"Fetching the length of the string {len}");

            int start = j + 1;
            int end = start + len;
            if (end > encodedStringLength)
            {
                throw new FormatException("Payload exceeds input length.");
            }

            decodedString.Add(encodedString.ToString()[start..end]);
            i = end; // next record
        }

        steps.Add($"Encoded string length: {encodedString.Length}");
        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = new { Encoded = encodedString.ToString(), Decoded = decodedString.ToArray() },
            Steps = new System.Collections.ObjectModel.Collection<string>(steps)
        };
    }
}
