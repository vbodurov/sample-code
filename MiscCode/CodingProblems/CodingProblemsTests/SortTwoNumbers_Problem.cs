using System;
using System.Collections.Generic;
using System.Linq;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class SortTwoNumbers_Problem
    {
        [Test]
        [TestCase(1, new int[] { 0, 0, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0 })]
        [TestCase(2, new int[] { 0, 1 })]
        [TestCase(3, new int[] { 1, 0 })]
        [TestCase(4, new int[] { 1, 0, 1, 0 })]
        [TestCase(5, new int[] { 0, 1, 0, 1 })]
        [TestCase(6, new int[] { 0, 1, 0, 0, 0, 1 })]
        [TestCase(7, new int[] { 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1 })]
        [TestCase(8, new int[] { 0, 0, 0, 0, 1, 1, 1, 0, 1 })]
        [TestCase(9, new int[] { 0, 0, 0, 0, 1 })]
        [TestCase(10, new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0 })]
        [TestCase(11, new int[] { 0, 0, 0, 0 })]
        [TestCase(11, new int[] { 1, 1, 1 })]
        [TestCase(11, new int[] { 1, 0, 1 })]
        [TestCase(11, new int[] { 0, 1, 0 })]
        public void SortTwoNumbers(int id, int[] arr)
        {
            var copy = new List<int>(arr);


            var j = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[j] > arr[i])
                {
                    Swap(arr, i, j);
                }
                while (j < i && arr[j] < arr[i])
                {
                    j++;
                }
            }
            Assert.That(arr.JoinStrings(","), Is.EqualTo(copy.OrderBy(e => e).JoinStrings(",")));
            Console.WriteLine(arr.JoinStrings(","));
        }
        void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}