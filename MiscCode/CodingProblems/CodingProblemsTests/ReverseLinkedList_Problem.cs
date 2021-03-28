using System;
using System.Collections.Generic;
using System.Linq;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class ReverseLinkedList_Problem
    {

        [Test, Category(category.FundamentalAlgorythms)]
        [TestCase("1,2,3,4,5,6,7")]
        [TestCase("1,1,2")]
        public void TestReverseLinkedListIterative(string inputStr)
        {
            var input = inputStr.Split(',').Select(s => int.Parse(s.Trim())).ToArray();

            var head = GetListHead(input);

            var newHead = ReverseLinkedListIterative(head);

            Assert.That(ListToString(newHead), Is.EqualTo(input.Reverse().JoinStrings(",")));
        }

        [Test, Category(category.FundamentalAlgorythms)]
        [TestCase("1,2,3,4,5,6,7")]
        [TestCase("1,1,2")]
        public void TestReverseLinkedListRecursive(string inputStr)
        {
            var input = inputStr.Split(',').Select(s => int.Parse(s.Trim())).ToArray();

            var head = GetListHead(input);

            var newHead = ReverseLinkedListRecursive(head);

            Assert.That(ListToString(newHead), Is.EqualTo(input.Reverse().JoinStrings(",")));
        }
        [Test, Category(category.FundamentalAlgorythms)]
        [TestCase("1,2,4,5,6,8,9", "1,4,2,5,8,6,9")]
        [TestCase("2,4,8,9,8,4,2", "8,4,2,9,2,4,8")]
        [TestCase("2,4,8,9,1,3,8,4,2", "8,4,2,9,1,3,2,4,8")]
        [TestCase("1,3,2,4,8,9,8,4,3,1", "1,3,8,4,2,9,4,8,3,1")]
        [TestCase("8,4,2", "2,4,8")]
        [TestCase("1,3,7", "1,3,7")]
        public void TestReverseLinkedListSectionsIterative(string inputStr, string expect)
        {
            var input = inputStr.Split(',').Select(s => int.Parse(s.Trim())).ToArray();

            var head = GetListHead(input);

            var newHead = ReverseLinkedListSections(head, null);

            Assert.That(ListToString(newHead), Is.EqualTo(expect));
        }

        #region Algorythms
        Node ReverseLinkedListSections(Node head, Node previous)
        {
            if (head == null) return null;

            Node current = head;
            while (current != null && current.Value % 2 == 0)
            {
                var newCurrent = current.Next;
                current.Next = previous;
                previous = current;
                current = newCurrent;
            }

            if (current != head) { // if there was change
                head.Next = current;
                ReverseLinkedListSections(current, null);
                return previous;
            } else { // process odd values
                head.Next = ReverseLinkedListSections(head.Next, head);
                return head;
            }
        }
        Node ReverseLinkedListIterative(Node head)
        {
            var node = head;
            Node previous = null;
            while (node != null)
            {
                var next = node.Next;
                node.Next = previous;
                previous = node;
                node = next;
            }
            return previous;
        }
        Node ReverseLinkedListRecursive(Node node)
        {
            if (node == null || node.Next == null) return node;
            Node parent = ReverseLinkedListRecursive(node.Next);
            node.Next.Next = node;
            node.Next = null;
            return parent;
        }
        #endregion


        #region Test Helpers
        class Node
        {
            internal Node Next;
            internal int Value;
            public override string ToString() { return "{" + Value + "}"; }
        }
        static string ListToString(Node head)
        {
            var all = new System.Collections.Generic.List<int>();
            var node = head;
            while (node != null)
            {
                all.Add(node.Value);
                node = node.Next;
            }
            return string.Join(",", all);
        }
        static Node GetListHead(IList<int> arr)
        {
            if (arr == null) return null;

            Node head = null;
            Node previous = null;
            foreach (var i in arr)
            {
                var current = new Node { Value = i };
                if (previous != null) previous.Next = current;
                previous = current;
                head ??= current;
            }

            return head;

        }
        #endregion

    }
}