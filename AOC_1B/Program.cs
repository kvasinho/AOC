using System;
using System.IO;

namespace AOC1B
{
    
    /// <summary>
    ///Your calculation isn't quite right.
    /// It looks like some of the digits are actually spelled out with letters:
    /// one, two, three, four, five, six, seven, eight, and nine also count as valid "digits".
    ///Equipped with this new information, you now need to find the real first and last digit on each line. For example:

    ///two1nine
    /// eightwothree
    /// abcone2threexyz
    ///xtwone3four
    ///4nineeightseven2
    ///zoneight234
    ///7pqrstsixteen
        
    ///In this example, the calibration values are 29, 83, 13, 24, 42, 14, and 76. Adding these together produces 281.
    /// </summary>
    /// 
    public class Solution
    {
        static Dictionary<string,int> SpelledDigits = new Dictionary<string, int>()
        {
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9}
        };
        
        /// <summary>
        /// Extracts all digit characters from a given string. Includes spelled out digits
        /// </summary>
        /// <param name="line">The input string from which digits will be extracted.</param>
        /// <returns>A list of integers representing the digits found in the input string.</returns>
        /// <remarks>This method slightly differs from the first challenge in that also spelled out digits are parsed</remarks>
        /// <example>
        /// <code>
        /// var digits = solution.ExtractDigits("eightwothree");
        /// // digits = { 8, 2, 3 }
        /// </code>
        /// </example>
        public List<int> ExtractDigits(string line)
        {
            List<int> digits = new();
    
            for (int i = 0; i < line.Length; i++)
            {
                if (Char.IsDigit(line[i]))
                {
                    digits.Add((int)Char.GetNumericValue(line[i]));
                    continue;
                }

                var substring = line.Substring(i);
                var digitMatch = GetDigitMatch(substring);
        
                if (SpelledDigits.TryGetValue(digitMatch, out int result))
                {
                    digits.Add(result);
                }
            }
            return digits;
        }
        
        /// <summary>
        /// Checks the current Substring for any digit matches.
        /// </summary>
        /// <param name="substring">The (sub)string in which any digit should be found.</param>
        /// <returns>A string containing the digit or an empty string is none was found.</returns>
        /// <example>
        /// <code>
        /// var digitMatch = solution.GetDigitMatch( "eightynine");
        /// // isDigitMatch = "eight"
        /// </code>
        /// </example>
        public string GetDigitMatch(string substring)
        {
            var digitMatch = SpelledDigits.Keys.FirstOrDefault(digit =>
                substring.StartsWith(digit, StringComparison.OrdinalIgnoreCase));

            return digitMatch ?? string.Empty;
        }
        
        /// <summary>
        /// Constructs an integer by combining the first and last digit in the list.
        /// </summary>
        /// <param name="digits">A list of integers from which the first and last elements will be used.</param>
        /// <returns>An integer formed by concatenating the first and last digits.</returns>
        /// <exception cref="Exception">Thrown when the list is empty or the digits cannot be parsed into an integer.</exception>
        /// <example>
        /// <code>
        /// var result = solution.GetFromFirstAndLastDigit(new List&lt;int&gt; { 1, 4, 9 });
        /// // result = 19
        /// </code>
        /// </example>
        public int GetFromFirstAndLastDigit(List<int> digits)
        {
            if (!digits.Any())
            {
                throw new Exception("Digits cannot be emtpty");
            }

            if (int.TryParse($"{digits.First()}{digits.Last()}", out int result))
            {
                return result;
            }

            throw new Exception("Could not parse the digits");
        }
        
        /// <summary>
        /// Reads the input file, extracts digits from each line, and calculates the sum based on the first and last digits.
        /// </summary>
        public void Solve()
        {
            var path = "./input.txt";
            string[] lines = File.ReadAllLines(path);

            int sum = 0;

            foreach (var line in lines)
            {
                var digits = ExtractDigits(line);
                var number = GetFromFirstAndLastDigit(digits);
                sum += number;
                Console.WriteLine($"Number: {number} List: {string.Join(", ", digits)} String: {line}");
            }
            
            Console.WriteLine(sum);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\n\n");
            var solution = new Solution();
            solution.Solve();
        }
        
    }
}