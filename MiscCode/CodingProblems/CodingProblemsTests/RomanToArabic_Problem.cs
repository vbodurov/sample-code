using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class RomanToArabic_Problem
    {
        [Test, Category(category.SpecificProblems)]
        [TestCase("IV", 4)]
        [TestCase("II", 2)]
        [TestCase("IX", 9)]
        [TestCase("XI", 11)]
        [TestCase("LVIII", 58)]
        [TestCase("MCMXCIV", 1994)]
        public void RomanToArabic(string roman, int expect)
        {
            var result = new Solution().RomanToInt(roman);
            Assert.That(result, Is.EqualTo(expect));
        }
        public class Solution
        {
            static readonly Dictionary<char, int> _map =
                new Dictionary<char, int>
                {
                    {'I',1},
                    {'V',5},
                    {'X',10},
                    {'L',50},
                    {'C',100},
                    {'D',500},
                    {'M',1000}
                };
            static readonly HashSet<string> _substracters =
                new HashSet<string>
                {
                    "IV", "IX", "XL", "XC", "CD", "CM"
                };
            public int RomanToInt(string s)
            {
                var result = 0;
                if (string.IsNullOrEmpty(s)) return result;
                for (int i = 0; i < s.Length; i++)
                {
                    var curr = s[i];
                    if (!_map.TryGetValue(char.ToUpperInvariant(curr), out var n))
                    {
                        throw new Exception("Invalid Character");
                    }
                    if (i < s.Length - 1 && _substracters.Contains(curr + s[i + 1].ToString()))
                    {
                        result -= n;
                    }
                    else
                    {
                        result += n;
                    }
                }
                return result;
            }
        }

    }

}