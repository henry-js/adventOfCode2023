var currentDir = Directory.GetCurrentDirectory();
var dataFile = "data.txt";
var path = File.Exists(Path.Combine(currentDir, dataFile))
               ? Path.Combine(currentDir, dataFile)
               : Path.Combine(currentDir, "Day01", dataFile);
var lines = File.ReadAllLines(path);
List<Game> games = [];
foreach (var line in lines)
{
    games.Add(ParseToGame(line));
}

int maxRed = 12, maxGreen = 13, maxBlue = 14;

var possibleGames = games.Where(game =>
{
    return !game.Sets.Any(s => s.Red > maxRed || s.Blue > maxBlue || s.Green > maxGreen);
}).ToList();
Console.WriteLine($"Sum of allowed game ids: {possibleGames.Sum(g => g.Id)}");

int totalSum = 0;
foreach (var game in games)
{
    var minimumRed = game.Sets.Max(s => s.Red);
    var minimumGreen = game.Sets.Max(s => s.Green);
    var minimumBlue = game.Sets.Max(s => s.Blue);
    var power = minimumRed * minimumGreen * minimumBlue;
    AnsiConsole.WriteLine($"Power of Game {game.Id} = Red: {minimumRed} * Green {minimumGreen} * Blue {minimumBlue} = {power}");
    totalSum += power;
}

AnsiConsole.WriteLine($"Total: {totalSum}");
Game ParseToGame(string line)
{
    var idPart = line.Split(':')[0];
    var gamePart = line.Split(':')[1];
    var sets = ParseSets(gamePart);

    return new Game
    {
        Id = ParseId(line.Split(':')[0]),
        Sets = ParseSets(line.Split(':')[1])
    };
}

int ParseId(string idPart)
{
    var intString = idPart.Remove(0, 5);
    var id = int.Parse(intString);
    return id;
}

Set[] ParseSets(string setsPart)
{
    var setsSplit = setsPart.Split(';', StringSplitOptions.TrimEntries);
    var sets = setsSplit.Select(ss => ParseSet(ss));
    return sets.ToArray();
}
Set ParseSet(string setString)
{
    int red = 0, blue = 0, green = 0;
    string[] cubes = setString.Split(',');
    foreach (var cube in cubes)
    {
        var pair = cube.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        switch (pair[1])
        {
            case "red":
                red = int.Parse(pair[0]);
                continue;
            case "blue":
                blue = int.Parse(pair[0]);
                continue;
            case "green":
                green = int.Parse(pair[0]);
                continue;
        }
    }
    return new Set { Red = red, Blue = blue, Green = green };
}