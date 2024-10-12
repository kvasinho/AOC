using Extensions;

namespace AOC5A;

public struct Converter
{

    public Dictionary<long, long> Map
    {
        get
        {
            Dictionary<long, long> map = new Dictionary<long, long>();
                
                
            foreach (var slice in MapSlices)
            {
                map.AddDictionary(slice.MappedValues);
            }

            return map;
        }
    }

    public List<MapSlice> MapSlices { get; set; } 

    public Converter()
    {
        MapSlices = new List<MapSlice>();
    }

    public Converter(string input)
    {
        var splits = input.Trim().Split("\n").ToList();
    
        Console.WriteLine("Initial splits count: " + splits.Count);
    
        // Ensure there are valid splits to process
        if (splits.Count == 0)
        {
            throw new Exception("No valid input slices found.");
        }

        var slices = splits
            .Select(split => 
            {
                try
                {
                    return new MapSlice(split.Trim());
                }
                catch (Exception ex)
                {
                    throw new ($"Error parsing slice: {split}. Exception: {ex.Message}");
                }
            })
            .ToList();

        MapSlices = slices;
    }
        
        
    public Convertible Convert(Convertible input)
    {
        var local = Map;
        var mappedValues = input.Values.Select(val =>
        {
            if (local.TryGetValue(val, out var mappedVal))
            {
                return mappedVal;
            }

            return val;
        }).ToList();

        return new Convertible(mappedValues);
        
    }
}