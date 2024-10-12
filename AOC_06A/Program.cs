using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC6A
{
    /// <summary>
    /// As part of signing up, you get a sheet of paper (your puzzle input) that lists the time allowed for each race and also the best distance ever recorded in that race.
    /// To guarantee you win the grand prize, you need to make sure you go farther in each race than the current record holder.

    /// The organizer brings you over to the area where the boat races are held.
    /// The boats are much smaller than you expected - they're actually toy boats, each with a big button on top.
    /// Holding down the button charges the boat, and releasing the button allows the boat to move.
    /// Boats move faster if their button was held longer, but time spent holding the button counts against the total race time.
    /// You can only hold the button at the start of the race, and boats don't move until the button is released.

    /// For example:

    /// Time:      7  15   30
    /// Distance:  9  40  200
    /// This document describes three races:

    /// The first race lasts 7 milliseconds. The record distance in this race is 9 millimeters.
    /// The second race lasts 15 milliseconds. The record distance in this race is 40 millimeters.
    /// The third race lasts 30 milliseconds. The record distance in this race is 200 millimeters.
    /// Your toy boat has a starting speed of zero millimeters per millisecond.
    /// For each whole millisecond you spend at the beginning of the race holding down the button, the boat's speed increases by one millimeter per millisecond.

    /// So, because the first race lasts 7 milliseconds, you only have a few options:

    /// Don't hold the button at all (that is, hold it for 0 milliseconds) at the start of the race. The boat won't move; it will have traveled 0 millimeters by the end of the race.
    /// Hold the button for 1 millisecond at the start of the race. Then, the boat will travel at a speed of 1 millimeter per millisecond for 6 milliseconds, reaching a total distance traveled of 6 millimeters.
    /// Hold the button for 2 milliseconds, giving the boat a speed of 2 millimeters per millisecond. It will then get 5 milliseconds to move, reaching a total distance of 10 millimeters.
    /// Hold the button for 3 milliseconds. After its remaining 4 milliseconds of travel time, the boat will have gone 12 millimeters.
    /// Hold the button for 4 milliseconds. After its remaining 3 milliseconds of travel time, the boat will have gone 12 millimeters.
    /// Hold the button for 5 milliseconds, causing the boat to travel a total of 10 millimeters.
    /// Hold the button for 6 milliseconds, causing the boat to travel a total of 6 millimeters.
    /// Hold the button for 7 milliseconds. That's the entire duration of the race. You never let go of the button. The boat can't move until you let go of the button. Please make sure you let go of the button so the boat gets to move. 0 millimeters.
    /// Since the current record for this race is 9 millimeters, there are actually 4 different ways you could win: you could hold the button for 2, 3, 4, or 5 milliseconds at the start of the race.

    /// In the second race, you could hold the button for at least 4 milliseconds and at most 11 milliseconds and beat the record, a total of 8 different ways to win.

    /// In the third race, you could hold the button for at least 11 milliseconds and no more than 19 milliseconds and still beat the record, a total of 9 ways you could win.

    /// To see how much margin of error you have, determine the number of ways you can beat the record in each race; in this example, if you multiply these values together, you get 288 (4 * 8 * 9).

    /// Determine the number of ways you could beat the record in each race. What do you get if you multiply these numbers together?
    /// </summary>

    
    public class Solution
    {
        /// <summary>
        /// There are actual and hypothetical Races.
        /// For actual races we know the time and distance. It also has a number of hypothetical races with varying button pressed times.
        /// The hypotheticalRaces have a given time (which is the same as the actual race time) and the button pressed time (which depends on the varying number in the actual race.
        /// The distance is calculated from those numbers. 
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


            var times = regex.Matches(lines[0]).Select(match => int.Parse(match.Value)).ToList();
            var distances = regex.Matches(lines[1]).Select(match => int.Parse(match.Value)).ToList();

            if (times.Count != distances.Count)
            {
                Console.WriteLine("Invalid Numbers");
                return;
            }

            var winningRacesProduct = times
                .Zip(distances, (time, distance) => new Race(time, distance).NumberOfWinningHypotheticalRaces)
                .Aggregate(1, (accumulator, value) => accumulator * value);
            
            Console.WriteLine(winningRacesProduct);
            

        }

    }
    
    public class HypotheticalRace(int raceTime, int timeButtonPressed)
    {
        /// <summary>
        /// Determines the actual amount of time the race was long
        /// </summary>
        private int RaceTime { get; init; } = raceTime;

        /// <summary>
        /// The time the button was pressed to prepare the racing.
        /// Equivalent to boat speed. E.g. if Button was pressed 3 seconds - the boat will be 3mm/ms fast.
        /// </summary>
        private int TimeButtonPressed { get; init; } = timeButtonPressed;

        /// <summary>
        /// The Time in which the button was not pressed and the boat was racing 
        /// </summary>
        private int TimeRacing => RaceTime - TimeButtonPressed;

        /// <summary>
        /// The distance the boat traveled while racing.
        /// TimeButtonPressed(Speed) is multiplied by the actual racing time.
        /// </summary>
        public int Distance => TimeButtonPressed * TimeRacing;
    }
    

    /// <summary>
    /// The actual Race Class that contains all information about the actual Race and the number of hypothetical races that beat the actual race
    /// </summary>
    /// <param name="raceTime">How long the Race actually was</param>
    /// <param name="Distance">How far the boat actually travelled.</param>
    public class Race
    {
        private int RaceTime { get; init; }
        private int Distance { get; init; }

        public Race(int raceTime, int distance)
        {
            RaceTime = raceTime;
            Distance = distance;
        }

        /// <summary>
        /// All hypothetical races with a varying button pressed time from 1 to raceTime - 1 seconds (Distance is zero if button is pressed the full time. 
        /// </summary>
        private List<HypotheticalRace> HypotheticalRaces => Enumerable.Range(1, RaceTime - 1)
            .Select(buttonPressedTime => new HypotheticalRace(RaceTime, buttonPressedTime)).ToList();



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