using System;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class Interview_Jan_2_2020
    {
        [Test]
        public void FibonacciTest()
        {
            // Arrange
            var expectArr = new[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 };


            for (var i = 0; i < expectArr.Length; ++i)
            {

                var expect = expectArr[i];

                // Act
                var result = Fibonacci(i + 1);

                // Assert
                if (result != expect)
                    throw new Exception($" for #{i + 1} expected was {expect} actual {result}");
            }


            Console.WriteLine("Test Pass");
        }

        static long Fibonacci(int n)
        {

            if (n == 1) return 0;
            if (n <= 3) return 1;

            var currMin2 = 1;
            var currMin1 = 1;

            var curr = 2;
            for (var i = 3; i < n; ++i)
            {
                curr = currMin2 + currMin1;
                currMin2 = currMin1;
                currMin1 = curr;
            }

            return curr;

        }
    }
}