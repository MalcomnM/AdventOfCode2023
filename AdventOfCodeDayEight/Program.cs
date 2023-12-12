using System.Text.RegularExpressions;
using Map = System.Collections.Generic.Dictionary<string, (string Left, string Right)>;

const string filePath = "input.txt";
var lines = File.ReadAllText(filePath);

long PartOne(string input) => Solve(input, "AAA", "ZZZ");
long PartTwo(string input) => Solve(input, "A", "Z");

long Solve(string input, string aMarker, string zMarker) {
    var blocks = input.Split("\n\n");
    var dirs = blocks[0];
    var map = ParseMap(blocks[1]);
    
    return map.Keys
        .Where(w => w.EndsWith(aMarker))
        .Select(w => StepsToZ(w, zMarker, dirs, map))
        .Aggregate(1L, Lcm);
}

long Lcm(long a, long b) => a * b / Gcd(a, b);
long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);

long StepsToZ(string current, string zMarker, string dirs, Map map) {
    var i = 0;
    while (!current.EndsWith(zMarker)) {
        var dir = dirs[i % dirs.Length];
        current =  dir == 'L' ? map[current].Left : map[current].Right;
        i++;
    }
    return i;
}

Map ParseMap(string input) =>
    input.Split("\n")
        .Select(line => Regex.Matches(line, "[A-Z]+"))
        .ToDictionary(m => m[0].Value, m => (m[1].Value, m[2].Value));

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