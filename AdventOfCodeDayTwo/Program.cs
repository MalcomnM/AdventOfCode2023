using System.Text.RegularExpressions;

const string filePath = "input.txt";
var lines = File.ReadAllText(filePath);

int PartOne(string input)
{
    return (from line in input.Split("\n")
            let game = Solve(line)
            where game.Red <= 12 && game.Green <= 13 && game.Blue <= 14
            select game.Id
        ).Sum();
}

int PartTwo(string input)
{
    return (from line in input.Split("\n")
            let game = Solve(line)
            select game.Red * game.Green * game.Blue).Sum();
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
