using System.Text.RegularExpressions;

const string filePath = "input.txt";
var lines = File.ReadAllText(filePath);

long PartOne(string input) => Solve(input);
long PartTwo(string input) => Solve(input.Replace(" ", ""));

long Solve(string input) {
    var rows = input.Split("\n");
    var times = Parse(rows[0]);
    var records = Parse(rows[1]);

    var res = 1L;
    for (var i = 0; i < times.Length; i++) {
        res *= WinningMoves(times[i], records[i]);
    }
    return res;
}

long WinningMoves(long time, long record) {
    var (x1, x2) = SolveEq(-1, time, -record);
    var maxX = (long)Math.Ceiling(x2) - 1;
    var minX = (long)Math.Floor(x1) + 1;
    return maxX - minX + 1; 
}

(double, double) SolveEq(long a, long b, long c) {
    var d = Math.Sqrt(b * b - 4 * a * c);
    var x1 = (-b - d) / (2 * a);
    var x2 = (-b + d) / (2 * a);
    return (Math.Min(x1, x2), Math.Max(x1, x2));
}

long[] Parse(string input) => (
    from m in Regex.Matches(input, @"\d+")
    select long.Parse(m.Value)
).ToArray();


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