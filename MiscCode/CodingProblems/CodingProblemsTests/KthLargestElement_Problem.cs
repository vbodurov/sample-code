﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using CodingProblemsTests.Extensions;
using CodingProblemsTests.Structures;
using CodingProblemsTests.Utils;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CodingProblemsTests
{
    [TestFixture()]
    public class KthLargestElement_Problem
    {
        //https://leetcode.com/problems/kth-largest-element-in-an-array/
        [Test, Category(category.SpecificProblems)]
        [TestCase("1")]
        [TestCase("1,1")]
        [TestCase("1,2")]
        [TestCase("1,2,3")]
        [TestCase("1,2,3,4,5,6,7,8,9")]
        [TestCase("1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19")]
        public void FindWithHeap(string str)
        {
            var nums = ListParser.ToListOfInts(str).Shuffle();
            var sorted = nums.OrderByDescending(n => n).ToArray();

            for (int i = 0; i < sorted.Length; i++)
            {
                var kValue = FindKthLargest_WithHeap(nums, i + 1);
                Assert.That(kValue, Is.EqualTo(sorted[i]), $"for index {i}");
            }
        }
        int FindKthLargest_WithHeap(int[] nums, int k)
        {
            var minHeap = new MinHeap<int>();
            foreach (var i in nums)
            {
                minHeap.Add(i);
                if (minHeap.Count > k) minHeap.Pop();
            }
            return minHeap.Pop();
        }
        [Test, Category(category.SpecificProblems)]
        [TestCase("1")]
        [TestCase("1,1")]
        [TestCase("1,2")]
        [TestCase("1,2,3")]
        [TestCase("1,2,3,4,5,6,7,8,9")]
        [TestCase("1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19")]
        public void FindWithQuickSelect(string str)
        {
            var nums = ListParser.ToListOfInts(str).Shuffle();
            var sorted = nums.OrderByDescending(n => n).ToArray();

            for (int i = 0; i < sorted.Length; i++)
            {
                var kValue = FindKthLargest_WithQuickSelect(nums, i + 1);
                Assert.That(kValue, Is.EqualTo(sorted[i]), $"for index {i}");
            }
        }
        int FindKthLargest_WithQuickSelect(int[] nums, int k)
        {
            var result = new QSResult();
            QuickSelect(nums, 0, nums.Length - 1, k, ref result);
            return result.Result;
        }
        void QuickSelect(int[] nums, int left, int right, int k, ref QSResult result)
        {
            if (left <= right)
            {
                var pivotIndex = Partition(nums, left, right);
                if ((pivotIndex + 1) == k) // + 1 because k is number not index
                {
                    result.Result = nums[pivotIndex];
                    result.IsFound = true;
                }
                if (!result.IsFound) QuickSelect(nums, left, pivotIndex - 1, k, ref result);
                if (!result.IsFound) QuickSelect(nums, pivotIndex + 1, right, k, ref result);
            }
        }
        int Partition(int[] nums, int left, int right)
        {
            var pivotValue = nums[right];
            var i = left;
            var j = left;
            for (; j < right; j++)
            {
                if (nums[j] > pivotValue)
                {
                    Swap(nums, i, j);
                    i++;
                }
            }
            Swap(nums, i, right); // bring the pivot in the middle
            return i;
        }
        void Swap(int[] nums, int i, int j)
        {
            var temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
        struct QSResult
        {
            internal int Result;
            internal bool IsFound;
        }
    }
}