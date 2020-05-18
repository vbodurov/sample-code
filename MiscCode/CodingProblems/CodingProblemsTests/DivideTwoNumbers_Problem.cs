using System;
using NUnit.Framework;

namespace CodingProblemsTests
{
    //https://leetcode.com/problems/divide-two-integers/submissions/
    /*
Given two integers dividend and divisor, 
divide two integers without using multiplication, 
division and mod operator.
Return the quotient after dividing dividend by divisor.
The integer division should truncate toward zero.
     */
    [TestFixture]
    public class DivideTwoNumbers_Problem
    {
        [Test, Category(category.SpecificProblems)]
        [TestCase(10, 3, 3)]
        [TestCase( 7,-3,-2)]
        [TestCase(1, 1, 1)]
        [TestCase(int.MaxValue, 1, int.MaxValue)]
        [TestCase(int.MinValue, -1, int.MaxValue)]
        public void DivideTwoNumbers(int dividend, int divisor, int expect)
        {
            var result = new Solution().Divide(dividend, divisor);
            Assert.That(result, Is.EqualTo(expect));
        }

        public class Solution
        {
            public int Divide(int dividend, int divisor)
            {
                if (divisor == 0) return int.MaxValue;
                int sign = dividend > 0 ^ divisor > 0 ? -1 : 1;
                long m = Math.Abs((long)dividend), n = Math.Abs((long)divisor), count = 0;
                for (m -= n; m >= 0; m -= n)
                {
                    count++;
                    if (m == 0) break;
                    for (int subCount = 1; m - (n << subCount) >= 0; subCount++)
                    {
                        m -= n << subCount;
                        count += (int)Math.Pow(2, subCount);
                    }
                }
                return count * sign > int.MaxValue ? int.MaxValue : (int)count * sign;
            }
        }
    }
}