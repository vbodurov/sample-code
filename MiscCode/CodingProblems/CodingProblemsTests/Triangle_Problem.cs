using System;
using System.Collections.Generic;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class Triangle_Problem
    {
        //https://leetcode.com/problems/triangle/
        [Test]
        public void Triangle()
        {
            var input = new[]
            {
                new[] {2},
                new[] {3,4},
                new[] {6,5,7},
                new[] {4,1,8,3}
            };
            var result = new Solution().MinimumTotal(input);
            Assert.That(result, Is.EqualTo(11));
        }


        public class Solution
        {
            public int MinimumTotal(IList<IList<int>> triangle)
            {
                var dp = new List<int>(triangle[triangle.Count - 1]);
Console.WriteLine($"INI {dp.JoinStrings(",")}");
                for (int i = triangle.Count - 2; i >= 0; i--)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        dp[j] = Math.Min(dp[j], dp[j + 1]) + triangle[i][j];

Console.WriteLine($"{i}@{j} {dp.JoinStrings(",")}");

                    }
                }
                return dp[0];
            }
        }
    }
}