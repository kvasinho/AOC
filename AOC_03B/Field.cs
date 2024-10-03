using Microsoft.VisualBasic.CompilerServices;

namespace AOC3A;

public class Field
{
    public char Character { get; set; } 
    
    
    public int RowPos { get; }
    public int ColPos { get; }

    public FieldType FieldType
    {
        get
        {
            if (Char.IsDigit(Character)) return FieldType.DIGIT;
            if (Character == '.') return FieldType.DOT;
            if (Character == '*') return FieldType.STAR;
            return FieldType.SYMBOL;
        }
    }

    public bool IsDigit => FieldType == FieldType.DIGIT;
    public bool IsDot => FieldType == FieldType.DOT;
    public bool IsSymbol => FieldType == FieldType.SYMBOL;
    public bool IsStar => FieldType == FieldType.STAR;

    public Field(char character, int rowPos, int colPos)
    {
        Character = character;
        RowPos = rowPos;
        ColPos = colPos;
    }
    /// <summary>
    /// Replaces the current digit field with a dot to prevent counting multiple times.
    /// </summary>
    public void MarkAsDone()
    {
        if (FieldType.DIGIT == FieldType)
        {
            Character = '.';
        }
    }



    public override int GetHashCode()
    {
        return HashCode.Combine(Character, RowPos, ColPos);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Field field)
        {
            return false;
        }

        return field.GetHashCode() == GetHashCode();
    }
    public static bool operator ==(Field? left, Field? right)
    {
        if (left is null || right is null) return false;
        return left.Equals(right);
    }
    
    public static bool operator !=(Field? left, Field? right)
    {
        if (left is null || right is null) return false;
        return !left.Equals(right);
    }

    public static IEnumerable<Field> FromLine(string line, int row)
    {
        return line.Select((character, index) => new Field(character, row, index));
    }

    public static int ToNumber(List<Field> digits)
    {
        if (!digits.Any()) throw new Exception("Must at least have length 1");
        
        if (digits.Any(field => field.FieldType != FieldType.DIGIT)) throw new Exception("All fields must be digits");

        
        string numberString = string.Join("",digits.Select(field => field.Character));

        if (!int.TryParse(numberString, out int result))
        {
            throw new Exception("Could not parse digits");
        }

        return result;
    }

    public override string ToString()
    {
        return $"{Character}";
    }
}

