 using System.Text.RegularExpressions;
 
 const string filePath = "input.txt";
 var lines = File.ReadAllText(filePath);

 int PartOne(string input) { 
     var rows = input.Split("\n");
     var symbols = Parse(rows, new Regex(@"[^.0-9]"));
     var nums = Parse(rows, new Regex(@"\d+"));

     return (
         from n in nums
         where symbols.Any(s => Adjacent(s, n))
         select n.Int
     ).Sum(); 
}

int PartTwo(string input) {
    var rows = input.Split("\n");
    var gears = Parse(rows, new Regex(@"\*"));
    var numbers = Parse(rows, new Regex(@"\d+"));

    return (
        from g in gears
        let neighbours = from n in numbers where Adjacent(n, g) select n.Int
        where neighbours.Count() == 2
        select neighbours.First() * neighbours.Last()
    ).Sum();
}

// https://stackoverflow.com/a/3269471.
bool Adjacent(Part p1, Part p2) =>
    Math.Abs(p2.Irow - p1.Irow) <= 1 &&
    p1.Icol <= p2.Icol + p2.Text.Length &&
    p2.Icol <= p1.Icol + p1.Text.Length;


Part[] Parse(string[] rows, Regex rx) => (
    from irow in Enumerable.Range(0, rows.Length)
    from match in rx.Matches(rows[irow])
    select new Part(match.Value, irow, match.Index)
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

record Part(string Text, int Irow, int Icol) {
    public int Int => int.Parse(Text);
}

