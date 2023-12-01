// Day 1: Trebuchet?! PART ONE AND TWO SOLUTION |  https://adventofcode.com/2023/day/1

using System.Text.RegularExpressions;

const string filePath = "calibration_document.txt";
var lines = File.ReadAllText(filePath);

int PartOne(string input)
{
    return Solve(input, @"\d");
}

int PartTwo(string input)
{
    return Solve(input, @"\d|one|two|three|four|five|six|seven|eight|nine");
}

int Solve(string input, string rx)
{
    return (from line in input.Split("\n")
        let first = Regex.Match(line, rx)
        let last = Regex.Match(line, rx, RegexOptions.RightToLeft)
        select ParseMatch(first.Value) * 10 + ParseMatch(last.Value)).Sum();
}

static int ParseMatch(string st) => st switch {
    "one" => 1,
    "two" => 2,
    "three" => 3,
    "four" => 4,
    "five" => 5,
    "six" => 6,
    "seven" => 7,
    "eight" => 8,
    "nine" => 9,
    var d => int.Parse(d)
};

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
