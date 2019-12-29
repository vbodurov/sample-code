using System;
using System.Collections.Generic;
using System.Linq;
using CodingProblemsTests.Extensions;

namespace CodingProblemsTests.Utils
{
    public static class ListParser
    {
        public static IList<string> ToListOfStrings(string str)
        {
            return str
                    .Trim(new[] {'[', ']', ' ', '\t'})
                    .Split(",")
                    .Select(e => e.Trim(new[] {'"', ' ', '\t'}))
                    .ToArray()
                ;
        }

        public static IList<IList<int>> ToListOfLists(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;

            var parts = str
                .Replace(" ", "")
                .Replace("\t", "")
                .Replace("\n", "")
                .Replace("\r", "")
                .Trim(new []{'[',']'})
                .Split(new string[] {"],["}, StringSplitOptions.RemoveEmptyEntries)
                ;
            IList<IList<int>> result = new List<IList<int>>();
            foreach (var part in parts)
            {
                result.Add(part.Split(',').Select(int.Parse).ToArray());
            }
            return result;
        }
        public static string AsSortedString(IList<IList<int>> input)
        {
            if (input == null) return null;
            return "[" + input.Select(
                           list => "[" + list.OrderBy(n => n).JoinStrings(",") + "]")
                       .OrderBy(e => e)
                       .JoinStrings(",") + "]";
        }
        public static IList<int?> ToListOfNullableInts(string expectStr)
        {
            return expectStr
                    .Replace(" ", "")
                    .Replace("\t", "")
                    .Replace("[", "")
                    .Replace("]", "")
                    .Split(',')
                    .Select(e => int.TryParse(e, out var i) ? i : (int?) null)
                    .ToArray()
                ;

        }
    }
}