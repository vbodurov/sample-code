using System;
using System.Text;
using NUnit.Framework;

namespace MiscCodeTests.Problems
{
    [TestFixture]
    public class CountAndSay_Problem
    {
        [Test]
        [TestCase(1, "1")]
        [TestCase(2, "11")]
        [TestCase(3, "21")]
        [TestCase(4, "1211")]
        [TestCase(5, "111221")]
        [TestCase(6, "312211")]
        [TestCase(7, "13112221")]
        [TestCase(8, "1113213211")]
        [TestCase(9, "31131211131221")]
        public void TestCountAndSay(int number, string expect)
        {
            var result = new Solution().CountAndSay(number);
            Assert.That(result, Is.EqualTo(expect));
        }

        public class Solution
        {
            public string CountAndSay(int n)
            {
                if (n < 1 || n > 30) throw new ArgumentException();
                if (n == 1) return "1";
                var digits = CountAndSay(n - 1);
                char prev = 'X';
                var sb = new StringBuilder();
                var count = 0;
                for (int i = 0; i < digits.Length; i++)
                {
                    var curr = digits[i];
   
                    if (i > 0 && curr != prev)
                    {
                        sb.Append(count);
                        sb.Append(prev);
                        count = 0;
                    }
                    
                    ++count;
                    prev = curr;
                }

                if (count > 0)
                {
                    sb.Append(count);
                    sb.Append(prev);
                }
                return sb.ToString();
            }
        }
    }
}