using System;
using System.Collections.Generic;
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
            var input  = new []{ 5, 15, 1, 3, 2, 8, 7, 9, 10, 6, 11, 4 };
            var expect = new [] { 5, 10, 5, 4, 3, 4, 5, 6,  7, 6,  7, 6 };

            var result = findMedian(input);

            if (string.Join(",", result) != string.Join(",", expect))
                throw new Exception($"Expected '{string.Join(",", expect)}' but found '{string.Join(",", result)}'");
        }

        private static int[] findMedian(int[] arr)
        {
            var minHeap = new HeapOfInt((a, b) => a.CompareTo(b));
            var maxHeap = new HeapOfInt((a, b) => -a.CompareTo(b));
            var result = new List<int>();
            var median = arr[0];

            for (var i = 0; i < arr.Length; ++i)
            {
                median = getCurrentMedian(arr[i], median, minHeap, maxHeap);

                Console.WriteLine(median);
                Console.WriteLine(minHeap.ToString() + " | " + maxHeap.ToString());

                result.Add(median);
            }

            return result.ToArray();
        }
        static int getCurrentMedian(int value, int prevMedian, HeapOfInt right, HeapOfInt left)
        {
            //5, 15, 1, 3
            // L: 5, 1
            // R: 15
            // M: 10
            // V: 1


            var rightCount = right.Count;
            var leftCount = left.Count;

            if (rightCount > leftCount)
            {
                if (value < prevMedian)
                {
                    left.Add(value);
                }
                else
                {
                    left.Add(right.Pop());
                    right.Add(value);
                }
            }
            else if (rightCount == leftCount)
            {
                if (value < prevMedian)
                {
                    left.Add(value);
                }
                else
                {
                    right.Add(value);
                }
            }
            else
            { // if(rightCount < leftCount){
                if (value > prevMedian)
                {
                    right.Add(value);
                }
                else
                {
                    right.Add(left.Pop());
                    left.Add(value);
                }
            }
            if (right.Count > left.Count) return right.Peek();
            if (left.Count > right.Count) return left.Peek();
            return average(right.Peek(), left.Peek());

        }
        static int average(int left, int right)
        {
            return (right + left) / 2;
        }

    }
}