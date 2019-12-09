using NUnit.Framework;

namespace MiscCodeTests.Problems
{
    [TestFixture]
    public class SortedArrayToBinaryTree_Problem
    {
        [Test]
        public void SortedArrayToBinaryTree()
        {
            var arr = new[] {-10, -3, 0, 5, 9};

            var node = new Solution().SortedArrayToBST(arr);

            Assert.That(node.val, Is.EqualTo(0));
            Assert.That(node.left.val, Is.EqualTo(-10));
            Assert.That(node.right.val, Is.EqualTo(5));
            Assert.That(node.left.left, Is.EqualTo(null));
            Assert.That(node.left.right.val, Is.EqualTo(-3));
            Assert.That(node.right.left, Is.EqualTo(null));
            Assert.That(node.right.right.val, Is.EqualTo(9));
        }
        public class Solution
        {
            public TreeNode SortedArrayToBST(int[] nums)
            {
                if (nums.Length == 0) return null;
                return ConstructTreeFromArray(nums, 0, nums.Length - 1);
            }
            TreeNode ConstructTreeFromArray(int[] nums, int left, int right)
            {
                if (left > right) return null;
                var midpoint = (right - left) / 2 + left;
                return new TreeNode(nums[midpoint])
                {
                    left = ConstructTreeFromArray(nums, left, midpoint - 1),
                    right = ConstructTreeFromArray(nums, midpoint + 1, right)
                };
            }
        }
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }
    }
}