using System.Collections;
using AOC7A;
using Xunit;
using Xunit.Abstractions;

namespace AOC_07A_Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;
    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void Card_Should_Throw_On_Invalid_Card()
    {
        Assert.Throws<ArgumentException>(() => new Card('b').Value);
    }
    [Fact]
    public void Card_Should_Be_Created_Correctly()
    {
        Assert.Equal(14, new Card('A').Value);
    }

    public static IEnumerable<object[]> GetHandInputs()
    {
        yield return new object[] { "32T3K", 2};
        yield return new object[] { "T55J5",4};
        yield return new object[] { "KK677",3};
        yield return new object[] { "KTJJT", 3};
        yield return new object[] { "QQQJA",4 };
    }
    [Theory]
    [MemberData(nameof(GetHandInputs))]
    public void Hand_Should_Be_Created_Correctly(string cardInput, int handValue)
    {
        var card = new Hand(cardInput);
        
        Assert.Equal(handValue,card.HandValue);
    }

    public static IEnumerable<object[]> GetRowInputs()
    {
        yield return new object[] { "32T3K 765", new Row(new Hand("32T3K"), 765) };
        yield return new object[] { "T55J5 684", new Row(new Hand("T55J5"), 684)};
        yield return new object[] { "KK677 28", new Row(new Hand("KK677"),28)  };
        yield return new object[] { "KTJJT 220", new Row(new Hand("KTJJT"), 220) };
        yield return new object[] { "QQQJA 483", new Row(new Hand("QQQJA"), 483) };
    }

    [Theory]
    [MemberData(nameof(GetRowInputs))]
    public void Row_Should_BeCreated_Correctly(string input, Row row)
    {
        var fromStringRow = Row.FromString(input);

        Assert.Equal(row.Hand, fromStringRow.Hand);
    }

    [Fact]
    public void Game_Should_BeCreated_Correctly()
    {
        var inputStrings = new List<string>
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483"
        }.ToArray();
        
        var game = Game.FromInputLines(inputStrings);
        Assert.Equal(6440, game.Result);
    }

    
    
    
    
    
}