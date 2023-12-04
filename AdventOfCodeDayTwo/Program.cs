using System.Text.RegularExpressions;

const string filePath = "input.txt";
var lines = File.ReadAllText(filePath);

int PartOne(string input) 
{
    return input.Split("\n")
        .Select(line => Solve(line))
        .Where(game => game.Red <= 12 && game.Green <= 13 && game.Blue <= 14)
        .Sum(game => game.Id);
}

int PartTwo(string input)
{
    return input.Split("\n")
        .Select(line => Solve(line))
        .Sum(game => game.Red * game.Green * game.Blue);
}


Game Solve(string line) =>
    new Game(
        ParseInts(line, @"Game (\d+)").First(),
        ParseInts(line, @"(\d+) red").Max(),
        ParseInts(line, @"(\d+) green").Max(),
        ParseInts(line, @"(\d+) blue").Max()
    );

IEnumerable<int> ParseInts(string st, string rx) {
    return Regex.Matches(st, rx).Select(m => int.Parse(m.Groups[1].Value));
}

try
{
    var partOneSolution = PartOne(lines);
    var partTwoSolution = PartTwo(lines);
    Console.WriteLine(partOneSolution);
    Console.WriteLine(partTwoSolution);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

internal record Game(int Id, int Red, int Green, int Blue);
