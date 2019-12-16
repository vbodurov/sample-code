using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace MiscCodeTests.Problems
{
    [TestFixture]
    public class Fibonacci_Problem
    {
        [Test]
        public void CanComputeRecursive()
        {
            var sw = Stopwatch.StartNew();
            var result = FibRec(40);
            sw.Stop();
            Assert.That(result, Is.EqualTo(102334155));
            Console.WriteLine("Elapsed:"+sw.ElapsedMilliseconds+" result: " + result);
            //Elapsed:1133 result: 102334155

        }
        [Test]
        public void CanComputeRecursiveWithCache()
        {
            var sw = Stopwatch.StartNew();
            var result = FibRecWithCache(100);
            sw.Stop();
            Assert.That(result, Is.EqualTo(354224848179261915075M));

            Console.WriteLine("Elapsed:" + sw.ElapsedMilliseconds + " result: " + result);
            //Elapsed:7 result: 354224848179261915075
        }
        [Test]
        public void CanComputeLinear()
        {
            var sw = Stopwatch.StartNew();
            var result = FibLinear(100);
            sw.Stop();
            Console.WriteLine("Elapsed:" + sw.ElapsedMilliseconds + " result: " + result);
            Assert.That(result, Is.EqualTo(354224848179261915075M));
            //Elapsed:0 result: 12586269025
        }
        [Test]
        public void CanComputeMatrix()
        {
            var sw = Stopwatch.StartNew();
            var result = FibMatrix(100);
            sw.Stop();
            Console.WriteLine("Elapsed:" + sw.ElapsedMilliseconds + " result: " + result);
            Assert.That(result, Is.EqualTo(354224848179261915075M));
            //Elapsed:5 result: 354224848179261915075
        }


        static decimal FibLinear(int n)
        {
            if (n == 0) return 0;
            if (n <= 2) return 1;
            decimal fMin2 = 1;
            decimal fMin1 = 1;
            decimal curr = 2;
            for(var i = 3; i <= n; ++i)
            {
                curr = fMin2 + fMin1;
                fMin2 = fMin1;
                fMin1 = curr;
            }
            return curr;
        }
        static decimal FibRecWithCache(int n, Dictionary<int, decimal> cache = null)
        {
            if (cache == null) cache = new Dictionary<int, decimal>();
            if (n == 0) return 0;
            if (n <= 2) return 1;
            return cache.TryGetValue(n, out var found) 
                    ? found
                    : cache[n] = FibRecWithCache(n - 1, cache) + FibRecWithCache(n - 2, cache);
        }
        static decimal FibRec(int n)
        {
            if (n == 0) return 0;
            if (n <= 2) return 1;
            return FibRec(n - 1) + FibRec(n - 2);
        }
        // O(log2 n)
        static decimal FibMatrix(int n)
        {

            int nAbs = Math.Abs(n);
            if (nAbs == 0) return 0;
            if (nAbs <= 2) return 1;

            var number = new decimal[,] { { 1, 1 }, { 1, 0 } };
            var result = new decimal[,] { { 1, 1 }, { 1, 0 } };

            while (nAbs > 0)
            {
                if (nAbs % 2 == 1) result = MultiplyMatrix(result, number);
                number = MultiplyMatrix(number, number);
                nAbs /= 2;
            }
            return result[1, 1] * ((n < 0) ? -1 : 1);
        }
        static decimal[,] MultiplyMatrix(decimal[,] mat1, decimal[,] mat2)
        {
            return new [,] {
                { mat1[0,0]*mat2[0,0] + mat1[0,1]*mat2[1,0], mat1[0,0]*mat2[0,1] + mat1[0,1]*mat2[1,1] },
                { mat1[1,0]*mat2[0,0] + mat1[1,1]*mat2[1,0], mat1[1,0]*mat2[0,1] + mat1[1,1]*mat2[1,1] }
            };
        }
    }
}