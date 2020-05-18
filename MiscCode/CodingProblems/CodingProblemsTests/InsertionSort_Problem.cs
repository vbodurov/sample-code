using System;
using System.Linq;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture, Category(category.FundamentalAlgorythms)]
    public class InsertionSort_Problem
    {
        [Test]
        [TestCase("3, 8, 7, 5, 2, 1, 9, 6, 4")]
        [TestCase("1, 2, 3, 4, 5")]
        [TestCase("5, 4, 3, 2, 1")]
        [TestCase("5")]
        [TestCase("1, 2")]
        [TestCase("2, 1")]
        [TestCase("8, 2, 3, 4, 5, 3, 1, 15, 9")]
        public void InsertionSortTest(string str)
        {
            // Arrange
            var curr = str.Split(',').Select(s => int.Parse(s.Trim())).ToArray();
            var targ = new int[curr.Length];
            Array.Copy(curr, targ, curr.Length);
            Array.Sort(targ);

            // Act
            InsertionSort(curr);

            // Assert
            for (int i = 0; i < curr.Length; i++)
            {
                Assert.That(curr[i], Is.EqualTo(targ[i]));
            }
        }

        private void InsertionSort(int[] numbers)
        {
            for (var i = 1; i < numbers.Length; i++)
            {
                var j = i;
                while (j > 0)
                {
                    if (numbers[j - 1] > numbers[j])
                    {
                        Swap(numbers, j - 1, j);
                        j--;
                    }
                    else break;
                }
            }
        }
        static void Swap(int[] arr, int a, int b)
        {
            var temp = arr[b];
            arr[b] = arr[a];
            arr[a] = temp;
        }
    }
}