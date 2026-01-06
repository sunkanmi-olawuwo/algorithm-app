using AlgorithmApp.Core;

namespace AlgorithmApp.Algorithms.Array;

internal class ValidSudoku : ArrayAlgorithmBase
{
    public override string Name => "Valid Sudoku";

    public override string Description => "Determines if a 9x9 Sudoku board is valid.";

    public override string TimeComplexity => "O(n^2)";

    public override string SpaceComplexity => "O(n^2)";

    public override string Hint => "Instead of checking rows, columns, and 3×3 boxes separately, we can validate the entire Sudoku board in one single pass.";

    public override bool ValidateInput(object input) => input is char[][] board && board.Length == 9 && board.All(row => row.Length == 9);
    public override object GenerateSampleInput(int size)
    {
        char[][] board =
        [
            ['5', '3', '.', '.', '7', '.', '.', '.', '.'],
            ['6', '.', '.', '1', '9', '5', '.', '.', '.'],
            ['.', '9', '8', '.', '.', '.', '.', '6', '.'],
            ['8', '.', '.', '.', '6', '.', '.', '.', '3'],
            ['4', '.', '.', '8', '.', '3', '.', '.', '1'],
            ['7', '.', '.', '.', '2', '.', '.', '.', '6'],
            ['.', '6', '.', '.', '.', '.', '2', '8', '.'],
            ['.', '.', '.', '4', '1', '9', '.', '.', '5'],
            ['.', '.', '.', '.', '8', '.', '.', '7', '9'],
        ];
        return board;
    }
    public override AlgorithmResult ExecuteAsync(object input)
    {
        var steps = new List<string>();
        char[][] board = (char[][])input;

        Dictionary<int, HashSet<char>> columns = [];
        Dictionary<int, HashSet<char>> rows = [];
        Dictionary<string, HashSet<char>> square = [];

        steps.Add("Initialized data structures for rows, columns, and squares.");

        for (int r = 0; r < 9; r++)
        {
            for (int c = 0; c < 9; c++)
            {
                steps.Add($"Checking cell at row {r}, column {c}.");
                char number = board[r][c];
                string squareKey = $"{r / 3}-{c / 3}";

                if (number == '.')
                {
                    steps.Add("Empty cell found, continuing to next cell.");
                    continue;
                }

                if(
                    rows.ContainsKey(r) && rows[r].Contains(number) ||
                    columns.ContainsKey(c) && columns[c].Contains(number) ||
                    square.ContainsKey(squareKey) && square[squareKey].Contains(number)
                )
                {
                    steps.Add($"Duplicate number {number} found in row {r}, column {c}, or square {squareKey}. Sudoku is invalid.");
                    return new AlgorithmResult
                    {
                        AlgorithmName = Name,
                        Input = input,
                        Output = false,
                        Steps = new System.Collections.ObjectModel.Collection<string>(steps)
                    };
                }

                steps.Add($"Placing number {number} in row {r}, column {c}, and square {squareKey}.");
                if (!columns.TryGetValue(c, out HashSet<char>? column))
                {
                    column = [];
                    columns[c] = column;
                }

                if (!rows.TryGetValue(r, out HashSet<char>? row))
                {
                    row = [];
                    rows[r] = row;
                }

                if (!square.TryGetValue(squareKey, out HashSet<char>? box))
                {
                    box = [];
                    square[squareKey] = box;
                }

                steps.Add($"Number {number} placed successfully.");
                row.Add(number);
                column.Add(number);
                box.Add(number);
            }
        }

        return new AlgorithmResult
        {
            AlgorithmName = Name,
            Input = input,
            Output = true,
            Steps = new System.Collections.ObjectModel.Collection<string>(steps)
        };
    }
}
