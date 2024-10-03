using System;
using System.Collections;
using AOC2A;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using Xunit.Abstractions;

namespace Aoc2_Tests
{
    
    public class Tests
    {
        
        private readonly ITestOutputHelper _output;
        
        public Tests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        public static IEnumerable<object[]> GetValidGameSetInputs()
        {
            yield return new object[] { "2 green", new GameSet(0,2)};
            yield return new object[] { "1 red, 2 green, 6 blue",new  GameSet(1,2,6) };
            yield return new object[] { "1 blue, 2 green", new GameSet(0,2,1)};
            yield return new object[] { "3 green, 4 blue, 1 red", new GameSet(1,3,4) };
            yield return new object[] { "1 green, 1 blue", new GameSet(0,1,1) };
            yield return new object[] { "", new GameSet() };
        }
        
        [Theory]
        [MemberData(nameof(GetValidGameSetInputs))]
        public void GameSet_FromString_Should_create_valid_inputs_correctly(string input, GameSet expectedGameSet)
        {
            var gameSet = GameSet.FromString(input);
            Assert.Equal(expectedGameSet,gameSet);
        }
        
        public static IEnumerable<object[]> GetGameInputsWithValidGameSets()
        {
            yield return new object[] { Tuple.Create("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 
                new Game(1, 
                    new List<GameSet>()
                    {
                        new GameSet(4,0,3),
                        new GameSet(1,2, 6),
                        new GameSet(0,2)
                    }))};

            yield return new object[] { Tuple.Create(
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                new Game(2, 
                    new List<GameSet>()
                    {
                        new GameSet(0,2,1),
                        new GameSet(1,3,4),
                        new GameSet(0,1,1)
                    }))};

            yield return new object[] { Tuple.Create(
                "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
                new Game(5, 
                    new List<GameSet>()
                    {
                        new GameSet(6,3,1),
                        new GameSet(1,2,2),
                    }))};
        }

        [Theory]
        [MemberData(nameof(GetGameInputsWithValidGameSets))]
        public void Game_FromInputLine_Should_create_valid_inputs_correctly(Tuple<string, Game> inputTuple)
        {
            string input = inputTuple.Item1;
            Game expectedGame = inputTuple.Item2;

            var game = Game.FromInputLine(input);
            
            _output.WriteLine($"Expected: {expectedGame.GameId}");
            expectedGame.GameSets.ForEach(set => _output.WriteLine(set.ToString()));
            
            _output.WriteLine($"Actual: {game.GameId}");
            game.GameSets.ForEach(set => _output.WriteLine(set.ToString()));
            
            _output.WriteLine($"Has equivalent sets: {game.Equals(expectedGame)}");
    
            Assert.NotNull(game);
            Assert.True(expectedGame.Equals(game));
        }
        
        [Fact]
        public void Game_FromInputLine_Single_Test_Case()
        {
            _output.WriteLine("Single test case executed");
            Assert.True(true);
        }
        
        public static IEnumerable<object[]> GetGameInputsWithInValidGameSets()
        {
            yield return new object[] { "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 
                new Game(3, new List<GameSet>()
                    {
                        new GameSet(20,8,6),
                        new GameSet(4,13,5),
                        new GameSet(1,5)
                    })
            };
            yield return new object[] { "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                new Game(4, 
                    new List<GameSet>()
                    {
                        new GameSet(3,1,6),
                        new GameSet(6,3),
                        new GameSet(14,3,15)
                    })
            };
        }
    }
}