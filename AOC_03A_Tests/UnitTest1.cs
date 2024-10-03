using System.Dynamic;
using AOC3A;
using Extensions;
using Xunit;
using Xunit.Abstractions;

namespace AOC_03A_Tests;

public class UnitTest1
{

    private readonly ITestOutputHelper _output;
    private readonly Matrix _fieldMatrix;
    private readonly Matrix _stringMatrix;
    
    private static readonly string[] _lines = new string[] {
        "467..114..",
        "...*......",
        "..35..633.",
    };
    private static readonly List<Field> _fields = new List<Field>()
    {
        new Field('4', 0, 0),
        new Field('6', 0, 1),
        new Field('7', 0, 2),
        new Field('.', 0, 3),
        new Field('.', 0, 4),
        new Field('1', 0, 5),
        new Field('1', 0, 6),
        new Field('4', 0, 7),
        new Field('.', 0, 8),
        new Field('.', 0, 9),

        new Field('.', 1, 0),
        new Field('.', 1, 1),
        new Field('.', 1, 2),
        new Field('*', 1, 3),
        new Field('.', 1, 4),
        new Field('.', 1, 5),
        new Field('.', 1, 6),
        new Field('.', 1, 7),
        new Field('.', 1, 8),
        new Field('.', 1, 9),


        new Field('.', 2, 0),
        new Field('.', 2, 1),
        new Field('3', 2, 2),
        new Field('5', 2, 3),
        new Field('.', 2, 4),
        new Field('.', 2, 5),
        new Field('6', 2, 6),
        new Field('3', 2, 7),
        new Field('3', 2, 8),
        new Field('.', 2, 9),

    };

    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
        _fieldMatrix = new Matrix();
        
        _fieldMatrix.Fields = _fields.ToHashSet();
        _stringMatrix = Matrix.FromLines(_lines.ToList());

    }
    
    [Theory]
    [InlineData('*')]
    [InlineData('$')]
    [InlineData('_')]
    [InlineData(',')]
    public void Field_Should_Be_Created_WithSymbol(char input)
    {
        var field = new Field(input,1,1);
        Assert.True(field.FieldType == FieldType.SYMBOL);
    }
    [Theory]
    [InlineData('1')]
    [InlineData('2')]
    [InlineData('3')]
    public void Field_Should_Be_Created_WithDigit(char input)
    {
        var field = new Field(input,1,1);
        Assert.True(field.IsDigit);
    }
    [Theory]
    [InlineData('.')]
    public void Field_Should_Be_Created_WithDot(char input)
    {
        var field = new Field(input,1,1);
        Assert.True(field.IsDot);
    }
    
    [Theory]
    [InlineData('1')]
    [InlineData('2')]
    [InlineData('3')]
    public void MarkAsDone_Should_SetCharacter_ToDot(char input)
    {
        var field = new Field(input,1,1);
        field.MarkAsDone();
        Assert.True(field.IsDot);
        Assert.Equal('.', field.Character);
    }

    [Fact]
    public void FromLine_Should_Create_Fields_Corretly()
    {
        var line = "123";
        var fields = new List<Field>()
        {
            new Field('1', 0, 0),
            new Field('2', 0, 1),
            new Field('3', 0, 2)
        };

        var createdFields = Field.FromLine(line, 0).ToList();
        fields.IsEquivalent(createdFields);
    }

    [Fact]
    public void ToNumber_Should_ExtractNumbers_Correctly()
    {
        var firstRow = _fields.Where(field => field.RowPos == 0).ToList();
        var numberFields1 = _fieldMatrix.GetNumberFields(_fieldMatrix.OrderedFields[0]);
        _output.WriteLine(string.Join("",numberFields1));
        
        Assert.Equal(467, Field.ToNumber(numberFields1));
    }
    
    
    [Fact]
    public void Matrix_Should_Have_Equal_Fields()
    {
        //Assert in constructor 
        Assert.Equal(_fieldMatrix.OrderedFields, _stringMatrix.OrderedFields);
    }
    
    [Fact]
    public void Matrix_Should_FindAdjacent_Numbers()
    {
        //Field 0 should be adjacent to 10,11,1;
        var adjacentFields = _fieldMatrix.GetAdjacentFields(_fields[0]).ToList();
        var expectedFields = new List<Field>() { _fields[1], _fields[10], _fields[11] };

        var adjacentFields2 = _fieldMatrix.GetAdjacentFields(_fields[24]).ToList();
        var expectedFields2 = new List<Field>
        {
            _fields[23], _fields[25],
            _fields[13], _fields[14], _fields[15],
        };
        
        var adjacentFields3 = _fieldMatrix.GetAdjacentFields(_fields[19]).ToList();
        var expectedFields3 = new List<Field>
        {
            _fields[8], _fields[9],
            _fields[18],
            _fields[28], _fields[29]
        };        
        
        Assert.True(expectedFields.IsEquivalent(adjacentFields));
        Assert.True(expectedFields2.IsEquivalent(adjacentFields2));
        Assert.True(expectedFields3.IsEquivalent(adjacentFields3));
    }

    [Fact]
    public void GetNumberFields_Should_Fail_OnNonDigits()
    {
        var input = _fieldMatrix.OrderedFields[4];
        Assert.Throws<Exception>(() => _fieldMatrix.GetNumberFields(input));
    }


}