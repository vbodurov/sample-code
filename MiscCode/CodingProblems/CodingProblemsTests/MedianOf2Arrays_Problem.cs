using System;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class MedianOf2Arrays_Problem
    {
        /*
            1. use 2 pointers to iterate both arrays are comparing the items 
               and keep tracking the second last and last item
            2. also keep a counter++ when each comparison
            3. stop iteration when counter >= total / 2 + 1
            4. then return value based on total even or odd numbers.
         */
        [Test, Category(category.SpecificProblems)]
        public void MedianOf2Arrays()
        {
            //1,2,12,13,15,17,26,30,38,45 m=16
            int[] ar1 = { 1, 12, 15, 26, 38 };
            int[] ar2 = { 2, 13, 17, 30, 45 };

            int n1 = ar1.Length;
            int n2 = ar2.Length;
            if (n1 == n2)
                Console.Write("Median is " +
                              getMedian(ar1, ar2));
            else
                Console.Write("arrays are of unequal size");
        }

        static int getMedian(int[] nums1, int[] nums2)
        {
            var total = nums1.Length + nums2.Length;
            var isOdd = total % 2 == 1;

            var i1 = 0;
            var i2 = 0;
            var last = 0;
            var secLast = 0;
            var counter = 0;

            while (i1 < nums1.Length || i2 < nums2.Length)
            {
                counter++;

                secLast = last;
                if (i1 < nums1.Length && i2 < nums2.Length)
                {
                    if (nums1[i1] > nums2[i2])
                    {
                        last = nums2[i2];
                        i2++;
                    }
                    else
                    {
                        last = nums1[i1];
                        i1++;
                    }
                }
                else if (i1 < nums1.Length)
                {
                    last = nums1[i1];
                    i1++;
                }
                else
                {
                    last = nums2[i2];
                    i2++;
                }

                //Console.WriteLine($"{total},{counter},{secLast},{last}");

                if (counter >= total / 2 + 1) break;
            }

            if (isOdd) return last;
            return (int)((last + secLast) / 2d);
        }
    }
}