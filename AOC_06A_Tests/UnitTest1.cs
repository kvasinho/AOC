using AOC6A;
using Xunit.Abstractions;

namespace AOC_06A_Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;
    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Theory]
    [InlineData(7,0,0)]
    [InlineData(7,1,6)]
    [InlineData(7,2,10)]
    [InlineData(7,3,12)]
    [InlineData(7,4,12)]
    [InlineData(7,5,10)]
    [InlineData(7,6,6)]
    [InlineData(7,7,0)]
    public void HypotheticalRace_Should_Calculate_Distance_Correctly(int raceTime, int buttonPressedTime, int expectedDistance)
    {
        HypotheticalRace race = new HypotheticalRace(raceTime, buttonPressedTime);
        Assert.Equal(expectedDistance, race.Distance);
    }

    [Theory]
    [InlineData(7, 9, 4)]
    [InlineData(15, 40, 8)]
    [InlineData(30, 200, 9 )]
    public void ActualRace_Should_CalculateWinningBoats_Correctly_(int raceTime, int distance, int winningHypotheticalBoats)
    {
        Race race = new Race(raceTime, distance);
        Assert.Equal(winningHypotheticalBoats, race.NumberOfWinningHypotheticalRaces);
    }
    
}