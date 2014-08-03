using System;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class MergeSortRecursiveTests
    {
        [Test]
        public void MergeSortTest()
        {
            int[] numbers = { 3, 8, 7, 5, 2, 1, 9, 6, 4 };

            MergeSortRecursive(numbers, 0, numbers.Length);

            foreach (var n in numbers) 
                Console.WriteLine(n);

        }
        public static void MergeSortRecursive<T>(T[] a, int left, int right) where T : IComparable<T>
        {
            int range = right - left;
            if (range <= 1)
                return;

            int mid = left + range / 2;

            MergeSortRecursive(a, left, mid);

            MergeSortRecursive(a, mid, right);

            // merge function
            var temp = new T[range];
            int lt = left, rt = mid;
            for (int k = 0; k < range; k++)
            {
                if (lt == mid) temp[k] = a[rt++];
                else if (rt == right) temp[k] = a[lt++];
                else if (a[rt].CompareTo(a[lt]) < 0) temp[k] = a[rt++];
                else temp[k] = a[lt++];
            }

            // copy temp into actual
            for (int k = 0; k < range; k++)
            {
                a[left + k] = temp[k];
            }
        }



    }
}
