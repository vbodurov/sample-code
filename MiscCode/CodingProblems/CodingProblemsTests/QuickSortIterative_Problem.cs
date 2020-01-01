using System;
using System.Collections.Generic;
using System.Linq;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class QuickSortIterative_Problem
    {
        [Test]
        [TestCase("1")]
        [TestCase("1,1")]
        [TestCase("1,1,2")]
        [TestCase("3,8,7,5,2,1,9,6,4")]
        [TestCase("100,-10,1,2,3,5,16")]
        [TestCase("11,55,1,2,5,5,111,-1")]
        [TestCase("9,8,7")]
        [TestCase("1,2,3")]
        [TestCase("1,0,0,0,1")]
        [TestCase("0,1,1,1,0")]
        public void QuickSortTest(string str)
        {
            int[] numbers = str.Split(',').Select(s => s.Trim()).Select(int.Parse).ToArray();
            var expect = numbers.OrderBy(e => e).JoinStrings(",");

            QuickSortIterative(numbers, 0, numbers.Length - 1);
            
            Console.WriteLine(numbers.JoinStrings(","));
            Assert.That(numbers.JoinStrings(","), Is.EqualTo(expect));
        }
        static int Partition(int[] numbers, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            int j = leftIndex;
            int pivotValue = numbers[rightIndex];
            for (; j < rightIndex; ++j)
            {
                if (numbers[j] <= pivotValue)
                {
                    Swap(numbers, i, j);
                    ++i;
                }
            }
            Swap(numbers, i, rightIndex);
            return i;
        }

        static void Swap(int[] arr, int a, int b)
        {
            if (a == b) return;
            int temp = arr[b];
            arr[b] = arr[a];
            arr[a] = temp;
        }

        static void QuickSortIterative(int[] numbers, int left, int right)
        {

            if (left >= right)
                return; // Invalid index range

            var queue = new Queue<(int left, int right)>();

            queue.Enqueue((left: left, right: right));

            while (queue.Count > 0)
            {
                var first = queue.Dequeue();
                left = first.left;
                right = first.right;
                
                int pivotIndex = Partition(numbers, left, right);

                if (left < pivotIndex - 1)
                {
                    queue.Enqueue((left: left, right: pivotIndex - 1));
                }

                if (pivotIndex + 1 < right)
                {
                    queue.Enqueue((left: pivotIndex + 1, right: right));
                }
            }
        }
    }
}