using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class QuickSortIterative_Problem
    {
        [Test]
        public void QuickSortTest()
        {
            int[] numbers = { 3, 8, 7, 5, 2, 1, 9, 6, 4 };


            QuickSortIterative(numbers, 0, numbers.Length - 1);


            for (int i = 0; i < 9; i++)
                Console.WriteLine(numbers[i]);
        }

        private static int Partition(int[] numbers, int left, int right)
        {
            int pivot = numbers[left];
            while (true)
            {
                while (numbers[left] < pivot)
                    left++;

                while (numbers[right] > pivot)
                    right--;

                if (left < right)
                {
                    Swap(numbers, left, right);
                }
                else
                {
                    return right;
                }
            }
        }

        private static void Swap(int[] arr, int a, int b)
        {
            int temp = arr[b];
            arr[b] = arr[a];
            arr[a] = temp;
        }

        struct QuickPosInfo
        {
            public int left;
            public int right;
        };

        private static void QuickSortIterative(int[] numbers, int left, int right)
        {

            if (left >= right)
                return; // Invalid index range

            var list = new List<QuickPosInfo>();

            QuickPosInfo info;
            info.left = left;
            info.right = right;
            list.Add(info);

            while (true)
            {
                if (list.Count == 0)
                    break;

                left = list[0].left;
                right = list[0].right;
                list.RemoveAt(0);

                int pivot = Partition(numbers, left, right);

                if (pivot > 1)
                {
                    info.left = left;
                    info.right = pivot - 1;
                    list.Add(info);
                }

                if (pivot + 1 < right)
                {
                    info.left = pivot + 1;
                    info.right = right;
                    list.Add(info);
                }
            }
        }
    }
}