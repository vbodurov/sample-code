using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MiscCodeTests.Problems
{
    [TestFixture]
    public class ParseInteger_atoi_Problem
    {

        [Test]
        public void atoi_Test()
        {
            var i = atoi("4123");
            Console.WriteLine(i);
        }

        private static readonly IDictionary<char, int> Chars = Enumerable.Range(0, 10).ToDictionary(i => i.ToString()[0], i => i);
        int atoi(String num)
        {
            if (num == null) throw new NullReferenceException("Expecting num to be integer found NULL");
            num = num.Trim();
            if (num == string.Empty) throw new ArgumentException("Expecting num to be integer found empty", "num");
            

            int result = 0; 
            var j = 0;
            for(var i = num.Length - 1; i >= 0; i--)
            {
                var ch = num[i];
                int curr;
                if(!Chars.TryGetValue(ch, out curr ))
                {
                    throw new ArgumentException("Not a number");
                }
                result += curr * (int)Math.Pow(10, j);
                ++j;
            }
            return result;
        }
    }
}