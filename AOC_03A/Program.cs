using System;
using System.Collections;
using System.IO;
using Extensions;

namespace AOC3A
{
    /// <summary>
    /// The engineer explains that an engine part seems to be missing from the engine,
    /// but nobody can figure out which one. If you can add up all the part numbers in the engine schematic,
    /// it should be easy to work out which part is missing.

    /// The engine schematic (your puzzle input) consists of a visual representation of the engine.
    /// There are lots of numbers and symbols you don't really understand, but apparently any number adjacent to a symbol,
    /// even diagonally, is a "part number" and should be included in your sum. (Periods (.) do not count as a symbol.)

    /// Here is an example engine schematic:

    /// 467..114..
    /// ...*......
    /// ..35..633.
    /// ......#...
    /// 617*......
    /// .....+.58.
    /// ..592.....
    /// ......755.
    /// ...$.*....
    /// .664.598..

    /// In this schematic, two numbers are not part numbers because
    /// they are not adjacent to a symbol: 114 (top right) and 58 (middle right).
    /// Every other number is adjacent to a symbol and so is a part number; their sum is 4361.

    /// Of course, the actual engine schematic is much larger. What is the sum of all of the part numbers in the engine schematic?
    /// </summary>
    public class Solution
    {
        /// <summary>
        /// Reads the input file, finds any symbol except "." and finds the adjacent numbers.
        /// Numbers with more than 1 adjacent symbol are still only counted once. 
        /// The power is then summarized.
        ///
        /// Get the dimensions (rows + cols) and then find symbols. 
        /// If a symbol is found, we want to check adjacent tiles (using dimensions).
        /// If anything is a digit -> extract full number and replace with dots. 
        /// Add number to sum. 
        /// </summary>
        public void Solve()
        {
            var path = "./input.txt";
            var lines = File.ReadAllLines(path).ToList();

            int sum = 0;

            Matrix matrix = Matrix.FromLines(lines);
            foreach (var field in matrix.OrderedFields)
            {
                //find all adjacent fields. skip if its not a symbol
                if (!field.IsSymbol) continue;

                var adjacentFields = matrix.GetAdjacentFields(field);

                foreach (var adjacentField in adjacentFields)
                {
                    //find the full number, extract it and convert it.
                    if (!adjacentField.IsDigit) continue;

                    var numberFields = matrix.GetNumberFields(adjacentField);

                    //extract the number -> add it 
                    int number = Field.ToNumber(numberFields);


                    //set to "."
                    numberFields.ForEach(numberField => numberField.MarkAsDone());

                    sum += number;
                }
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