namespace Extensions;

public static class DictionaryExtensions
{
    public static void AddDictionary<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> addiction, bool replace = true)
    {
        foreach (var entry in addiction)
        {
            if (replace)
            {
                source.Add(entry.Key,entry.Value);
                continue;
            }

            if (!source.TryAdd(entry.Key, entry.Value)) throw new Exception($"Key: {entry.Key} already exists");
        }
    }
}