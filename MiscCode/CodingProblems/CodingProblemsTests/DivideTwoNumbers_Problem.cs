using System;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class DivideTwoNumbers_Problem
    {
        [Test]
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
                int sign = 1;
                long lDividend = (long)dividend;
                long lDivisor = (long)divisor;

                if (dividend < 0)
                {
                    sign = -sign;
                    lDividend = -lDividend;
                }

                if (divisor < 0)
                {
                    lDivisor = -lDivisor;
                    sign = -sign;
                }

                int count = 1;
                while ((lDivisor << 1) <= lDividend)
                {
                    lDivisor <<= 1;
                    count++;
                }

                long result = 0;
                for (int i = 0; i < count; i++)
                {
                    result <<= 1;
                    if (lDividend >= lDivisor)
                    {
                        lDividend -= lDivisor;
                        result |= 1;
                    }
                    lDivisor >>= 1;
                }
                return (int)Math.Min(sign * result, (long)int.MaxValue);
            }
        }
    }
}