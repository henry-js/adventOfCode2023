using Day07;
using Spectre.Console;
using SpectreHelpers;

var currentDir = Directory.GetCurrentDirectory();
var dataFile = "data.txt";
var path = File.Exists(Path.Combine(currentDir, dataFile)) ? Path.Combine(currentDir, dataFile) : Path.Combine(currentDir, "Day01", dataFile);

var inputLines = File.ReadAllLines(path);
var hands = inputLines.Select(line => line.Split(' ', StringSplitOptions.TrimEntries))
                      .Select(arr => new Hand(arr[0], int.Parse(arr[1])))
                      .OrderBy(hand => hand.Type);

var flattenedGroupHands = hands
                    .GroupBy(h => h.Type)
                    .SelectMany(x => x)
                    .OrderBy(x => x.Type)
                    .ThenBy(x => x.Cards, new HandComparer());
flattenedGroupHands = ApplyRanks(flattenedGroupHands);
AnsiConsole.Write(flattenedGroupHands.ToTable());
// var totalRanks = ApplyRanks(groupedHands.SelectMany(x => x).Reverse());
// AnsiConsole.Write(totalRanks.ToTable());


AnsiConsole.WriteLine($"Total Winnings: {flattenedGroupHands.Sum(hand => hand.Winnings)}");



IOrderedEnumerable<Hand> ApplyRanks(IOrderedEnumerable<Hand> handList)
{
    var listArray = handList.Reverse().ToArray();
    var rank = 1;
    for (int i = listArray.Length - 1; i >= 0; i--)
    {
        listArray[i] = listArray[i] with { Rank = rank, Winnings = rank * listArray[i].Bid };
        rank++;
    }
    return listArray.OrderByDescending(x => x.Rank);
}

Console.ReadLine();


public class HandComparer : IComparer<char[]>
{
    public int Compare(char[]? x, char[]? y)
    {
        if (x is null) return -1;
        if (y is null) return 1;


        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] == y[i]) continue;
            if (RankOf(x[i]) > RankOf(y[i])) return 1;
            else return -1;
        }

        return 0;
    }

    private int RankOf(char v) => v switch
    {
        '1' => 1,
        '2' => 2,
        '3' => 3,
        '4' => 4,
        '5' => 5,
        '6' => 6,
        '7' => 7,
        '8' => 8,
        '9' => 9,
        'T' => 10,
        'J' => 11,
        'Q' => 12,
        'K' => 13,
        'A' => 14,
        _ => 0
    };
}