using System.Collections;

namespace Extensions;

public static class ListExtensions
{
    /// <summary>
    /// A list extension method to compare two lists. Is true if both have the same elements. Number of Elements does not matter
    /// </summary>
    /// <param name="current">A list with a given Type that acts as base for extension method</param>
    /// <param name="other">A list that is being compared to</param>
    /// <returns></returns>
    public static bool IsEquivalent(this IList current, IList other)
    {
        if (current == null || other == null)
            return false;
        
        if (current.Equals(other))
            return true;

        if (current.Count != other.Count)
            return false;

        var hashSet1 = new HashSet<object>(current.Cast<object>());
        var hashSet2 = new HashSet<object>(other.Cast<object>());

        return hashSet1.SetEquals(hashSet2);    
    }
}