using System.Diagnostics;

const string filePath = "input.txt";
var input = File.ReadAllLines(filePath);

List<long> ParseSeeds(string[] input)
{
    return input[0].Split(' ').Skip(1).Select(long.Parse).ToList();
}

List<List<(long from, long to, long adjustment)>> ParseMaps(string[] input)
{
    var maps = new List<List<(long from, long to, long adjustment)>>();
    List<(long from, long to, long adjustment)>? currMap = null;

    foreach (var line in input.Skip(2))
    {
        if (line.EndsWith(':'))
        {
            currMap = new List<(long from, long to, long adjustment)>();
            continue;
        }
        else if (line.Length == 0 && currMap != null)
        {
            maps.Add(currMap);
            currMap = null;
            continue;
        }

        var nums = line.Split(' ').Select(long.Parse).ToArray();
        currMap?.Add((nums[1], nums[1] + nums[2] - 1, nums[0] - nums[1]));
    }

    if (currMap != null)
    {
        maps.Add(currMap);   
    }

    return maps;
}

IEnumerable<(long from, long to)> ApplyAdjustments((long from, long to) range, List<(long from, long to, long adjustment)> orderedMap)
{
    foreach (var mapping in orderedMap)
    {
        if (range.from < mapping.from)
        {
            yield return (range.from, Math.Min(range.to, mapping.from - 1));
            range.from = mapping.from;
        }

        if (range.from <= mapping.to)
        {
            yield return (range.from + mapping.adjustment, Math.Min(range.to, mapping.to) + mapping.adjustment);
            range.from = mapping.to + 1;
        }

        if (range.from > range.to)
        {
            break;
        }
    }

    if (range.from <= range.to)
    {
        yield return range;
    }
}

long PartOne(List<long> seeds, List<List<(long from, long to, long adjustment)>> maps)
{
    return seeds.Select(seed => 
    {
        foreach (var map in maps)
        {
            var adjustment = map.FirstOrDefault(item => seed >= item.from && seed <= item.to).adjustment;
            if (adjustment != 0) seed += adjustment;
        }
        return seed;
    }).Min();
}

long PartTwo(List<long> seeds, List<List<(long from, long to, long adjustment)>> maps)
{
    var ranges = Enumerable.Range(0, seeds.Count / 2)
        .Select(i => (from: seeds[i * 2], to: seeds[i * 2] + seeds[i * 2 + 1] - 1))
        .ToList();

    foreach (var map in maps)
    {
        var orderedMap = map.OrderBy(x => x.from).ToList();
        ranges = ranges.SelectMany(range => ApplyAdjustments(range, orderedMap)).ToList();
    }

    return ranges.Min(r => r.from);
}

try
{
    var seeds = ParseSeeds(input);
    var maps = ParseMaps(input);

    var partOneSolution = PartOne(seeds, maps);
    var partTwoSolution = PartTwo(seeds, maps);

    Console.WriteLine($"Part One: {partOneSolution}");
    Console.WriteLine($"Part Two:  {partTwoSolution}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
