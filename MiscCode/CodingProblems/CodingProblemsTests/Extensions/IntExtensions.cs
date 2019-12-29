using System.Collections.Generic;
using CodingProblemsTests.Structures;

namespace CodingProblemsTests.Extensions
{
    public static class IntExtensions
    {
        public static ListNode ToSinglyLinkedList(this IEnumerable<int> ints)
        {
            ListNode prev = null;
            ListNode head = null;
            foreach (var i in ints)
            {
                var curr = new ListNode(i);
                if (head == null) head = curr;
                if (prev != null) prev.next = curr;
                prev = curr;
            }
            return head;
        }
    }
}