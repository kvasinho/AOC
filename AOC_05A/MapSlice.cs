using System.Text;
using System.Text.RegularExpressions;

namespace AOC5A;

public struct MapSlice
{
    public long Destination { get; set; }
    public long Source { get; set; }
    public long Count { get; set; }

    private Dictionary<long, long>? _mappedValues; // Backing field

    public Dictionary<long, long> MappedValues
    {
        get
        {
            if (_mappedValues == null) // Create the dictionary if it hasn't been created yet
            {
                _mappedValues = new Dictionary<long, long>();
                for (long i = 0; i < Count; i++)
                {
                    _mappedValues.Add(Source + i, Destination + i);
                }
            }

            return _mappedValues;
        }
    }

    public MapSlice(long destination, long source, long count)
    {
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative.");
    
        Destination = destination;
        Source = source;
        Count = count;
        _mappedValues = null; // Initialize backing field
    }


    public MapSlice(string input)
    {
        string pattern = @"\d+";
        Regex regex = new Regex(pattern);

        if (!regex.IsMatch(input)) throw new Exception($"{input} has invalid format. Expected: numbers");

        var matches = regex.Matches(input);

        if (matches.Count != 3) throw new Exception($"{input} has invalid format. Expected: number number number");

        Destination = long.Parse(matches[0].Value);
        Source = long.Parse(matches[1].Value);
        Count = long.Parse(matches[2].Value);

        _mappedValues = null; // Initialize backing field
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Source: {Source}, Destination: {Destination}, Count: {Count}");
        sb.AppendLine("First 10 Mapped Values:");

        int count = 0;
        foreach (var kvp in MappedValues)
        {
            sb.AppendLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            count++;

            if (count >= 10)
            {
                break; // Stop after printing 10 entries
            }
        }

        return sb.ToString();
    }


}