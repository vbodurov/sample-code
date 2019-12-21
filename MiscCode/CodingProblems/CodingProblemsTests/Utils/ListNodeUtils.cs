using System.Collections.Generic;
using CodingProblemsTests.Extensions;
using CodingProblemsTests.Structures;

namespace CodingProblemsTests.Utils
{
    public static class ListNodeUtils
    {
        public static ListNode Create(int[] source)
        {
            ListNode head = null;
            ListNode prev = null;
            foreach (var n in source)
            {
                ListNode node = new ListNode(n);
                if (head == null) head = node;
                if (prev != null) prev.next = node;
                prev = node;
            }
            return head;
        }

        public static int[] ToArray(ListNode node)
        {
            var list = new List<int>();
            while (node != null)
            {
                list.Add(node.val);
                node = node.next;
            }
            return list.ToArray();
        }
        public static string ToString(ListNode node)
        {
            return ToArray(node).JoinStrings(",");
        }
    }
}