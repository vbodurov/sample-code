using System;
using System.Linq;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class HeapSort_Problem
    {
        [Test, Category(category.FundamentalAlgorythms)]
        [TestCase("2,5,6,4,1,5,6,9,5,8,1")]
        [TestCase("2")]
        [TestCase("1,2,3")]
        [TestCase("3,2,1")]
        [TestCase("3,2,1,2,3")]
        public void TestHeapSort(string str)
        {
            // Arrange
            var arr1 = str.Split(',').Select(s => int.Parse(s.Trim())).ToArray();
            var arr2 = new int[arr1.Length];
            Array.Copy(arr1, arr2, arr1.Length);
            Array.Sort(arr2);

            // Act
            HeapSort(arr1, arr1.Length);

            // Assert
            Console.WriteLine(string.Join(", ", arr1));
            for (int i = 0; i < arr1.Length; i++)
            {
                Assert.That(arr1[i], Is.EqualTo(arr2[i]));
            }
            

        }
        static int GetLeftChildIndex(int i) => 2 * i + 1;
        static int GetRightChildIndex(int i) => 2 * i + 2;
        static int GetParentIndex(int i) => (i - 1) / 2;
        static void HeapSort(int[] arr, int n)
        {
            // build max heap
            for (var i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(arr, n, i);
            }

            for (var i = n - 1; i >= 0; i--)
            {
                Swap(arr, 0, i);
                Heapify(arr, i, 0);
            }
        }
        static void Heapify(int[] arr, int n, int i)
        {
            var largest = i;
            var left = GetLeftChildIndex(i);
            var right = GetRightChildIndex(i);
            if (left < n && arr[left] > arr[largest]) largest = left;
            if (right < n && arr[right] > arr[largest]) largest = right;
            if (largest != i)
            {
                Swap(arr, i, largest);
                Heapify(arr, n, largest);
            }
        }
        static void Swap(int[] arr, int a, int b)
        {
            var swap = arr[a];
            arr[a] = arr[b];
            arr[b] = swap;
        }

    }
}