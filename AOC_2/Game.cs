using Extensions;

namespace AOC2A;


/// <summary>
/// The game class contains all the logic of a single input line. A game id and one or more sets.
/// It parses a full line and creates all necessary objects and furthermore checks whether a game is possible (has only valid Game sets).
/// </summary>
public class Game
    {
        public int GameId { get; set; }
        public List<GameSet> GameSets { get; set; }
            
        public Game(int? gameId = null, List<GameSet>? gameSets = null)
        {
            GameId = gameId ?? 0;
            GameSets = gameSets ?? new List<GameSet>();
        }
            
        /// <summary>
        /// Computed Field to check whether all Sets of a game are valid (meet the restrictions)
        /// </summary>
        public bool IsPossible => GameSets.All(set => set.IsValid);

        /// <summary>
        /// Static Method to generate a game object from a string. 
        /// </summary>
        /// <param name="input">The string input that is used to create the game.</param>
        /// <returns>A Game object</returns>
        /// <exception cref="Exception">Exception if the input does not contain a colon.</exception>
        public static Game FromInputLine(string input)
        {
            var game = new Game();

            var splitInputLine = input.Trim().ToLower().Split(":");

            if (splitInputLine.Length != 2)
            {
                throw new Exception("Invalid result count");
            }
                

            if (int.TryParse(splitInputLine[0].Replace("game ", ""), out int gameId))
            {
                game.GameId = gameId;
                game.GameSets = splitInputLine[1].Split(";").Select(split => GameSet.FromString(split)).ToList();
            }

            return game;
        }
            
        /// <summary>
        /// Checks this game object and another object for equality.
        /// Uses IsEquivalent that compares 2 lists of games.
        /// Also works when the same set appears multiple times - can be used here since we only check for validity.
        /// </summary>
        /// <param name="obj">Comparison object</param>
        /// <returns>a Boolean that indicates the equality of 2 sets.</returns>
        public override bool Equals(object? obj)
        {
            if(obj is null || obj is not Game other)
            {
                return false;
            }

            if (GameSets.Count != other.GameSets.Count)
            {
                return false;
            }

            if (GameSets.IsEquivalent(other.GameSets))
            {
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Return the string representation of a Game. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID: {GameId} Sets: {GameSets.Count}";
        }
    }