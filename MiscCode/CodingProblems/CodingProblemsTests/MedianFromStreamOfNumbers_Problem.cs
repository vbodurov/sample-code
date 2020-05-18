using System;
using CodingProblemsTests.Structures;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class MedianFromStreamOfNumbers_Problem
    {
        [Test, Category(category.FundamentalAlgorythms)]
        public void CanGetMedianFromStreamOfIntegers()
        {
            var array =        new []{ 5, 15, 1, 3, 2, 8, 7, 9, 10, 6, 11, 4 };
            var expectMedian = new[] { 5, 10, 5, 4, 3, 4, 5, 6,  7, 6,  7, 6 };
            var median = array[0];

            var left  = new Max_Heap();
            var right = new Min_Heap();

            for (var i = 0; i < array.Length; ++i )
            {
                median = AddNumberAndGetMedian(array[i], median, left, right);
                
                Console.WriteLine(i + " => " + median);
                Assert.That(median, Is.EqualTo(expectMedian[i]));
            }
        }

        int AddNumberAndGetMedian(int e, int prevMedian, Base_Heap left, Base_Heap right)
        {
            var leftCount = left.GetCount();
            var rightCount = right.GetCount();

            if(leftCount > rightCount)
            {
                if (e < prevMedian) 
                {
                    right.Insert(left.ExtractTop());

                    left.Insert(e);
                }
                else
                {
                    right.Insert(e);
                }

                return Average(left.GetTop(), right.GetTop());
            }

            if (leftCount == rightCount)
            {
                if (e < prevMedian) 
                {
                    left.Insert(e);
                    return left.GetTop();
                }
                right.Insert(e);
                return right.GetTop();
            }
            
            
            // if(leftCount < rightCount) ...
            
            if (e < prevMedian)
            {
                left.Insert(e);
            }
            else
            {
                left.Insert(right.ExtractTop());

                right.Insert(e);
            }

            return Average(left.GetTop(), right.GetTop());
            
        }

        int Average(int a, int b) { return (a + b) / 2; }
    }
}