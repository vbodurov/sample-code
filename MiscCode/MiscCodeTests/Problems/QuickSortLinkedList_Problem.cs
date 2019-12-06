using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MiscCodeTests.Problems
{
    [TestFixture]
    public class QuickSortLinkedList_Problem
    {
        [Test]
        public void QuickSortTest()
        {
            var numbers = new LinkedList<int>(new []{ 3, 11, 8, 7, 5, 2, 1, 12, 9, 6, 4, 10 });


            QuickSortRecursive(numbers.First, numbers.Last);


            foreach (var number in numbers)
                Console.WriteLine(number);
                
        }
        void QuickSortRecursive(LinkedListNode<int> left, LinkedListNode<int> right)
        {
            // For Recusrion
            if (left != right)
            {
                var pivot = Partition(left, right);

                if (pivot != null && pivot != left && pivot != left.Next)
                    QuickSortRecursive(left, pivot.Previous);

                if (pivot != null && pivot != right && pivot != right.Previous)
                    QuickSortRecursive(pivot.Next, right);
            }
        }

        LinkedListNode<int> Partition(LinkedListNode<int> left, LinkedListNode<int> right)
        {
            var pivot = left;
            while (true)
            {
                while (left.Value < pivot.Value && left.Next != null && left != right)
                    left = left.Next;
                
                while (right.Value > pivot.Value && right.Previous != null && left != right)
                    right = right.Previous;
                
                if(left == right) return right;
                
                Swap(left, right);
            }
        }
        void Swap(LinkedListNode<int> left, LinkedListNode<int> right)
        {
            var leftVal = left.Value;
            var rightVal = right.Value;
            left.Value = rightVal;
            right.Value = leftVal;
        }
    }
}