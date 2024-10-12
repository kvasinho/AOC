using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC7A
{
    /// <summary>
    /// Because the journey will take a few days, she offers to teach you the game of Camel Cards.
    /// Camel Cards is sort of similar to poker except it's designed to be easier to play while riding a camel.

    /// In Camel Cards, you get a list of hands, and your goal is to order them based on the strength of each hand.
    /// A hand consists of five cards labeled one of A, K, Q, J, T, 9, 8, 7, 6, 5, 4, 3, or 2.
    /// The relative strength of each card follows this order, where A is the highest and 2 is the lowest.

    /// Every hand is exactly one type. From strongest to weakest, they are:

    /// Five of a kind, where all five cards have the same label: AAAAA
    /// Four of a kind, where four cards have the same label and one card has a different label: AA8AA
    /// Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
    /// Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
    /// Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
    /// One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
    /// High card, where all cards' labels are distinct: 23456
    /// Hands are primarily ordered based on type; for example, every full house is stronger than any three of a kind.

    /// If two hands have the same type, a second ordering rule takes effect. Start by comparing the first card in each hand.
    /// If these cards are different, the hand with the stronger first card is considered stronger.
    /// If the first card in each hand have the same label, however, then move on to considering the second card in each hand.
    /// If they differ, the hand with the higher second card wins; otherwise, continue with the third card in each hand, then the fourth, then the fifth.

    /// So, 33332 and 2AAAA are both four of a kind hands, but 33332 is stronger because its first card is stronger.
    /// Similarly, 77888 and 77788 are both a full house, but 77888 is stronger because its third card is stronger (and both hands have the same first and second card).

    /// To play Camel Cards, you are given a list of hands and their corresponding bid (your puzzle input). For example:

    /// 32T3K 765
    /// T55J5 684
    /// KK677 28
    /// KTJJT 220
    /// QQQJA 483
    /// This example shows five hands; each hand is followed by its bid amount.
    /// Each hand wins an amount equal to its bid multiplied by its rank, where the weakest hand gets rank 1, the second-weakest hand gets rank 2, and so on up to the strongest hand.
    /// Because there are five hands in this example, the strongest hand will have rank 5 and its bid will be multiplied by 5.

    /// So, the first step is to put the hands in order of strength:

    /// 32T3K is the only one pair and the other hands are all a stronger type, so it gets rank 1.
    /// KK677 and KTJJT are both two pair. Their first cards both have the same label, but the second card of KK677 is stronger (K vs T), so KTJJT gets rank 2 and KK677 gets rank 3.
    /// T55J5 and QQQJA are both three of a kind. QQQJA has a stronger first card, so it gets rank 5 and T55J5 gets rank 4.
    /// Now, you can determine the total winnings of this set of hands by adding up the result of multiplying each hand's bid with its rank (765 * 1 + 220 * 2 + 28 * 3 + 684 * 4 + 483 * 5). So the total winnings in this example are 6440.

    /// Find the rank of every hand in your set. What are the total winnings?
        
    /// </summary>

    
    public class Solution
    {
        /// <summary>
        /// There is class Hand and Row and Game and Card.
        /// Card has an input Char and a numeric value (to order)
        /// Hand has a card array and a dictionary in which a card is the key and the value is the count.
        /// From that we may conclude the five of a kind, pairs etc.
        /// Row has Hand and  Bid Amount, game has all rows and calulates the the ranks (orders them).
        /// Game contains all rows and sorts the rows directly.
        /// Result adds all numbers of the sorted rows
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
                    '2' => 2,
                    '3' => 3,
                    '4' => 4,
                    '5' => 5,
                    '6' => 6,
                    '7' => 7,
                    '8' => 8,
                    '9' => 9,
                    'T' => 10,
                    'J' => 11,
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
        /// The numeric card values as list
        /// </summary>
        public List<int> CardValues => Cards.Select(card => card.Value).ToList();
        
        /// <summary>
        /// A Dictionary that tracks how often each card occurs in the hand
        /// </summary>
        private Dictionary<Card, int> _cardCounts
        {
            get
            {
                var cardCounts = new Dictionary<Card, int>();
                foreach (var card in Cards)
                {
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
        /// Boolean Method to check whether the card is a five of a kind. 
        /// </summary>
        public bool Is5OfAKind => _cardCounts.Values.Any(value => value == 5);
        public bool Is4OfAKind => _cardCounts.Values.Any(value => value == 4);
        public bool Is3OfAKind => _cardCounts.Values.Any(value => value == 3);
        public bool IsTwoPairs => _cardCounts.Values.Count(value => value == 2) == 2;
        public bool IsOnePair => _cardCounts.Values.Count(value => value == 2) == 1;
        public bool IsFullHouse => Is3OfAKind && IsOnePair;

        public bool IsHighCard => _cardCounts.Values.All(value => value == 1);

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