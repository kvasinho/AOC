using System;
using System.Collections;
using AOC2B;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using Xunit.Abstractions;

namespace AOC2B_Tests
{
    public class Tests
    {
        
        private readonly ITestOutputHelper _output;
        
        public Tests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        public static IEnumerable<object[]> GetGameInputsWithValidGameSets()
        {
            yield return new object[]
            {
                Tuple.Create(
                    "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                    new GameSet(4,2,6),
                    48
                )
            };

            yield return new object[] { Tuple.Create(
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                new GameSet(1,3,4),
                12
                )
            };

            yield return new object[] { Tuple.Create(
                    "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 
                    new GameSet(20,13,6),
                    1560
                )
            };
            yield return new object[] { Tuple.Create(
                   "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                    new GameSet(14,3,15),
                   630
                )
            };
            yield return new object[] { Tuple.Create(
                    "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
                    new GameSet(6,3,2),
                    36
                )
            };
        }

        [Theory]
        [MemberData(nameof(GetGameInputsWithValidGameSets))]
        public void Game_SmallestPossibleGameSets_Should_Be_CalulatedCorrectly(Tuple<string, GameSet, int> inputTuple)
        {
            string input = inputTuple.Item1;
            GameSet expectedGameSet = inputTuple.Item2;
            int expectedPower = inputTuple.Item3;

            var game = Game.FromInputLine(input);
            
            Assert.Equal(expectedGameSet, game.GetSmallestPossibleGameSet);
            Assert.Equal(expectedPower, game.GetSmallestPossibleGameSetPower);
        }
        
    }
}