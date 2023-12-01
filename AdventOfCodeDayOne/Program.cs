// Day 1: Trebuchet?! PART ONE AND TWO SOLUTION |  https://adventofcode.com/2023/day/1

using System.Text.RegularExpressions;

const string filePath = "calibration_document.txt";
var lines = File.ReadAllLines(filePath).ToList();

static int DecodeCalibrationDocument(List<string> calibrationDocumentLines, bool convertWordedNumbers = false)
{
    if (calibrationDocumentLines?.Any() != true) 
        throw new ArgumentException("Calibration document lines are null or empty.");

    return ExtractAndSumNumbers(calibrationDocumentLines, convertWordedNumbers);
}

static int ExtractAndSumNumbers(List<string> lines, bool convertWordedNumbers = false)
{
    var numbers = new List<int>();
    foreach (var line in lines)
    {
        var pattern = BuildNumericRegexPattern(convertWordedNumbers);
        
        var lineNumbers = Regex.Matches(line, pattern)
            .Select(match => ConvertToNumber(match.Groups[1].Value))
            .ToList();
        
        if (lineNumbers.Count == 0) continue;
        
        var number = int.Parse($"{lineNumbers.First()}{lineNumbers.Last()}");
        numbers.Add(number);
    }

    return numbers.Sum();
}

static string BuildNumericRegexPattern(bool includeWordedDigits)
{
    return includeWordedDigits ? 
        @"(?=(one|two|three|four|five|six|seven|eight|nine|1|2|3|4|5|6|7|8|9))" :
        @"(?=(1|2|3|4|5|6|7|8|9))";
}

static int ConvertToNumber(string value)
{
    var digitDictionary = new Dictionary<string, int>
    {
        { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 },
        { "five", 5 }, { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 }
    };
    
    if (int.TryParse(value, out var number))
        return number;
    
    return digitDictionary.TryGetValue(value, out number) ? number : 0;
}

try
{
    var calibrationValue = DecodeCalibrationDocument(lines, false);
    Console.WriteLine(calibrationValue);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
