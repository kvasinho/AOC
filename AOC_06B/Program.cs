using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC6B
{
    /// <summary>
    /// --- Part Two ---
    /// As the race is about to start, you realize the piece of paper with race times and record distances you got earlier actually just has very bad kerning. There's really only one race - ignore the spaces between the numbers on each line.

    ///     So, the example from before:

    /// Time:      7  15   30
    /// Distance:  9  40  200
    ///     ...now instead means this:

    /// Time:      71530
    /// Distance:  940200
    /// Now, you have to figure out how many ways there are to win this single race. In this example, the race lasts for 71530 milliseconds and the record distance you need to beat is 940200 millimeters. You could hold the button anywhere from 14 to 71516 milliseconds and beat the record, a total of 71503 ways!

    /// How many ways can you beat the record in this one much longer race?
        
    /// </summary>

    
    public class Solution
    {
        /// <summary>
        /// There are basically no changes compared to Part 1. We also change the int to long since  there are some overflows with only ints
        /// THe only adjustment we are doing is changing the parsing to only create a single race rather than multiple
        /// </summary>
        public void Solve()
        {
            var path = "./input.txt";
            var lines = File.ReadAllLines(path).ToList();
            
            string pattern = @"\d+";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(lines[0]) || !regex.IsMatch(lines[0]))
            {
                Console.WriteLine("Invlalid pattern");
                return;
            }
            
            var time = long.Parse(string.Join("", regex.Matches(lines[0]).Select(match => match.Value)));
            var distance = long.Parse(string.Join("",regex.Matches(lines[1]).Select(match => match.Value)));
            var race = new Race(time, distance);
            
            Console.WriteLine(race.NumberOfWinningHypotheticalRaces);

        }

    }
    
    /// <summary>
    /// Contains the logic for  a hypothetical Race.
    /// The actual distance has to be calculated in a race based on a tiem limit (which is set by the actual race) and the time that was used to press a button. 
    /// </summary>
    /// <param name="raceTime">The duration of the actual race.</param>
    /// <param name="timeButtonPressed">The duration a button was pressed to accelerate before start.</param>
    public class HypotheticalRace(int raceTime, int timeButtonPressed)
    {
        /// <summary>
        /// Determines the actual amount of time the race was long
        /// </summary>
        private long RaceTime { get; init; } = raceTime;

        /// <summary>
        /// The time the button was pressed to prepare the racing.
        /// Equivalent to boat speed. E.g. if Button was pressed 3 seconds - the boat will be 3mm/ms fast.
        /// </summary>
        private long TimeButtonPressed { get; init; } = timeButtonPressed;

        /// <summary>
        /// The Time in which the button was not pressed and the boat was racing 
        /// </summary>
        private long TimeRacing => RaceTime - TimeButtonPressed;

        /// <summary>
        /// The distance the boat traveled while racing.
        /// TimeButtonPressed(Speed) is multiplied by the actual racing time.
        /// </summary>
        public long Distance => TimeButtonPressed * TimeRacing;
    }
    

    /// <summary>
    /// The actual Race Class that contains all information about the actual Race and the number of hypothetical races that beat the actual race
    /// </summary>
    /// <param name="raceTime">How long the Race actually was</param>
    /// <param name="Distance">How far the boat actually travelled.</param>
    public class Race
    {
        /// <summary>
        /// The actual race time 
        /// </summary>
        private long RaceTime { get; init; }
        /// <summary>
        /// The distance of the actual race.
        /// </summary>
        private long Distance { get; init; }

        public Race(long raceTime, long distance)
        {
            RaceTime = raceTime;
            Distance = distance;
        }

        /// <summary>
        /// All hypothetical races with a varying button pressed time from 1 to raceTime - 1 seconds (Distance is zero if button is pressed the full time. 
        /// </summary>
        private List<HypotheticalRace> HypotheticalRaces => Enumerable.Range(1, (int)RaceTime - 1)
            .Select(buttonPressedTime => new HypotheticalRace((int)RaceTime, buttonPressedTime)).ToList();

        
        /// <summary>
        /// The number of hypiothetical Races what would win against the actual Race
        /// </summary>
        public int NumberOfWinningHypotheticalRaces => HypotheticalRaces.Count(race => race.Distance > Distance);
        
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