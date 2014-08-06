using System;
using MiscCodeTests.Model;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class MedianFromStreamOfNumbersTests
    {
        [Test]
        public void CanGetMedianFromStreamOfIntegers()
        {
            var array =        new []{ 5, 15, 1, 3, 2, 8, 7, 9, 10, 6, 11, 4 };
            var expectMedian = new[] { 5, 10, 5, 4, 3, 4, 5, 6,  7, 6,  7, 6 };
            var median = array[0];

            var left  = new MaxHeap();
            var right = new MinHeap();

            for (var i = 0; i < array.Length; ++i )
            {
                median = AddNumberAndGetMedian(array[i], median, left, right);
                
                Console.WriteLine(i + " => " + median);
                Assert.That(median, Is.EqualTo(expectMedian[i]));
            }
        }

        public int AddNumberAndGetMedian(int e, int prevMedian, BaseHeap left, BaseHeap right)
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