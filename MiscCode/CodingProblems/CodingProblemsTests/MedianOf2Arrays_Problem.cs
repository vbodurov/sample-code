using System;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class MedianOf2Arrays_Problem
    {
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

        static int getMedian(int[] ar1, int[] ar2)
        {
            var n = ar1.Length;
            int i = 0;
            int j = 0;
            int count;
            int m1 = -1, m2 = -1;

            // Since there are 2n elements,  
            // median will be average of  
            // elements at index n-1 and n in  
            // the array obtained after  
            // merging ar1 and ar2 
            for (count = 0; count <= n; count++)
            {
                // Below is to handle case  
                // where all elements of ar1[]   
                // are smaller than smallest 
                // (or first) element of ar2[]  
                if (i == n)
                {
                    m1 = m2;
                    m2 = ar2[0];
                    break;
                }

                /* Below is to handle case where all  
                elements of ar2[] are smaller than  
                smallest(or first) element of ar1[] */
                if (j == n)
                {
                    m1 = m2;
                    m2 = ar1[0];
                    break;
                }

                if (ar1[i] < ar2[j])
                {
                    // Store the prev median  
                    m1 = m2;
                    m2 = ar1[i];
                    i++;
                }
                else
                {
                    // Store the prev median  
                    m1 = m2;
                    m2 = ar2[j];
                    j++;
                }
            }

            return (m1 + m2) / 2;
        }
    }
}