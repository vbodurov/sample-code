using System;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class BubbleSort_Problem
    {
        [Test, Category(category.InefficientAlgorythms)]
        public void BubbleSortTest()
        {
            int[] numbers = { 3, 8, 7, 5, 2, 1, 9, 6, 4 };

            BubbleSort(numbers);

            for (int i = 0; i < 9; i++)
                Console.WriteLine(numbers[i]);
        }

        private void BubbleSort(int[] numbers)
        {
            for (int i = 1; i < numbers.Length; ++i)
            {
                for (int j = 0; j < numbers.Length - i; ++j)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        Swap(numbers, j, j + 1);
                    }
                }
            }
        }
        private static void Swap(int[] arr, int a, int b)
        {
            int temp = arr[b];
            arr[b] = arr[a];
            arr[a] = temp;
        }
    }
}