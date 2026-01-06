using AlgorithmApp.Algorithms.Array;
using AlgorithmApp.Core;

namespace AlgorithmApp.Tests.Algorithms.Array;

[TestFixture]
public class ValidSudokuTests
{
    private ValidSudoku _algorithm;

    [SetUp]
    public void Setup() => _algorithm = new ValidSudoku();

    [Test]
    public void Name_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Name, Is.EqualTo("Valid Sudoku"));

    [Test]
    public void Category_ReturnsCorrectValue() =>
        Assert.That(_algorithm.Category, Is.EqualTo("Array"));

    [Test]
    public void TimeComplexity_ReturnsCorrectValue() =>
        Assert.That(_algorithm.TimeComplexity, Is.EqualTo("O(n^2)"));

    [Test]
    public void SpaceComplexity_ReturnsCorrectValue() =>
        Assert.That(_algorithm.SpaceComplexity, Is.EqualTo("O(n^2)"));

    [Test]
    public void ValidateInput_WithValid9x9Board_ReturnsTrue()
    {
        // Arrange
        char[][] board = CreateValidBoard();

        // Act
        bool result = _algorithm.ValidateInput(board);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateInput_WithNonCharArrayInput_ReturnsFalse()
    {
        // Arrange
        string input = "not a board";

        // Act
        bool result = _algorithm.ValidateInput(input);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateInput_WithWrongRowCount_ReturnsFalse()
    {
        // Arrange
        char[][] board = new char[8][];
        for (int i = 0; i < 8; i++)
            board[i] = new char[9];

        // Act
        bool result = _algorithm.ValidateInput(board);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateInput_WithWrongColumnCount_ReturnsFalse()
    {
        // Arrange
        char[][] board = new char[9][];
        for (int i = 0; i < 9; i++)
            board[i] = new char[8];

        // Act
        bool result = _algorithm.ValidateInput(board);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GenerateSampleInput_Returns9x9Board()
    {
        // Act
        object result = _algorithm.GenerateSampleInput(0);

        // Assert
        Assert.That(result, Is.TypeOf<char[][]>());
        char[][] board = (char[][])result;
        Assert.That(board.Length, Is.EqualTo(9));
        Assert.That(board.All(row => row.Length == 9), Is.True);
    }

    [Test]
    public void ExecuteAsync_WithValidBoard_ReturnsTrue()
    {
        // Arrange
        char[][] board = CreateValidBoard();

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(board);

        // Assert
        Assert.That(result.Output, Is.True);
        Assert.That(result.AlgorithmName, Is.EqualTo("Valid Sudoku"));
    }

    [Test]
    public void ExecuteAsync_WithDuplicateInRow_ReturnsFalse()
    {
        // Arrange
        char[][] board = CreateEmptyBoard();
        board[0][0] = '5';
        board[0][4] = '5'; // Duplicate in row 0

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(board);

        // Assert
        Assert.That(result.Output, Is.False);
    }

    [Test]
    public void ExecuteAsync_WithDuplicateInColumn_ReturnsFalse()
    {
        // Arrange
        char[][] board = CreateEmptyBoard();
        board[0][0] = '5';
        board[4][0] = '5'; // Duplicate in column 0

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(board);

        // Assert
        Assert.That(result.Output, Is.False);
    }

    [Test]
    public void ExecuteAsync_WithDuplicateIn3x3Box_ReturnsFalse()
    {
        // Arrange
        char[][] board = CreateEmptyBoard();
        board[0][0] = '5';
        board[2][2] = '5'; // Duplicate in top-left 3x3 box

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(board);

        // Assert
        Assert.That(result.Output, Is.False);
    }

    [Test]
    public void ExecuteAsync_WithEmptyBoard_ReturnsTrue()
    {
        // Arrange
        char[][] board = CreateEmptyBoard();

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(board);

        // Assert
        Assert.That(result.Output, Is.True);
    }

    [Test]
    public void ExecuteAsync_WithSameNumberInDifferentBoxes_ReturnsTrue()
    {
        // Arrange
        char[][] board = CreateEmptyBoard();
        board[0][0] = '5'; // Top-left box
        board[0][3] = '5'; // Top-middle box (different box, same row is invalid)

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(board);

        // Assert - Same number in same row should be invalid
        Assert.That(result.Output, Is.False);
    }

    [Test]
    public void ExecuteAsync_WithSameNumberInDifferentRowsColumnsAndBoxes_ReturnsTrue()
    {
        // Arrange
        char[][] board = CreateEmptyBoard();
        board[0][0] = '5'; // Top-left box
        board[4][4] = '5'; // Center box (different row, column, and box)

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(board);

        // Assert
        Assert.That(result.Output, Is.True);
    }

    [Test]
    public void ExecuteAsync_ReturnsSteps()
    {
        // Arrange
        char[][] board = CreateValidBoard();

        // Act
        AlgorithmResult result = _algorithm.ExecuteAsync(board);

        // Assert
        Assert.That(result.Steps, Is.Not.Null);
        Assert.That(result.Steps.Count, Is.GreaterThan(0));
    }

    private static char[][] CreateValidBoard()
    {
        return
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
    }

    private static char[][] CreateEmptyBoard()
    {
        char[][] board = new char[9][];
        for (int i = 0; i < 9; i++)
        {
            board[i] = ['.', '.', '.', '.', '.', '.', '.', '.', '.'];
        }
        return board;
    }
}
