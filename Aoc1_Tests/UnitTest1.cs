using System;
using System.Collections;
using AOC1;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace Aoc1_Tests
{
    
    public class Tests
    {
        
        Solution solution = new Solution();
        public static IEnumerable<object[]> GetValidStringInputs()
        {
            yield return new object[] { "1abc2", new List<int> { 1, 2 } };
            yield return new object[] { "pqr3stu8vwx", new List<int> { 3, 8 } };
            yield return new object[] { "a1b2c3d4e5f", new List<int> { 1, 2, 3, 4, 5 } };
            yield return new object[] { "treb7uchet", new List<int> { 7 } };
            yield return new object[] { "tuchet", new List<int> {  } };
        }


        
        [Theory]
        [MemberData(nameof(GetValidStringInputs))]
        public void ParseLine_Should_handle_valid_Inputs_Correctly(string input, List<int> result)
        {
            var digits = solution.ExtractDigits(input);
            Assert.Equal(result, digits);
        }
        
        
        public static IEnumerable<object[]> GetValidIntListInputs()
        {
            yield return new object[] { new List<int> { 1, 2, 4 }, 14 };
            yield return new object[] { new List<int> { 1, 4 }, 14 };
            yield return new object[] { new List<int> { 1 }, 11 };
            yield return new object[] { new List<int> { 0, 0, 0 }, 0 };
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