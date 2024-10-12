using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC7B
{
    /// <summary>
    /// To make things a little more interesting, the Elf introduces one additional rule. Now, J cards are jokers - wildcards that can act like whatever card would make the hand the strongest type possible.

    /// To balance this, J cards are now the weakest individual cards, weaker even than 2. The other cards stay in the same order: A, K, Q, T, 9, 8, 7, 6, 5, 4, 3, 2, J.
    /// J cards can pretend to be whatever card is best for the purpose of determining hand type; for example, QJJQ2 is now considered four of a kind. However, for the purpose of breaking ties between two hands of the same type, J is always treated as J, not the card it's pretending to be: JKKK2 is weaker than QQQQ2 because J is weaker than Q.

    /// Now, the above example goes very differently:

    /// 32T3K 765
    /// T55J5 684
    /// KK677 28
    /// KTJJT 220
    /// QQQJA 483
    /// 32T3K is still the only one pair; it doesn't contain any jokers, so its strength doesn't increase.
    /// KK677 is now the only two pair, making it the second-weakest hand.
    /// T55J5, KTJJT, and QQQJA are now all four of a kind! T55J5 gets rank 3, QQQJA gets rank 4, and KTJJT gets rank 5.
    /// With the new joker rule, the total winnings in this example are 5905.

    /// Using the new joker rule, find the rank of every hand in your set. What are the new total winnings?
    /// </summary>

    
    public class Solution
    {
        /// <summary>
        /// We need to apply some minor adjustmens to card and hand.
        /// The values in card are now reordered
        /// In hand, we need to find the best hand by converting the joker
        /// This is done by introducing a new field that tracks the number of Jokers in the hand and by adjusting the dictionary to only consider non jokers.
        /// Then, jokers are added to the highest counts.
        /// </summary>
        public void Solve()
        {
            var path = "./input.txt";
            var input = File.ReadAllLines(path);
            var rows = input.Select(line => Row.FromString(line.Trim())).ToList();
            var game = new Game(rows);
            
            Console.WriteLine(game.Result);
        }

    }

    /// <summary>
    /// Card maps a Label (e.g. King 'K' or '9' to a numeric value that can be compared more easily)
    /// </summary>
    public class Card : IEquatable<Card>, IComparable<Card>
    {
        /// <summary>
        /// The Card label
        /// </summary>
        private char Label { get; init; }

        /// <summary>
        /// The resulting numeric value.
        /// Throws an exception if the Label is invalid
        /// </summary>
        /// <exception cref="ArgumentException">An exception on invalid input</exception>
        public int Value
        {
            get
            {
                return Label switch
                {            
                    'J' => 1,
                    '2' => 2,
                    '3' => 3,
                    '4' => 4,
                    '5' => 5,
                    '6' => 6,
                    '7' => 7,
                    '8' => 8,
                    '9' => 9,
                    'T' => 10,
                    'Q' => 12,
                    'K' => 13,
                    'A' => 14, // Or 1, depending on the rules of your game
                    _ => throw new ArgumentException("Invalid card rank"),
                };
            }
        }

        public Card(char label)
        {
            Label = label;
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Card otherRank)
            {
                return Equals(otherRank);
            }
            return false;
        }

        public bool Equals(Card? other)
        {
            if (other == null) return false;
            return Value == other.Value;
        }

        public int CompareTo(Card? other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return Value.CompareTo(other.Value);
        }
    }

    /// <summary>
    /// Hand contains all logic to generate the hand values and whether its a pair, 2 pairs or something else.
    /// A dictionary with card counts facilitates the counting logic.
    /// Hand VAlue is a numeeric value that converts the actual value of the deck to a numeric (and comparable value)
    /// </summary>
    public class Hand
    {
        /// <summary>
        /// The input string of the hand
        /// </summary>
        private  string _cardString { get; init; }

        /// <summary>
        /// A list of Cards that converts the input string.
        /// </summary>
        private IEnumerable<Card> Cards => _cardString.Select(card => new Card(card));

        /// <summary>
        /// Tracks the number of jokers (value = 1) in the hand. 
        /// </summary>
        private int JokerCount => Cards.Count(card => card.Value == 1);

        /// <summary>
        /// The numeric card values as list
        /// </summary>
        public List<int> CardValues => Cards.Select(card => card.Value).ToList();
        
        /// <summary>
        /// A Dictionary that tracks how often each card occurs in the hand excluding jokers.
        /// </summary>
        private Dictionary<Card, int> _cardCounts
        {
            get
            {
                var cardCounts = new Dictionary<Card, int>();
                foreach (var card in Cards)
                {
                    if(card.Value == 1) continue;
                    
                    if (!cardCounts.TryAdd(card, 1))
                    {
                        cardCounts[card] += 1;
                    }
                }

                return cardCounts;
            }
        }

        public Hand(string cardString)
        {
            if (cardString.Count() != 5) throw new ArgumentException("invalid card count");
            _cardString = cardString;
        }
        /// <summary>
        /// Boolean Method to check whether the card is a five of a kind with additional jokers. 
        /// </summary>
        public bool Is5OfAKind {
            get
            {
                if (JokerCount == 5) return true;
                return _cardCounts.Values.Max() + JokerCount == 5;
            }}

        public bool Is4OfAKind => _cardCounts.Values.Max() + JokerCount >= 4;
        public bool Is3OfAKind => _cardCounts.Values.Max() + JokerCount >= 3;
        
        /// <summary>
        /// This method checks how many pairs we can form with jokers.
        /// </summary>
        private bool CanFormPairsWithJokers(int targetPairCount)
        {
            int pairs = 0;
            int jokersRemaining = JokerCount;

            foreach (var count in _cardCounts.Values)
            {
                if (count >= 2)
                {
                    pairs++;
                }
                else if (count == 1 && jokersRemaining > 0)
                {
                    pairs++;
                    jokersRemaining--;
                }

                if (pairs >= targetPairCount)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsTwoPairs => CanFormPairsWithJokers(2);

        public bool IsOnePair => CanFormPairsWithJokers(1);

        public bool IsFullHouse
        {
            get
            {
                var counts = _cardCounts.Values.OrderByDescending(c => c).ToList();
                return (counts[0] + JokerCount >= 3) && (counts[1] + Math.Max(0, JokerCount - (3 - counts[0])) >= 2);
            }
        }
        
        /// <summary>
        /// Convert the value of the hand to a numeric one.
        /// </summary>
        public int HandValue
        {
            get
            {
                if (Is5OfAKind) return 7;
                if (Is4OfAKind) return 6;
                if (IsFullHouse) return 5;
                if (Is3OfAKind) return 4;
                if (IsTwoPairs) return 3;
                if (IsOnePair) return 2;
                return 1;
            }
        }
        

        public override int GetHashCode()
        {
            int hash = HandValue;
            foreach (var cardValue in CardValues.OrderBy(v => v)) 
            {
                hash = hash * 31 + cardValue; 
            }
            return hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Hand otherHand)
            {
                return Equals(otherHand);
            }
            return false;
        }

        public bool Equals(Hand? other)
        {
            if (other is null) return false;
            return GetHashCode() == other.GetHashCode();
        }

        public override string ToString()
        {
            return $"Cards: {_cardString} Values:{string.Join(",", CardValues)} HandValue: {HandValue}";
        }
    }

    /// <summary>
    /// Contains all data from a single row (hand + bid)
    /// </summary>
    public class Row
    {
        public Hand Hand { get; init; }
        public int Bid { get; init; }

        public Row(Hand hand, int bid)
        {
            Hand = hand;
            Bid = bid;
        }

        public static Row FromString(string input)
        {
            var split = input.ToUpper().Trim().Split(" ");

            if (split.Length != 2 || !int.TryParse(split[1], out var result))
            {
                throw new Exception("Invalid format");
            }

            return new Row(new Hand(split[0].Trim()), result);
        }
    }

    /// <summary>
    /// Sorting logic for all rows in a game.
    /// Calculates the result after sorting.
    /// </summary>
    public class Game
    {
        public List<Row> Rows { get; init; }

        public Game(List<Row> rows)
        {
            Rows = rows;
        }

        public static Game FromInputLines(string[] lines)
        {
            var rows = lines.Select(line => Row.FromString(line)).ToList();

            return new Game(rows);
        }

        /// <summary>
        /// Sorts the input rows by its hand value (5 of a kind, 2 pairs etc) and then by the first to last card in the hand. 
        /// </summary>
        public List<Row> SortedRows => 
            Rows
                .OrderBy(row => row.Hand.HandValue)
                .ThenBy(row => row.Hand.CardValues[0])
                .ThenBy(row => row.Hand.CardValues[1]) 
                .ThenBy(row => row.Hand.CardValues[2]) 
                .ThenBy(row => row.Hand.CardValues[3]) 
                .ThenBy(row => row.Hand.CardValues[4]) 
                .ToList();


        /// <summary>
        /// Calculates the game score.
        /// </summary>
        public long Result => SortedRows.Select((row, index) => row.Bid * (index + 1)).Sum();
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