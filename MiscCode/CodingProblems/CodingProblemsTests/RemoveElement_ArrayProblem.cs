using System.Linq;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class RemoveElement_ArrayProblem
    {
        // https://leetcode.com/problems/remove-element/
        [Test]
        [TestCase(new[] { 3, 2, 2, 3 }, 3, 2)]
        [TestCase(new[] { 0, 1, 2, 2, 3, 0, 4, 2 }, 2, 5)]
        [TestCase(new[] { 1 }, 1, 0)]
        public void RemoveElement(int[] array, int removeValue, int expectLength)
        {
            var length = new Solution().RemoveElement(array, removeValue);

            Assert.That(length, Is.EqualTo(expectLength));
            Assert.That(array.Take(length).OrderBy(n=>n).JoinStrings(","), 
                Is.EqualTo(array.Where(e => e != removeValue).OrderBy(n => n).JoinStrings(",")));
        }
        public class Solution
        {
            public int RemoveElement(int[] nums, int val)
            {
                if (nums == null || nums.Length == 0) return 0;

                var left = 0;
                var right = nums.Length - 1;
                var found = 0;
                while (true)
                {
                    while (left < nums.Length && nums[left] != val)
                    {
                        left++;
                    }
                    while (right >= 0 && nums[right] == val)
                    {
                        right--;
                        ++found;
                    }
                    if (left >= right) return nums.Length - found;
                    if (nums[left] == val && nums[right] != val)
                    {
                        Swap(nums, left, right);
                    }
                }
            }
            static void Swap(int[] list, int index1, int index2)
            {
                int tmp = list[index1];
                list[index1] = list[index2];
                list[index2] = tmp;
            }
        }
    }
}