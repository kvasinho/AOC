namespace AOC3A;

/// <summary>
/// The full matrix -> List of Fields. Each Field should also have
/// </summary>
public class Matrix
{
    /// <summary>
    /// A set of Fields with different posititions.
    /// HashValue is calculated by row and col position and therefore each position may only occur once.
    /// </summary>
    public HashSet<Field> Fields { get; set; } = new HashSet<Field>();

    /// <summary>
    /// Field that  orders by rows and then by columns. 
    /// </summary>
    public List<Field> OrderedFields
    {
        get
        {
            return Fields.OrderBy(field => field.RowPos)
                .ThenBy(field => field.ColPos)
                .ToList();
        }
    }

    /// <summary>
    /// The number of rows. 
    /// </summary>
    public int Rows => Fields.Max(field => field.RowPos + 1);
    
    /// <summary>
    /// The number of columns
    /// </summary>
    public int Columns => Fields.Max(field => field.ColPos + 1);

    /// <summary>
    /// Get all adjacent fields of a specific field in a new list. 
    /// </summary>
    /// <param name="field">The field to find the adjacent fields for</param>
    /// <returns></returns>
    public IEnumerable<Field> GetAdjacentFields(Field field)
    {
        
        var minRow = Math.Max(0, field.RowPos - 1);
        var maxRow = Math.Min(Rows, field.RowPos + 1);

        var minCol = Math.Max(0, field.ColPos - 1);
        var maxCol = Math.Min(Columns, field.ColPos + 1);

        return Fields.Where(f =>
            f.ColPos >= minCol && 
            f.ColPos <= maxCol && 
            f.RowPos >= minRow &&
            f.RowPos <= maxRow &&
            f != field );
    }

    
    /// <summary>
    /// Finds the full number for the array of fields if the current number is a digit.
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    public List<Field> GetNumberFields(Field field)
    {
        if (!field.IsDigit)
        {
            throw new Exception("Field must be a digit");
        }

        var numberFields = new List<Field>() { field };

        // Search to the right (larger ColPos)
        for (int i = field.ColPos + 1; i < Columns; i++)
        {
            var adjacentField = Fields.FirstOrDefault(f => f.RowPos == field.RowPos && f.ColPos == i && f.IsDigit);
        
            if (adjacentField is null) break; 
        
            numberFields.Add(adjacentField);
        }

        // Search to the left (smaller ColPos)
        for (int i = field.ColPos - 1; i >= 0; i--)
        {
            var adjacentField = Fields.FirstOrDefault(f => f.RowPos == field.RowPos && f.ColPos == i && f.IsDigit);
        
            if (adjacentField is null) break; 
        
            numberFields.Insert(0, adjacentField); 
        }

        return numberFields;
    }

    
    
    /// <summary>
    /// Instantiates a matrix from a list of input string lines. 
    /// </summary>
    /// <param name="lines">The input List of strings.</param>
    /// <returns></returns>
    public static Matrix FromLines(List<string> lines)
    {
        Matrix matrix = new Matrix();

        var fields = lines.SelectMany(Field.FromLine);
        matrix.Fields.UnionWith(fields);

        return matrix;
    }

    public override int GetHashCode()
    {
        // Use the fields' hash codes directly instead of OrderedFields, to avoid re-sorting every time
        int hash = 17;
        foreach (var field in Fields)
        {
            hash = hash * 23 + field.GetHashCode();
        }
        return hash;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Matrix matrix)
        {
            return false;
        }

        return OrderedFields.Equals(matrix.OrderedFields);
    }
}