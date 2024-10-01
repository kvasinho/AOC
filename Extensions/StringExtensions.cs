namespace Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Extract all digits as positive integer from the start of a string.
    /// If no digit appears at the start of a string, -1 is returned. 
    /// </summary>
    /// <param name="input"></param>
    /// <returns>the resulting integer or 0</returns>
    public static int ExtractNumberFromStringStart(this string input)
    {
        var number = "";
        input = input.Trim();
        foreach (var c in input)
        {
            if (char.IsDigit(c))
            {
                number += c;
                continue;
            }

            break;
        }

        if (int.TryParse(number, out var result))
        {
            return result;
        }

        return -1;
    }
}