using NUnit.Framework;

namespace MiscCodeTests.Problems
{
    [TestFixture]
    public class ReverseLinkedList_Problem
    {

        [Test]
        public void TestReverseNonRecursive()
        {
            var list = GetList();

            ReverseNonRecursive(list);

            Assert.That(ListToString(list), Is.EqualTo("4;3;2;1"));
        }

        [Test]
        public void TestReverseRecursive()
        {
            var list = GetList();

            ReverseRecursive(list);

            Assert.That(ListToString(list), Is.EqualTo("4;3;2;1"));
        }

        private void ReverseNonRecursive(List list)
        {
            var node = list.Head;
            Node newNext = null;
            while (node != null)
            {
                var next = node.Next;
                node.Next = newNext;
                newNext = node;
                if (next == null) list.Head = node;
                node = next;
            }
            
        }
        private void ReverseRecursive(List list)
        {
            ReverseNode(list, list.Head);
        }
        private void ReverseNode(List list, Node node, Node newNext = null)
        {
            var next = node.Next;
            node.Next = newNext;
            newNext = node;
            if (next == null) list.Head = node;
            else ReverseNode(list, next, newNext);
        }


        class List
        {
            internal Node Head;
        }
        class Node
        {
            internal Node Next;
            internal int Value;
            public override string ToString() { return "{"+Value+"}"; }
        }
        private string ListToString(List list)
        {
            var all = new System.Collections.Generic.List<int>();
            var node = list.Head;
            while (node != null)
            {
                all.Add(node.Value);
                node = node.Next;
            }
            return string.Join(";", all);
        }
        private List GetList()
        {
            return new List
            {
                Head = new Node
                {
                    Value = 1,
                    Next = new Node
                    {
                        Value = 2,
                        Next = new Node
                        {
                            Value = 3,
                            Next = new Node
                            {
                                Value = 4
                            }
                        }
                    }
                }
            };
        }
    }
}