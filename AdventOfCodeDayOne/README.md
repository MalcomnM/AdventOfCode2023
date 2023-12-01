# ADVENT OF CODE 2023 - DAY ONE

## Overview
[ADVENT OF CODE 2023 - DAY ONE | Exercise](https://adventofcode.com/2023/day/1)

This solution is designed to decode a specific type of calibration document. The document contains lines of text that include numbers in both numeric and worded formats. The solution reads these lines, extracts the numbers, and calculates the sum of these numbers.

## How It Works
- The program reads a list of strings from a calibration document, where each string represents a line in the document.
- It extracts numbers from these lines. The extraction process supports both numeric (e.g., "1", "2") and worded numbers (e.g., "one", "two").
- After extracting the numbers, the program concatenates the first and last number in each line, converts them into a single integer, and then sums up these integers across all lines.

## Key Components
1. `DecodeCalibrationDocument`: Decodes the entire document.
2. `ExtractAndSumNumbers`: Extracts numbers from each line and sums them.
3. `BuildNumericRegexPattern`: Creates a regex pattern for number extraction.
4. `ConvertToNumber`: Converts string representations of numbers to integers.

## Proposed Improvements
This solution meets the requirements of this advent of code exercise, however, in an attempt to be a better developer. I've though through potential improvements for this solution.
- **Scalability Improvements**
  - **Decoding the Document:**: The complexity of regex matching can vary based on the regex pattern and the length of the line. In the worst case, it can be O(M*K), where M is the length of the line and K is the complexity of the regex pattern. However, for simple patterns and assuming the length of each line is relatively small and bounded, this can be approximated to O(M).
- **Extend Worded Number Support**: Include a more comprehensive set of worded numbers or implement a dynamic conversion method.
- **Enhanced Error Handling**: Improve error handling to manage different types of input and parsing errors more gracefully.
- **Comprehensive Testing**: Develop a suite of unit tests to ensure the solution works reliably under different scenarios and inputs. (One one likes to write them, but they can save your booty 😂)

## Usage
- Place the calibration document in a file named `calibration_document.txt`.
- The program reads this file and processes each line to decode the numbers.
- By default, the program only considers numeric digits. To include worded numbers, set the `convertWordedNumbers` flag to `true`.