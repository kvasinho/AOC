using System;
using System.Collections;
using System.IO;
using Extensions;

namespace AOC3A
{
    /// <summary>
    /// The missing part wasn't the only issue - one of the gears in the engine is wrong.
    /// A gear is any * symbol that is adjacent to exactly two part numbers.
    /// Its gear ratio is the result of multiplying those two numbers together.

    /// This time, you need to find the gear ratio of every gear and add them all up so that the engineer can figure out which gear needs to be replaced.

    /// Consider the same engine schematic again:
    
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

    /// In this schematic, there are two gears. The first is in the top left; it has part numbers 467 and 35, so its gear ratio is 16345.
    /// The second gear is in the lower right; its gear ratio is 451490.
    /// (The * adjacent to 617 is not a gear because it is only adjacent to one part number.) Adding up all of the gear ratios produces 467835.

    /// What is the sum of all of the gear ratios in your engine schematic?
    
    /// </summary>
    public class Solution
    {
        /// <summary>
        /// Reads the input file, finds any *   and finds the adjacent numbers.
        /// The ratio is then calculated of those 2.
        ///
        /// Get the dimensions (rows + cols) and then find symbols. 
        /// If a * is found we check whether 2 numbers exist adjacent.
        /// If so, we replace and calculate raio
        /// Add the ratio to the sum
        /// </summary>
        public void Solve()
        {
            var path = "./input.txt";
            var lines = File.ReadAllLines(path).ToList();

            int sum = 0;

            Matrix matrix = Matrix.FromLines(lines);
            foreach (var field in matrix.OrderedFields)
            {
                //find all adjacent fields. skip if its not a * . 
                if (!field.IsStar) continue;

                var adjacentFields = matrix.GetAdjacentFields(field);

                var adjacentNumbers = new List<int>();
                foreach (var adjacentField in adjacentFields)
                {
                    //consider only the digits.
                    if (!adjacentField.IsDigit) continue;

                    var numberFields = matrix.GetNumberFields(adjacentField);

                    //extract the number -> add it 
                    int number = Field.ToNumber(numberFields);
                    
                    adjacentNumbers.Add(number);

                    
                    //set to "."
                    numberFields.ForEach(numberField => numberField.MarkAsDone());

                    //sum += number;
                }

                if (adjacentNumbers.Count == 2)
                {
                    sum += (adjacentNumbers[0] * adjacentNumbers[1]);
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