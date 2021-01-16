using System.Linq;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class RemoveElement_ArrayProblem
    {
        // https://leetcode.com/problems/remove-element/
        [Test, Category(category.SpecificProblems)]
        [TestCase(new[] { 3, 2, 2, 3 }, 3, 2)]
        [TestCase(new[] { 0, 1, 2, 2, 3, 0, 4, 2 }, 2, 5)]
        [TestCase(new[] { 1 }, 1, 0)]
        public void RemoveElement(int[] array, int removeValue, int expectLength)
        {
            // Arrange
            var expected = array.Where(e => e != removeValue).OrderBy(n => n).Take(expectLength).JoinStrings(",");

            // Act
            var length = new Solution().RemoveElement(array, removeValue);

            // Assert
            Assert.That(length, Is.EqualTo(expectLength));
            Assert.That(array.Take(length).OrderBy(n=>n).JoinStrings(","), 
                Is.EqualTo(expected));
        }
        public class Solution
        {
            public int RemoveElement(int[] nums, int valToRemove)
            {
                int i = -1;
                foreach (var curr in nums)
                {
                    if (curr != valToRemove)
                        nums[++i] = curr;
                }
                return i + 1;
            }
        }
    }
}