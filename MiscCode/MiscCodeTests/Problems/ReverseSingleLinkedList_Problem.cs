using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using NUnit.Framework;

namespace MiscCodeTests.Problems
{
    [TestFixture]
    public class ReverseSingleLinkedList_Problem
    {
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        [Test]
        [TestCase(new[]{0,1,2,3,4,5,6,7,8,9})]
        public void ReverseSingleLinkedList(int[] array)
        {
            ListNode prev = null;
            ListNode head = null;
            foreach (var i in array)
            {
                var curr = new ListNode(i);
                if (head == null) head = curr;
                if (prev != null) prev.next = curr;
                prev = curr;
            }
            head = ReverseList(head);

            var result = new List<int>();
            var node = head;
            while (node != null)
            {
                result.Add(node.val);
                node = node.next;
            }
            Assert.That(result.JoinStrings(","), Is.EqualTo(array.Reverse().JoinStrings(",")));
        }
        ListNode ReverseList(ListNode root)
        {
            var curr = root;
            ListNode tmp2 = null;
            while (curr != null)
            {
                var tmp1 = curr.next;
                curr.next = tmp2;
                tmp2 = curr;
                curr = tmp1;
            }
            return tmp2;
        }
    }
}