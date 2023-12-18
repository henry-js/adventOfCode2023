
using System.Diagnostics;
using System.Xml.Schema;

var currentDir = Directory.GetCurrentDirectory();
var dataFile = "data.txt";
var path = File.Exists(Path.Combine(currentDir, dataFile)) ? Path.Combine(currentDir, dataFile) : Path.Combine(currentDir, "Day01", dataFile);

var inputLines = File.ReadAllLines(path);

var total = inputLines.Select(FindAllNumbers).Select(numbers =>
{
    var sum = numbers.First().value.ToString() + numbers.Last().value.ToString();
    Debug.WriteLine($"Original: {numbers.First().original}, Count: {numbers.Count()}, First: {numbers.First().value}, Last: {numbers.Last().value}, Sum: {sum}");
    return int.Parse(sum);
});
Console.WriteLine(total.Sum());

IEnumerable<(string original, int value)> FindAllNumbers(string arg1)
{
    string[] stringDigits = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
    List<(string, int)> result = [];
    for (int i = 0; i < arg1.Length; i++)
    {
        char c = arg1[i];
        if (char.IsDigit(c))
        {
            result.Add((arg1, int.Parse(c.ToString())));
        }
        else
        {
            foreach (var digit in stringDigits)
            {
                var endIndex = i + digit.Length;
                if (endIndex > arg1.Length) continue;
                if (arg1[i..endIndex] == digit)
                {
                    result.Add((arg1, ParseStringDigit(digit)));
                }
            }
        }
    }
    return result;
}

int ParseStringDigit(string digit) => digit switch
{
    "one" => 1,
    "two" => 2,
    "three" => 3,
    "four" => 4,
    "five" => 5,
    "six" => 6,
    "seven" => 7,
    "eight" => 8,
    "nine" => 9,
    _ => 0
};