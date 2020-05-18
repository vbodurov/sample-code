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

            var tortoise = list.Head.Next;
            var hare = list.Head.Next.Next;

            while (tortoise != null && hare != null)
            {
                if(tortoise == hare)
                {
                    return true;
                }
                if(hare.Next != null)
                {
                    hare = hare.Next.Next;
                }
                tortoise = tortoise.Next;
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