using System.Text.RegularExpressions;

namespace AOC5A;

public struct Convertible
{
    public IEnumerable<long> Values { get; set; } 

    public Convertible(List<long>? values = null)
    {
        Values = values ?? new List<long>();
    }

    public Convertible(string input)
    {
        string pattern = @"\d+";
        Regex regex = new Regex(pattern);
        if (!regex.IsMatch(input)) throw new Exception("invalid");

        var matches = regex.Matches(input).Select(match => long.Parse(match.Value)).ToList();

        Values = matches;
    }

    public override string ToString()
    {
        return $"Min: {Values.Min()} Values: {string.Join(",", Values)}";
    }
}