using Microsoft.Practices.ObjectBuilder2;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiscCodeTests.Problems
{
    //https://www.geeksforgeeks.org/quicksort-on-singly-linked-list/
    [TestFixture]
    public class QuickSortSingleLinkedList_Problem
    {
        [Test]
        [TestCase(new[] { 3, 11, 8, 7, 5, 2, 1, 12, 9, 6, 4, 10 }, 1)]
        [TestCase(new[] { -5656, 6, 200, 0, -5, 10000 }, 2)]
        [TestCase(new[] { 5, 1, 3, 2, 4 }, 3)]
        [TestCase(new[] { 1, 2, 3, 4 }, 4)]
        [TestCase(new[] { 1 }, 5)]
        [TestCase(new[] { 4, 3, 2, 1 }, 6)]
        public void QuickSortTest(int[] source, int id)
        {
            var numbers = new LinkedList<int>(source);


            var last = numbers.First;
            while (last?.Next != null) last = last.Next;

            QuickSortSingleLinkedListRecursive(numbers.First, last);
            
            foreach (var number in numbers)
                Console.WriteLine(number);

            Assert.That(numbers.JoinStrings(","), Is.EqualTo(source.OrderBy(e=>e).JoinStrings(",")));

        }
        void QuickSortSingleLinkedListRecursive(LinkedListNode<int> start, LinkedListNode<int> end)
        {
            if (start == end) return;

            // For Recusrion
            var pivot = Partition(start, end);
            QuickSortSingleLinkedListRecursive(start, pivot);

            // if pivot is picked and moved to the start, 
            // that means start and pivot is same  
            // so pick from next of pivot 
            if (pivot != null && pivot == start)
                QuickSortSingleLinkedListRecursive(pivot.Next, end);

            // if pivot is in between of the list, 
            // start from next of pivot,  
            // since we have pivot_prev, so we move two nodes 
            else if (pivot?.Next != null)
                QuickSortSingleLinkedListRecursive(pivot.Next.Next, end);
            
        }

        LinkedListNode<int> Partition(LinkedListNode<int> start, LinkedListNode<int> end)
        {
            if (start == end ||
                start == null || end == null)
                return start;

            var current1 = start;
            var current2 = start;
            var nextPivotNode = start;
            var privotData = end.Value;
            // iterate till one before the end,  
            // no need to iterate till the end  
            // because end is pivot 
            while (current2 != end)
            {
                if (current2.Value < privotData)
                {
                    nextPivotNode = current1;
                    Swap(current1, current2);
                    current1 = current1.Next;
                }
                current2 = current2.Next;
                if(current2 == null) break;
            }

            // swap the position of curr i.e. 
            // next suitable index and pivot 
            if(current2 != null) Swap(current1, end);

            return nextPivotNode;
        }
        void Swap(LinkedListNode<int> left, LinkedListNode<int> right)
        {
            var leftVal = left.Value;
            left.Value = right.Value;
            right.Value = leftVal;
        }
    }
}