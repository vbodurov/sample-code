using System.Text;
using NUnit.Framework;

namespace CodingProblemsTests
{
    //https://leetcode.com/problems/longest-common-prefix/
    [TestFixture]
    public class LongestCommonPrefix_Problem
    {
        [Test]
        [TestCase("qq;q", "q")]
        [TestCase("flower;flow;flight", "fl")]
        [TestCase("dog;racecar;car", "")]
        public void LongestCommonPrefix(string input, string expect)
        {
            var result = new Solution().LongestCommonPrefix(input.Split(';'));
            Assert.That(result, Is.EqualTo(expect));
        }

        public class Solution
        {
            public string LongestCommonPrefix(string[] strs)
            {
                if (strs == null || strs.Length == 0) return "";
                var sb = new StringBuilder();
                for (int i = 0; i < strs[0].Length; i++)
                {
                    var ch = strs[0][i];
                    for (int j = 1; j < strs.Length; j++)
                    {
                        if(i >= strs[j].Length || strs[j][i] != ch) return sb.ToString();
                    }
                    sb.Append(ch);
                }
                return sb.ToString();
            }
        }
    }
}