using System.Collections.Generic;

namespace CodingProblemsTests.Extensions
{
    public static class StringExtensions
    {
        public static string JoinStrings<T>(this IEnumerable<T> collection, string separator)
        {
            return string.Join(separator, collection);
        }
    }
}