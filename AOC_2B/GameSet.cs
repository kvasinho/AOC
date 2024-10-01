using Extensions;

namespace AOC2B;

public class GameSet
        {
            /// <summary>
            /// The number of red cubes. 
            /// </summary>
            public int Red { get; set; } 
            /// <summary>
            /// The number of green cubes. 
            /// </summary>
            public int Green { get; set; } 
            /// <summary>
            /// The number of blue cubes. 
            /// </summary>
            public int Blue { get; set; } 
            

            public GameSet(int red = 0, int green = 0, int blue = 0)
            {
                Red = red;
                Green = green;
                Blue = blue;
            }


 
            /// <summary>
            /// Static method to create a Gameset object from a given set string. 
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public static GameSet FromString(string input)
            {
                var set = new GameSet();

                var cubeStrings = input.Split(",").Select(cube => cube.ToLower().Trim());
                
                foreach (var cubeString in cubeStrings)
                {
                    if (cubeString.EndsWith("blue"))
                    {
                        set.Blue = cubeString.ExtractNumberFromStringStart();
                    }

                    if (cubeString.EndsWith("red"))
                    {
                        set.Red = cubeString.ExtractNumberFromStringStart();
                    }

                    if (cubeString.EndsWith("green"))
                    {
                        set.Green = cubeString.ExtractNumberFromStringStart();
                    }
                }

                return set;
            }
            /// <summary>
            /// Return the HashCode 
            /// </summary>
            /// <returns>The hashed integer</returns>
            public override int GetHashCode()
            {
                return HashCode.Combine(Red, Green, Blue);
            }

            /// <summary>
            /// Compares this GameSet to another object for rquality. 
            /// </summary>
            /// <param name="obj">The comparison object</param>
            /// <returns>A boolean indicating equality of 2 objects.</returns>
            public override bool Equals(object? obj)
            {
                if (obj is null || obj is not GameSet other)
                {
                    return false;
                }

                return Red == other.Red && Blue == other.Blue && Green == other.Green;
            }
            

            public override string ToString()
            {
                return $"Red: {Red}, Blue: {Blue}, Green: {Green}";
            }
        }