using System;
using System.Linq;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class QuickSortRecursive_Problem
    {
        [Test]
        [TestCase("1")]
        [TestCase("1,1")]
        [TestCase("1,1,2")]
        [TestCase("3,8,7,5,2,1,9,6,4")]
        [TestCase("100,-10,1,2,3,5,16")]
        [TestCase("9,8,7")]
        [TestCase("1,2,3")]
        [TestCase("1,0,0,0,1")]
        [TestCase("0,1,1,1,0")]
        public void QuickSortTest(string str)
        {
            int[] numbers = str.Split(',').Select(s => s.Trim()).Select(int.Parse).ToArray();
            var expect = numbers.OrderBy(e => e).JoinStrings(",");

            QuickSortRecursive(numbers, 0, numbers.Length - 1);

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
            Swap(numbers, i, rightIndex); // bring privot in the middle
            return i;
        }

        static void Swap(int[] arr, int a, int b)
        {
            if(a == b) return;
            int temp = arr[b];
            arr[b] = arr[a];
            arr[a] = temp;
        }

        private static void QuickSortRecursive(int[] arr, int leftIndex, int rightIndex)
        {
            // For Recusrion
            if (leftIndex < rightIndex)
            {
                int pivotIndex = Partition(arr, leftIndex, rightIndex);

                if (pivotIndex - 1 > leftIndex)
                    QuickSortRecursive(arr, leftIndex, pivotIndex - 1);

                if (pivotIndex + 1 < rightIndex)
                    QuickSortRecursive(arr, pivotIndex + 1, rightIndex);
            }
        }
    }
}