using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class DuplicatesInArray_Problem
    {
        // https://leetcode.com/problems/find-all-duplicates-in-an-array/
        [Test, Category(category.SpecificProblems)]
        [TestCase(new[]{ 4, 3, 2, 7, 8, 2, 3, 1 }, new[]{ 2, 3 })]
        public void DuplicatesInArray(int[] input, int[] expect)
        {
            var result = new Solution().FindDuplicates(input);
            Assert.That(result, Is.EquivalentTo(expect));
        }

        public class Solution
        {
            public IList<int> FindDuplicates(int[] nums)
            {
                var result = new List<int>();
                for (int i = 0; i < nums.Length; i++)
                {
                    var absVal = Math.Abs(nums[i]);
                    var index = absVal - 1;
                    if(nums[index] < 0) result.Add(absVal);
                    nums[index] = -nums[index];
                }
                return result;
            }
        }

    }
}