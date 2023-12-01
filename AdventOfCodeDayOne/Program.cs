// Day 1: Trebuchet?! |  https://adventofcode.com/2023/day/1
const string filePath = "calibration_document.txt";
var lines = File.ReadAllLines(filePath).ToList();

static int DecodeCalibrationDocument(List<string> calibrationDocumentLines)
{
    if (calibrationDocumentLines?.Any() != true)
    {
        throw new ArgumentException("Calibration document lines are null or empty.");
    }
    
    var numbers = new List<int>();
    foreach (var line in calibrationDocumentLines)
    {
        var digits = line.Where(char.IsDigit).ToList();
        if (digits.Count == 0) continue;
        
        var firstDigit = digits.First();
        var lastDigit = digits.Last();
        var number = int.Parse($"{firstDigit}{lastDigit}");
        numbers.Add(number);
    }

    return numbers.Sum();
}

try
{
    var calibrationValue = DecodeCalibrationDocument(lines);
    Console.WriteLine(calibrationValue);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
