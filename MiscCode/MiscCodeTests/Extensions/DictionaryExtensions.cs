using System.Collections.Generic;

namespace MiscCodeTests.Extensions
{
    public static class DictionaryExtensions
    {
        public static IDictionary<T, int> Increment<T>(this IDictionary<T, int> dict, T currentKey)
        {
            if (!dict.TryGetValue(currentKey, out var count)) count = 0;
            dict[currentKey] = count + 1;
            return dict;
        }
    }
}