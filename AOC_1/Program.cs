using System;
using System.IO;


namespace AOC1 
{
    
    /// <summary>
    /// The newly-improved calibration document consists of lines of text; each line originally contained a specific
    /// calibration value that the Elves now need to recover. On each line, the calibration value can be found
    /// by combining the first digit and the last digit (in that order) to form a single two-digit number.

    ///For example:
        ///1abc2
        ///pqr3stu8vwx
        ///a1b2c3d4e5f
        ///treb7uchet
        
    ///In this example, the calibration values of these four lines are 12, 38, 15, and 77. Adding these together produces 142.

    ///Consider your entire calibration document. What is the sum of all of the calibration values?
    /// </summary>
    /// 
    public class Solution
    {
        
        /// <summary>
        /// Extracts all digit characters from a given string.
        /// </summary>
        /// <param name="line">The input string from which digits will be extracted.</param>
        /// <returns>A list of integers representing the digits found in the input string.</returns>
        /// <example>
        /// <code>
        /// var digits = solution.ExtractDigits("a1b2c3");
        /// // digits = { 1, 2, 3 }
        /// </code>
        /// </example>
        public List<int> ExtractDigits(string line)
        {
            List<int> digits = new();
            foreach (var c in line)
            {
                if (Char.IsDigit(c))
                {
                    digits.Add((int)Char.GetNumericValue(c));
                }
            }
            return digits;
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
                sum += GetFromFirstAndLastDigit(digits);
            }
            
            Console.WriteLine(sum);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var solution = new Solution();
            solution.Solve();
        }
        
    }
}