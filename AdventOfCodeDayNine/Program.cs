using System.Text.RegularExpressions;

const string filePath = "input.txt";
var lines = File.ReadAllText(filePath);

long PartOne(string input) => Solve(input, ExtrapolateRight);
long PartTwo(string input) => Solve(input, ExtrapolateLeft);

long Solve(string input, Func<long[], long> extrapolate) =>
    input.Split("\n").Select(ParseNumbers).Select(extrapolate).Sum();

long[] ParseNumbers(string line) =>
    line.Split(" ").Select(long.Parse).ToArray();

long[] Diff(long[] numbers) =>
    numbers.Zip(numbers.Skip(1)).Select(p => p.Second - p.First).ToArray();

long ExtrapolateRight(long[] numbers) =>
    !numbers.Any() ? 0 : ExtrapolateRight(Diff(numbers)) + numbers.Last();

long ExtrapolateLeft(long[] numbers) =>
    ExtrapolateRight(numbers.Reverse().ToArray());

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