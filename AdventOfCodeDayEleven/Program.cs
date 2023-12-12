using System.Text.RegularExpressions;
using Map = System.Collections.Generic.Dictionary<string, (string Left, string Right)>;

const string filePath = "input.txt";
var lines = File.ReadAllText(filePath);

long PartOne(string input) => Solve(input, 1);
long PartTwo(string input) => Solve(input, 999999);

long Solve(string input, int expansion) {
    var map = input.Split("\n");

    Func<int, bool> isRowEmpty = EmptyRows(map).ToHashSet().Contains;
    Func<int, bool> isColEmpty = EmptyCols(map).ToHashSet().Contains;

    var galaxies = FindAll(map, '#');
    return (
        from g1 in galaxies
        from g2 in galaxies
        select
            Distance(g1.irow, g2.irow, expansion, isRowEmpty) +
            Distance(g1.icol, g2.icol, expansion, isColEmpty)
    ).Sum() / 2;
}

long Distance(int i1, int i2, int expansion, Func<int, bool> isEmpty) {
    var a = Math.Min(i1, i2);
    var d = Math.Abs(i1 - i2);
    return d + expansion * Enumerable.Range(a, d).Count(isEmpty);
}

IEnumerable<int> EmptyRows(string[] map) =>
    from irow in Enumerable.Range(0, map.Length)
    where map[irow].All(ch => ch == '.')
    select irow;

IEnumerable<int> EmptyCols(string[] map) =>
    from icol in Enumerable.Range(0, map[0].Length)
    where map.All(row => row[icol] == '.')
    select icol;

IEnumerable<Position> FindAll(string[] map, char ch) =>
    from irow in Enumerable.Range(0, map.Length)
    from icol in Enumerable.Range(0, map[0].Length)
    where map[irow][icol] == ch
    select new Position(irow, icol);

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

record Position(int irow, int icol);