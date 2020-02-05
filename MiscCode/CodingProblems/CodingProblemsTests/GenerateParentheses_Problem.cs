using System.Collections.Generic;
using System.Linq;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture()]
    public class GenerateParentheses_Problem
    {
        //https://leetcode.com/problems/generate-parentheses/
        [Test]
        public void GenerateParentheses()
        {
            // Arrange
            var n = 3;
            var expect = new []{
                "((()))",
                "(()())",
                "(())()",
                "()(())",
                "()()()"
            };
            // Act
            var result = new Solution().GenerateParenthesis(n);

            // Assert
            string ToDebufString(IList<string> list) 
                => list.OrderBy(e => e).JoinStrings(";");
            Assert.That(
                ToDebufString(result),
                Is.EqualTo(ToDebufString(expect))
                );
        }

        class Solution
        {
            public IList<string> GenerateParenthesis(int n)
            {
                IList<string> result = new List<string>();
                Backtrack(result, "", 0, 0, n);
                return result;
            }
            void Backtrack(IList<string> result, string cur, int open, int close, int max)
            {
                if (cur.Length == max * 2)
                {
                    result.Add(cur);
                    return;
                }

                if (open < max) Backtrack(result, cur + "(", open + 1, close, max);
                if (close < open) Backtrack(result, cur + ")", open, close + 1, max);
            }
        }
    }
}