using System;
using System.Collections;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using AOC1B;
using Xunit;
using Xunit.Abstractions;

namespace Aoc1B_Tests
{
    
    public class Tests
    {
        
        private readonly ITestOutputHelper _output;
        
        public Tests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        Solution solution = new Solution();
        
        
        [Theory]
        [InlineData("one")]
        [InlineData("oneeee")]
        [InlineData("oneoneone")]
        public void GetDigitMatch_Should_handle_true_inputs_correctly(string substring)
        {
            _output.WriteLine("TESTSTST");
            Assert.Equal("one", solution.GetDigitMatch(substring));
        }
        
        [Theory]
        [InlineData("on")]
        [InlineData("onne")]
        [InlineData("")]
        public void GetDigitMatch_Should_handle_false_inputs_correctly(string substring)
        {
            Assert.Equal("", solution.GetDigitMatch(substring));
        }
        
        public static IEnumerable<object[]> GetValidStringInputs()
        {
            yield return new object[] { "two1nine", new List<int> { 2,1,9 } };
            yield return new object[] { "eightwothree", new List<int> { 8,2, 3 } };
            yield return new object[] { "abcone2threexyz", new List<int> { 1,2,3 } };
            yield return new object[] { "xtwone3four", new List<int> { 2,1,3,4 } };
            yield return new object[] { "4nineeightseven2", new List<int> { 4,9,8,7,2 } };
            yield return new object[] { "zoneight234", new List<int> { 1,8, 2, 3, 4  } };
            yield return new object[] { "7pqrstsixteen", new List<int> { 7,6 } };
        }
        
        [Theory]
        [MemberData(nameof(GetValidStringInputs))]
        public void ExtractDigits_Should_handle_valid_Inputs_Correctly(string input, List<int> result)
        {
            var digits = solution.ExtractDigits(input);
            Assert.Equal(result, digits);
        }
        
        
        public static IEnumerable<object[]> GetValidIntListInputs()
        {
            yield return new object[] { new List<int> { 2,1,9 }, 29 };
            yield return new object[] { new List<int> { 8,2,3 }, 83 };
            yield return new object[] { new List<int> { 1,2,3 }, 13 };
            yield return new object[] { new List<int> { 2,3,4 }, 24 };
            yield return new object[] { new List<int> { 4,9,8,7,2 }, 42 };
            yield return new object[] { new List<int> { 1,2,3,4 }, 14};
            yield return new object[] { new List<int> { 1 }, 11 };
        }

        
        [Theory]
        [MemberData(nameof(GetValidIntListInputs))]
        public void GetFromFirstAndLastDigit_Should_handle_valid_Inputs_Correctly(List<int> intList, int result)
        {
            var digits = solution.GetFromFirstAndLastDigit(intList);
            Assert.Equal(result, digits);
        }
        
        [Fact]
        public void GetFromFirstAndLastDigit_Should_Throw_OnEmptyList()
        {
            
            Assert.Throws<Exception>(() => solution.GetFromFirstAndLastDigit(new List<int>()));
        }
        

        
    }
}