using System;
using System.Collections.Generic;
using System.Linq;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class Subsets_Problem
    {
        //https://leetcode.com/problems/subsets/
        [Test]
        public void Subsets()
        {
            var subsets = new Solution().Subsets(new[] {1, 2, 3});
            Console.WriteLine(subsets.Select(e => "["+e.JoinStrings(",")+"]").JoinStrings("\n"));
        }
        public class Solution
        {
            public IList<IList<int>> Subsets(int[] nums)
            {
                IList<IList<int>> subsets = new List<IList<int>>();
                GenerateSubsets(0, nums, new List<int>(), subsets);
                return subsets;
            }
            void GenerateSubsets(int index, int[] nums, List<int> current, IList<IList<int>> subsets)
            {
                subsets.Add(new List<int>(current));
                for (int i = index; i < nums.Length; i++)
                {
                    current.Add(nums[i]);
                    GenerateSubsets(i + 1, nums, current, subsets);
                    current.RemoveAt(current.Count - 1);
                }
            }
        }
    }
}