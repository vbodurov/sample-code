using System.Collections.Generic;
using System.Linq;
using CodingProblemsTests.Extensions;
using CodingProblemsTests.Structures;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class ReverseSingleLinkedList_Problem
    {

        [Test]
        [TestCase(new[]{0,1,2,3,4,5,6,7,8,9})]
        [TestCase(new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 })]
        public void Reverse_SinglyLinkedList_Iterative(int[] array)
        {
            var head = array.ToSinglyLinkedList();

            ListNode prev = null;
            var curr = head;
            while (curr != null)
            {
                var next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }
            head = prev;

            Assert.That(head.ToArray().JoinStrings(","), Is.EqualTo(array.Reverse().JoinStrings(",")));
        }


        [Test]
        [TestCase(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        [TestCase(new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 })]
        public void Reverse_SinglyLinkedList_Recursive(int[] array)
        {
            var head = array.ToSinglyLinkedList();

            ListNode prev = null;
            ReverseListNode(head, ref prev);

            Assert.That(prev.ToArray().JoinStrings(","), Is.EqualTo(array.Reverse().JoinStrings(",")));
        }

        void ReverseListNode(ListNode curr, ref ListNode prev)
        {
            if (curr == null) return;

            var next = curr.next;
            curr.next = prev;
            prev = curr;
            curr = next;

            ReverseListNode(curr, ref prev);
        }
    }
}