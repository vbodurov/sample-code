using System;
using System.Collections.Generic;
using System.Linq;
using CodingProblemsTests.Extensions;

namespace CodingProblemsTests.Utils
{
    public static class ListParser
    {
        public static IList<IList<int>> ToListOfLists(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;

            var parts = str
                .Replace(" ", "")
                .Replace("\t", "")
                .Trim(new []{'[',']'})
                .Split(new string[] {"],["}, StringSplitOptions.RemoveEmptyEntries)
                ;
            IList<IList<int>> result = new List<IList<int>>();
            foreach (var part in parts)
            {
                Console.WriteLine("|"+part+"|");
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
    }
}