using System.Collections.Generic;
using CodingProblemsTests.Structures;

namespace CodingProblemsTests.Extensions
{
    public static class ListNodeExtensions
    {
        public static int[] ToArray(this ListNode head)
        {
            var result = new List<int>();
            var node = head;
            while (node != null)
            {
                result.Add(node.val);
                node = node.next;
            }
            return result.ToArray();
        }
    }
}