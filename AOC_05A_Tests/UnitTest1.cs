using System.Collections;
using System.Text.RegularExpressions;
using AOC5A;
using Extensions;
using Xunit.Abstractions;
using InvalidCastException = System.InvalidCastException;

namespace AOC_05A_Tests;

public class UnitTest1
{

    private readonly ITestOutputHelper _output;
    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }
    
    
    //We have a seed (convertible) object <- convertible is instantiated from string and then just converted further.
    //We also have a map object that is a dictionary (map) that maps from one value to the other and then changes the convertible
    //The map object should fill in all missing values with itself after all mapped values are inserted.
    
    public static IEnumerable<object[]> GetMapSliceInputs()
    {
        yield return new object[] { "2093671499 2217398614 16037515", new MapSlice(2093671499,2217398614,16037515), true};
        yield return new object[] { "144190400 1167267743 4402289",  new MapSlice(144190400,1167267743,4402289), true };
        yield return new object[] { "", new MapSlice(0,0,0), false};
        yield return new object[] { "1 2", new MapSlice(0,0,0), false};
    }
    
    [Theory]
    [MemberData(nameof(GetMapSliceInputs))]
    public void MapSlice_Should_Be_Created_SuccessFully(string input, MapSlice expectedMapSlice, bool isSuccess)
    {
        if (!isSuccess)
        {
            Assert.Throws<Exception>(() => new MapSlice(input));
        }
        else
        {
            var slice = new MapSlice(input);
            Assert.Equal(expectedMapSlice.Source, slice.Source);
            Assert.Equal(expectedMapSlice.Destination, slice.Destination);
            Assert.Equal(expectedMapSlice.Count, slice.Count);
        }
    }
    
    public static IEnumerable<object[]> GetConvertibleInputs()
    {
        yield return new object[] { "seeds: 79 14 55 13", new List<long>(){79,14,55,13}, true};
        yield return new object[] { "seeds: 1 2 3 4 05", new List<long>(){1,2,3,4,5}, true };
        yield return new object[] { "", new List<long>(), false};
        yield return new object[] { "test", new List<long>(), false };
    }
    [Theory]
    [MemberData(nameof(GetConvertibleInputs))]
    public void Convertible_Should_Be_Created_SuccessFully(string input, List<long> expectedValues, bool isSuccess)
    {
        if (!isSuccess)
        {
            Assert.Throws<Exception>(() => new Convertible(input));
        }
        else
        {
            Assert.Equal(expectedValues, new Convertible(input).Values);
        }
    }

    public static IEnumerable<object[]> GetConverterInputs()
    {
        yield return new object[]
        {
            "3 5 2\n 8 11 3\n", new Dictionary<long, long>()
            {
                { 5, 3 },
                { 6, 4 },
                { 11, 8 },
                { 12, 9 },
                { 13, 10 }
            },
            true
        };
        yield return new object[]
        {
            "seed-to-soil map:\n3 5 2\n 8 11 3\n", new Dictionary<long, long>()
            {
                { 5, 3 },
                { 6, 4 },
                { 11, 8 },
                { 12, 9 },
                { 13, 10 }
            },
            false
        };


        yield return new object[]
        {
            "seed-to-soil map:\ntestetsttest\n", new Dictionary<long, long>()
            {
                { 1, 1 },
                { 2, 2 },
                { 3, 3 },
                { 4, 4 },
                { 5, 3 },
                { 6, 4 },
                { 7, 5 },
                { 11, 8 },
                { 12, 9 },
                { 13, 10 }
            },
            false
        };
    }

    [Theory]
    [MemberData(nameof(GetConverterInputs))]
    public void Converter_Should_Be_Created_Properly(string input, Dictionary<long, long> expectedMapping, bool isSuccess)
    {
        if (!isSuccess)
        {
            Assert.Throws<Exception>(() => new Converter(input));
        }
        else
        {
            var converter = new Converter(input);
            _output.WriteLine($"{converter.MapSlices.Count()}");
            _output.WriteLine($"" +
                              $"{converter.MapSlices.First().Source}" +
                              $"{converter.MapSlices.First().Destination}" +
                              $"{converter.MapSlices.First().Count}" +
                              $"{string.Join(",", converter.MapSlices.First().MappedValues.Keys)}");
            _output.WriteLine($"{expectedMapping.Count}");
            //Assert.Equal(expectedMapping, converter.Map);
            Assert.Equal(expectedMapping, converter.Map);
        }
    }
    
}