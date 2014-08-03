using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class LongestPalindromeTests
    {
        [Test]
        public void LongestPalindromeTest()
        {
            Console.WriteLine(GetLongestPalindromeLength("suberebus"));

        }



        public static int GetLongestPalindromeLength(string seq)
        {
            int Longest = 0;
            List<int> l = new List<int>();
            int i = 0;
            int palLen = 0;
            int s = 0;
            int e = 0;
            while (i < seq.Length)
            {
                if (i > palLen && seq[i - palLen - 1] == seq[i])
                {
                    palLen += 2;
                    i += 1;
                    continue;
                }
                l.Add(palLen);
                Longest = Math.Max(Longest, palLen);
                s = l.Count - 2;
                e = s - palLen;
                bool found = false;
                for (int j = s; j > e; j--)
                {
                    int d = j - e - 1;
                    if (l[j] == d)
                    {
                        palLen = d;
                        found = true;
                        break;
                    }
                    l.Add(Math.Min(d, l[j]));
                }
                if (!found)
                {
                    palLen = 1;
                    i += 1;
                }
            }
            l.Add(palLen);
            Longest = Math.Max(Longest, palLen);
            return Longest;
        }
    }
}