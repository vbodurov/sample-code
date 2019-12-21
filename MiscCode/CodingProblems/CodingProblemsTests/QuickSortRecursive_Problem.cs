using System;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class QuickSortRecursive_Problem
    {
        [Test]
        public void QuickSortTest()
        {
            int[] numbers = { 3, 8, 7, 5, 2, 1, 9, 6, 4 };


            QuickSortRecursive(numbers, 0, numbers.Length - 1);


            for (int i = 0; i < 9; i++)
                Console.WriteLine(numbers[i]);
        }


        private static int Partition(int[] numbers, int leftIndex, int rightIndex)
        {
            int pivotValue = numbers[leftIndex];
            while (true)
            {
                while (numbers[leftIndex] < pivotValue)
                    leftIndex++;

                while (numbers[rightIndex] > pivotValue)
                    rightIndex--;

                if (leftIndex < rightIndex)
                {
                    Swap(numbers, leftIndex, rightIndex);
                }
                else
                {
                    return rightIndex;
                }
            }
        }

        private static void Swap(int[] arr, int a, int b)
        {
            int temp = arr[b];
            arr[b] = arr[a];
            arr[a] = temp;
        }

        private static void QuickSortRecursive(int[] arr, int leftIndex, int rightIndex)
        {
            // For Recusrion
            if (leftIndex < rightIndex)
            {
                int pivotIndex = Partition(arr, leftIndex, rightIndex);

                if (pivotIndex > 1)
                    QuickSortRecursive(arr, leftIndex, pivotIndex - 1);

                if (pivotIndex + 1 < rightIndex)
                    QuickSortRecursive(arr, pivotIndex + 1, rightIndex);
            }
        }
    }
}