using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class ParseInteger_atoi_Problem
    {
        //https://leetcode.com/problems/string-to-integer-atoi/

        [Test]
        [TestCase("4123", 4123)]
        [TestCase("42", 42)]
        [TestCase("  -42", -42)]
        [TestCase("4193 with words", 4193)]
        [TestCase("words and 987", 0)]
        [TestCase("-91283472332", int.MinValue)]
        [TestCase("123283472332", int.MaxValue)]
        [TestCase("3.14159", 3)]
        [TestCase(".1", 0)]
        [TestCase("+1", 1)]
        [TestCase("  0000000000012345678", 12345678)]
        [TestCase("+-2", 0)]
        [TestCase("-+1", 0)]
        [TestCase("  -0012a42", -12)]
        [TestCase("  +b12102370352", 0)]
        public void atoi_Test(string str, int expect)
        {
            var i = atoi(str);
            Console.WriteLine(i);
            Assert.That(i, Is.EqualTo(expect));
        }

        static readonly IDictionary<char, int> Chars = Enumerable.Range(0, 10).ToDictionary(i => i.ToString()[0], i => i);
        int atoi(string num)
        {
            if (num == null) throw new NullReferenceException("Expecting num to be integer found NULL");

            num = num.Trim().Split('.')[0];
            
            var sign = num.StartsWith("-") ? -1 : 1;

            if (num.StartsWith('+')) num = num.Substring(1);

            var start = sign < 0 ? 1 : 0;

            int result = 0; 
            var j = 0;
            for(var i = num.Length - 1; i >= start; i--)
            {
                var ch = num[i];
                if(!Chars.TryGetValue(ch, out var curr))
                {
                    result = 0;
                    j = 0;
                    continue;
                }
                try
                {
                    var candidate = curr * Math.Pow(10, j);
                    checked
                    {
                        result += (int)candidate;
                    }
                }
                catch (OverflowException)
                {
                    result = sign < 0 ? int.MinValue  : int.MaxValue;
                }
                
                ++j;
            }
            return result * sign;
        }
    }
}