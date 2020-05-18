using System;
using System.Linq;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class CoinChange_Problem
    {
        //https://leetcode.com/problems/coin-change/
        [Test]
        [TestCase(new[] { 1, 2, 5 }, 11, 3)]
        [TestCase(new[] { 2 }, 3, -1)]
        [TestCase(new[] { 5 }, 4, -1)]
        [Category(category.DynamicProgramming)]
        public void CoinChange(int[] coins, int amount, int expect)
        {
            var result = new Solution().CoinChange(coins, amount);
            Assert.That(result, Is.EqualTo(expect));
        }

        class Solution
        {
            public int CoinChange(int[] coins, int amount)
            {
                Array.Sort(coins);
                var dp = Enumerable.Repeat(amount + 1, amount + 1).ToArray();
                dp[0] = 0;
                for (int i = 0; i <= amount; i++)
                {
                    for (int j = 0; j < coins.Length; j++)
                    {
                        if (coins[j] <= i)
                        {
                            dp[i] = Math.Min(dp[i], 1 + dp[i - coins[j]]);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return dp[amount] > amount ? -1 : dp[amount];
            }
        }
    }
}