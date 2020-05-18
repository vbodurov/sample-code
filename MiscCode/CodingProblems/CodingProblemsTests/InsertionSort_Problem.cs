using System;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture, Category(category.FundamentalAlgorythms)]
    public class InsertionSort_Problem
    {
        [Test]
        public void InsertionSortTest()
        {
            int[] numbers = { 3, 8, 7, 5, 2, 1, 9, 6, 4 };

            InsertionSort(numbers);

            for (int i = 0; i < 9; i++)
                Console.WriteLine(numbers[i]);
        }

        private void InsertionSort(int[] numbers)
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                int j = i;
                while (j > 0)
                {
                    if (numbers[j - 1] > numbers[j])
                    {
                        Swap(numbers, j - 1, j);
                        j--;
                    }
                    else
                        break;
                }
            }
        }
        static void Swap(int[] arr, int a, int b)
        {
            int temp = arr[b];
            arr[b] = arr[a];
            arr[a] = temp;
        }
    }
}