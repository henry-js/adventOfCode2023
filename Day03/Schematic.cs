namespace Day03;

public class Schematic
{
    private Schematic(char[,] grid)
    {
        Grid = grid;
    }

    public char[,] Grid { get; }

    public static Schematic Import(string[] lines)
    {
        return GenerateGrid(lines);
    }

    private static Schematic GenerateGrid(string[] lines)
    {
        char[,] grid = new char[lines[0].Length, lines.Length];
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                grid[row, col] = lines[row][col];
            }
        }

        return new Schematic(grid);
    }

    public List<Part> GetPartsList()
    {
        List<string> numbers = [];

        for (int row = 0; row < Grid.GetLength(0); row++)
        {
            for (int col = 0; col < Grid.GetLength(1); col++)
            {
                var num = "";
                if (char.IsDigit(Grid[row, col]))
                {
                    num += Grid[row, col];
                }
                else
                {
                    if (num.Length > 0)
                    {
                        var part = new Part { PartNum = num, Pos = new Position(row) };
                    }
                }
            }
        }
    }


}

public record Part
{
    public string PartNum { get; set; } = string.Empty;
}