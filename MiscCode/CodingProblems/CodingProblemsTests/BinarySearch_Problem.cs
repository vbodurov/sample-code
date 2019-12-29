using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class BinarySearch_Problem
    {
        [Test]
        [TestCase(new[] { 12, 13, 14, 15 }, -1000)]
        [TestCase(new[] { 12, 13, 14, 15 }, 1000)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15 }, 9)]
        [TestCase(new[] { 1 }, 8)]
        public void CanFindElementIterative(int[] arr, int notThere)
        {

            Assert.That(TryFindWithBinarySearchIterative(arr, notThere), Is.False);

            foreach (var curr in arr)
                Assert.That(TryFindWithBinarySearchIterative(arr, curr), Is.True);

        }
        static bool TryFindWithBinarySearchIterative(int[] arr, int target)
        {
            if (arr == null || arr.Length == 0) return false;
            
            var len = arr.Length;
            var l = 0;
            var r = len - 1;
            while (l < r)
            {
                var pivotInx = (r - l) / 2 + l;
                var pivotVal = arr[pivotInx];

                if (pivotVal == target) return true;
                if (pivotVal < target) l = pivotInx + 1;
                else if (pivotVal > target) r = pivotInx - 1;
            }
            return arr[l] == target;
        }

        [Test]
        public void CanFindElementRecursive()
        {
            const int notThere = 9;
            var arr = new[] {1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15};

            Assert.That(TryFindWithBinarySearchRecursive(arr, notThere), Is.False);
            
            foreach (var curr in arr)
                Assert.That(TryFindWithBinarySearchRecursive(arr, curr), Is.True);
            
        }

        static bool TryFindWithBinarySearchRecursive(int[] arr, int target, int minIndex = -1, int maxIndex = -1)
        {
            if (minIndex < 0) minIndex = 0;
            if (maxIndex < 0) maxIndex = arr.Length - 1;

            // test if array is empty
            if (maxIndex < minIndex)
            {
                // set is empty, so return value showing not found
                return false;
            }
            
            // calculate midpoint to cut set in half
            var midIndex = (int)(minIndex + (maxIndex - minIndex) / 2f);

            // three-way comparison
            // key is in lower subset
            if (arr[midIndex] == target)
            {
                return true;
            }
            if (arr[midIndex] > target && TryFindWithBinarySearchRecursive(arr, target, minIndex, midIndex - 1))
            {
                return true;
            }
            // key is in upper subset
            if (arr[midIndex] < target && TryFindWithBinarySearchRecursive(arr, target, midIndex + 1, maxIndex))
            {
                return true;
            }
            return false;
        }
    }
}