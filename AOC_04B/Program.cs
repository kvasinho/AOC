using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using Extensions;
using Console = System.Console;

namespace AOC4B
{
    /// <summary>
    /// There's no such thing as "points". Instead, scratchcards only cause you to win more scratchcards equal to the number of winning numbers you have.
    /// Specifically, you win copies of the scratchcards below the winning card equal to the number of matches.
    /// So, if card 10 were to have 5 matching numbers, you would win one copy each of cards 11, 12, 13, 14, and 15.
    /// Copies of scratchcards are scored like normal scratchcards and have the same card number as the card they copied.
    /// So, if you win a copy of card 10 and it has 5 matching numbers it would then win a copy of the same cards that the original card 10 won:
    /// cards 11, 12, 13, 14, and 15. This process repeats until none of the copies cause you to win any more cards.
    /// (Cards will never make you copy a card past the end of the table.)
    
    /// This time, the above example goes differently:

    /// Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    /// Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
    /// Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
    /// Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
    /// Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
    /// Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
    
    /// Card 1 has four matching numbers, so you win one copy each of the next four cards: cards 2, 3, 4, and 5.
    /// Your original card 2 has two matching numbers, so you win one copy each of cards 3 and 4.
    /// Your copy of card 2 also wins one copy each of cards 3 and 4.
    /// Your four instances of card 3 (one original and three copies) have two matching numbers, so you win four copies each of cards 4 and 5.
    /// Your eight instances of card 4 (one original and seven copies) have one matching number, so you win eight copies of card 5.
    /// Your fourteen instances of card 5 (one original and thirteen copies) have no matching numbers and win no more cards.
    /// Your one instance of card 6 (one original) has no matching numbers and wins no more cards.
    /// Once all of the originals and copies have been processed, you end up with 1 instance of card 1, 2 instances of card 2, 4 instances of card 3, 8 instances of card 4, 14 instances of card 5, and 1 instance of card 6. In total, this example pile of scratchcards causes you to ultimately have 30 scratchcards!

    /// Process all of the original and copied scratchcards until no more scratchcards are won. Including the original set of scratchcards, how many total scratchcards do you end up with?
    /// </summary>
    public class Solution
    {
        /// <summary>
        /// Reads the input file and split into 2 lists. Start with the first line and check winning numbers.
        /// Add counter for how many copies of each card exist in a map. Loop over them and calculate the sum.
        /// Repeat until either zero or the end is reached. 
        /// </summary>
        public void Solve()
        {
            var path = "./input.txt";
            var lines = File.ReadAllLines(path).ToList();

            var cardCounts = Enumerable.Range(1, lines.Count).ToDictionary(key => key, _ => 1);

            foreach (var line in lines)
            {
                var card = Card.FromString(line);
                var matchCount = card.Matches.Count;
                var id = card.Id;

                if (matchCount == 0) continue;

                var currentCardCount = cardCounts[id];

                for (int i = 1; i <= matchCount; i++)
                {
                    var nextCardId = id + i;
                    if (!cardCounts.ContainsKey(nextCardId)) break;

                    cardCounts[nextCardId] += currentCardCount;
                }
            }


            //second loop to itereate over dictionary and add all keys until the first is zero or end.
            var sum = cardCounts.Values.GetValuesUntil(0).Sum();

            
            
            

            Console.WriteLine(sum);

        }

    }

    public class Card
    {
        public int Id { get; set;  }
        public IEnumerable<int> PersonalNumbers { get; set; } = new List<int>();
        public IEnumerable<int> WinningNumbers { get; set; } = new List<int>();
        

        public List<int> Matches => PersonalNumbers.Intersect(WinningNumbers).ToList();

        /// <summary>
        /// Creates a card instance from an input string.
        /// </summary>
        /// <remarks>
        /// The input string must match this regex pattern:
        /// @"Card\s+(\d+):\s+([\d\s]+)\|\s+([\d\s]+)";
        /// </remarks>
        /// <param name="input">The string that is used to instantiate the object</param>
        /// <returns>A class instance of Card.</returns>
        /// <exception cref="Exception">Throws an exception if the string does not match the pattern. </exception>
        public static Card FromString(string input)
        {
            string pattern = @"Card\s+(\d+):\s+([\d\s]+)\|\s+([\d\s]+)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            if (match.Success)
            {
                var card = new Card
                {
                    Id = int.Parse(match.Groups[1].Value),
                    WinningNumbers = match.Groups[2].Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse),
                    PersonalNumbers = match.Groups[3].Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                };

                return card;
            }

            throw new Exception("Invalid Input format");
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