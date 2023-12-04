using System.Text.RegularExpressions;
 
const string filePath = "input.txt";
var lines = File.ReadAllText(filePath);

double PartOne(string input)
{
    return (from line in input.Split("\n")
        let card = ParseCard(line)
        where card.matches > 0
        select Math.Pow(2, card.matches - 1)).Sum();
}


double PartTwo(string input) {
    var cards = input.Split("\n").Select(ParseCard).ToArray();
    var counts = cards.Select(_ => 1).ToArray();

    for (var i = 0; i < cards.Length; i++) {
        var (card, count) = (cards[i], counts[i]);
        for (var j = 0; j < card.matches; j++) {
            counts[i + j + 1] += count;
        }
    }
    return counts.Sum();
}

Card ParseCard(string line) {
    var parts = line.Split(':', '|');
    var l = from m in Regex.Matches(parts[1], @"\d+") select m.Value;
    var r = from m in Regex.Matches(parts[2], @"\d+") select m.Value;
    return new Card(l.Intersect(r).Count());
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

record Card(int matches);