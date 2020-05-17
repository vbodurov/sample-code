using System;
using System.Collections.Generic;
using System.Linq;
using CodingProblemsTests.Extensions;
using CodingProblemsTests.Utils;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class SortTwoNumbers_Problem
    {
        [Test]
        [TestCase("0, 0, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0")]
        [TestCase("0")]
        [TestCase("1")]
        [TestCase("0, 0")]
        [TestCase("1, 1")]
        [TestCase("0, 1")]
        [TestCase("1, 0")]
        [TestCase("1, 0, 1, 0")]
        [TestCase("0, 1, 0, 1")]
        [TestCase("0, 1, 0, 0, 0, 1")]
        [TestCase("1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1")]
        [TestCase("0, 0, 0, 0, 1, 1, 1, 0, 1")]
        [TestCase("1, 0, 1, 0, 1, 0, 1, 0, 1")]
        [TestCase("0, 1, 0, 1, 0, 1, 0, 1, 0")]
        [TestCase("0, 0, 0, 0, 1")]
        [TestCase("0, 0, 0, 0, 1, 0, 0, 0, 0")]
        [TestCase("0, 0, 0, 0")]
        [TestCase("1, 1, 1, 1")]
        [TestCase("1, 1, 1, 1, 0, 0, 0, 0")]
        [TestCase("0, 0, 0, 0, 1, 1, 1, 1")]
        [TestCase("1, 0, 0, 0, 0, 1")]
        [TestCase("0, 1, 1, 1, 1, 0")]
        [TestCase("1, 1, 0, 1, 1")]
        [TestCase("1, 1, 1")]
        [TestCase("1, 0, 1")]
        [TestCase("0, 1, 0")]
        public void SortTwoNumbers(string str)
        {
            var arr = ListParser.ToListOfInts(str);
            var copy = new List<int>(arr);

            Sort(arr);

            Assert.That(arr.JoinStrings(","), Is.EqualTo(copy.OrderBy(e => e).JoinStrings(",")));
            Console.WriteLine(arr.JoinStrings(","));
        }
        void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        void Sort(int[] arr)
        {
            var l = 0;
            for (int r = 1; r < arr.Length; r++)
            {
                if (arr[l] > arr[r])
                {
                    Swap(arr, r, l);
                }
                while (l < r && arr[l] < arr[r])
                {
                    l++;
                }
            }
        }
    }
}