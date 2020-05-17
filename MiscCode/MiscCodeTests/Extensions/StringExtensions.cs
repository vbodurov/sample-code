using System.Collections.Generic;
using System.Linq;

namespace MiscCodeTests.Extensions
{
    public static class StringExtensions
    {
        public static string JoinAsString<T>(this IEnumerable<T> enumerable, string limiter)
        {
            if (enumerable == null) return null;
            return string.Join(limiter, enumerable.Select(e => e.ToString()).ToArray());
        }
    }
}