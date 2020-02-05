using System;
using System.Collections.Generic;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class LongestPalindrome_Problem
    {
        //https://leetcode.com/problems/longest-palindromic-substring/
        [Test]
        [TestCase("aasdf", "aa")]
        [TestCase("babad", "bab")]
        [TestCase("cbbd", "bb")]
        [TestCase("asdfasdferrefwtewg", "ferref")]
        [TestCase("a", "a")]
        public void LongestPalindromeTest(string s, string expect)
        {
            var result = new Solution().LongestPalindrome(s);
            Assert.That(result, Is.EqualTo(expect));
        }

        public class Solution
        {
            public string LongestPalindrome(string s)
            {
                if (string.IsNullOrEmpty(s)) return "";
                int start = 0, length = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    int len1 = ExpandAroundCenter(s, i, i);
                    int len2 = ExpandAroundCenter(s, i, i + 1);
                    int currLen = Math.Max(len1, len2);
                    if (currLen > length)
                    {
                        start = i - (currLen - 1) / 2;
                        length = currLen;
                    }
                }
                return s.Substring(start, length);
            }
            int ExpandAroundCenter(string s, int left, int right)
            {
                var length = 0;
                while (left >= 0 && right < s.Length && s[left] == s[right])
                {
                    length += left == right ? 1 : 2;
                    --left;
                    ++right;
                    if(left < 0 || right >= s.Length) break;
                }
                return length;
            }
        }
    }
}