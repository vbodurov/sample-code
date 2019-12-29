using NUnit.Framework;

namespace CodingProblemsTests
{
    
    [TestFixture]
    public class FirstAndLastPositionSortedArray_Problem
    {
        //https://leetcode.com/problems/find-first-and-last-position-of-element-in-sorted-array/
        [Test]
        [TestCase(new[] { 5, 7, 7, 8, 8, 10 }, 8, new[] { 3, 4 })]
        [TestCase(new[] { 5, 7, 7, 8, 8, 10 }, 6, new[] { -1, -1 })]
        public void FirstAndLastPositionSortedArray(int[] input, int target, int[] expect)
        {
            var result = new Solution().SearchRange(input, target);
            Assert.That(result[0], Is.EqualTo(expect[0]));
            Assert.That(result[1], Is.EqualTo(expect[1]));
        }
        public class Solution
        {
            public int[] SearchRange(int[] nums, int target)
            {
                var res = new[] {-1, -1};
                if (nums == null || nums.Length == 0) return res;
                var first = BinarySearch(nums, target, true);
                if (first == -1) return res;
                var last = BinarySearch(nums, target, false);
                res[0] = first;
                res[1] = last;
                return res;
            }
            int BinarySearch(int[] nums, int target, bool isFirst)
            {
                int left = 0, right = nums.Length -1, res = -1;
                while (left <= right)
                {
                    var mid = (right - left) / 2 + left;
                    if (nums[mid] == target)
                    {
                        res = mid;
                        if (isFirst) right = mid - 1;
                        else left = mid + 1;
                    }
                    else if (nums[mid] < target) left = mid + 1;
                    else right = mid - 1;
                }
                return res;
            }
        }
    }
}