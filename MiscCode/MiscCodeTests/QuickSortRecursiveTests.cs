using System;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class QuickSortRecursiveTests
    {
        [Test]
        public void QuickSortTest()
        {
            int[] numbers = { 3, 8, 7, 5, 2, 1, 9, 6, 4 };


            QuickSortRecursive(numbers, 0, numbers.Length - 1);


            for (int i = 0; i < 9; i++)
                Console.WriteLine(numbers[i]);
        }


        private static int Partition(int[] numbers, int left, int right)
        {
            int pivotValue = numbers[left];
            while (true)
            {
                while (numbers[left] < pivotValue)
                    left++;

                while (numbers[right] > pivotValue)
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

        private static void QuickSortRecursive(int[] arr, int left, int right)
        {
            // For Recusrion
            if (left < right)
            {
                int pivotIndex = Partition(arr, left, right);

                if (pivotIndex > 1)
                    QuickSortRecursive(arr, left, pivotIndex - 1);

                if (pivotIndex + 1 < right)
                    QuickSortRecursive(arr, pivotIndex + 1, right);
            }
        }
    }
}