
var currentDir = Directory.GetCurrentDirectory();
var dataFile = "data.txt";
var path = File.Exists(Path.Combine(currentDir, dataFile)) ? Path.Combine(currentDir, dataFile) : Path.Combine(currentDir, "Day01", dataFile);

var lines = File.ReadAllLines(path);
var instructions = lines[0];
var navNodes = lines[2..]
    .Select(line => new Navigate(line[0..3], line[7..10], line[12..15]))
    .ToDictionary(x => x.Current);

(Navigate pos, int turns) result = (navNodes["AAA"], 0);

for (int i = 0; i < instructions.Length; i++)
{
    result = TakeTurn(result, instructions[i]);
    if (result.pos == navNodes["ZZZ"]) break;
    if (i == (instructions.Length - 1)) i = 0;
}

AnsiConsole.WriteLine($"Final position: {result.pos}, Turns taken {result.turns}");

(Navigate pos, int turns) TakeTurn((Navigate pos, int turns) result, char leftOrRight)
{
    result.pos = leftOrRight switch
    {
        'L' => navNodes[result.pos.LeftNode],
        'R' => navNodes[result.pos.RightNode],
        _ => throw new ArgumentOutOfRangeException(nameof(leftOrRight)),
    };
    result.turns++;
    return result;
}
public record Navigate(string Current, string LeftNode, string RightNode);