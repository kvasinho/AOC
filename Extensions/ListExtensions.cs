using System.Collections;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;

namespace Extensions;

public static class ListExtensions
{
    public static IEnumerable<int> GetValuesUntil(this IEnumerable<int> source, int stopValue) => GetValuesUntil<int>(source, stopValue);
    public static IEnumerable<float> GetValuesUntil(this IEnumerable<float> source, float stopValue) => GetValuesUntil<float>(source, stopValue);
    public static IEnumerable<decimal> GetValuesUntil(this IEnumerable<decimal> source, decimal stopValue) => GetValuesUntil<decimal>(source, stopValue);
    public static IEnumerable<double> GetValuesUntil(this IEnumerable<double> source, double stopValue) => GetValuesUntil<double>(source, stopValue);

    /// <summary>
    /// Extension Method to get all elements in a list before the first occurence of a certain value.
    /// </summary>
    /// <param name="source">An enumerable of numeric values. </param>
    /// <param name="stopValue">The value to stop iterating at first occurence</param>
    /// <typeparam name="T">Numeric Types</typeparam>
    /// <returns>A deferrable IEnumerable T </returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    private static IEnumerable<T> GetValuesUntil<T>(this IEnumerable<T> source, T stopValue) where T: struct, INumber<T>, IEquatable<T>
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        
        foreach (var item in source)
        {
            if (item.Equals(stopValue)) yield break;

            yield return item;
        }
    }
    
    public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
    {
        if(target==null)
            throw new ArgumentNullException(nameof(target));
        if(source==null)
            throw new ArgumentNullException(nameof(source));
        foreach(var element in source)
            target.Add(element);
    }


    
    private static bool IsNumericType(this Type type)
    {
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return true;
            default:
                return false;
        }
    }
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