using System;
using NUnit.Framework;

namespace CodingProblemsTests
{
    public class FloydCycleFinding_Problem
    {
        [Test, Category(category.FundamentalAlgorythms)]
        public void FloydCycleFindingTest()
        {
            var n5 = new Node { Value = 5 };
            var n4 = new Node { Value = 4, Next = n5};
            var n3 = new Node { Value = 3, Next = n4 };
            var n2 = new Node { Value = 2, Next = n3 };
            var n1 = new Node { Value = 1, Next = n2 };
            var n0 = new Node { Value = 0, Next = n1 };

            n5.Next = n3;

            var list = new LinkedList {Head = n0};

            var r = FloydCycleDetection(list);

            Console.WriteLine(r);
        }

        static bool FloydCycleDetection(LinkedList list)
        {
            if (list?.Head?.Next == null) return false;

            var slow = list.Head.Next;
            var fast = list.Head.Next.Next;

            while (slow != null && fast != null)
            {
                if(slow == fast)
                {
                    return true;
                }
                if(fast.Next != null)
                {
                    fast = fast.Next.Next;
                }
                slow = slow.Next;
            }
            return false;
        }


        class LinkedList
        {
            public Node Head;
        }
        class Node
        {
            public int Value;
            public Node Next;
        }
    }

    
}