using Day03;

var currentDir = Directory.GetCurrentDirectory();
var dataFile = "data.txt";
var path = File.Exists(Path.Combine(currentDir, dataFile)) ? Path.Combine(currentDir, dataFile) : Path.Combine(currentDir, "Day01", dataFile);

var lines = File.ReadAllLines(path);

Schematic schematic = Schematic.Import(lines);

var parts = schematic.GetPartsList();
