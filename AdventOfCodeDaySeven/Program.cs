using System.Text.RegularExpressions;

const string filePath = "input.txt";
var lines = File.ReadAllText(filePath);

long PartOne(string input) => Solve(input, Part1Points);
long PartTwo(string input) => Solve(input, Part2Points);

(long, long) Part1Points(string hand) => (PatternValue(hand), CardValue(hand, "123456789TJQKA"));
(long, long) Part2Points(string hand) {
    var cards = "J123456789TQKA";
    var patternValue = cards.Select(ch => PatternValue(hand.Replace('J', ch))).Max();
    return (patternValue, CardValue(hand, cards));
}

long CardValue(string hand, string cardOrder) => Pack(hand.Select(card => cardOrder.IndexOf(card)));

long PatternValue(string hand) => Pack(hand.Select(card => hand.Count(x => x == card)).OrderDescending());

long Pack(IEnumerable<int> numbers) => numbers.Aggregate(1L, (a, v) => (a * 256) + v);

int Solve(string input, Func<string, (long, long)> getPoints) {
    var bidsByRanking = (
        from line in input.Split("\n")
        let hand = line.Split(" ")[0]
        let bid = int.Parse(line.Split(" ")[1])
        orderby getPoints(hand)
        select bid
    );

    return bidsByRanking.Select((bid, rank) => (rank + 1) * bid).Sum();
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