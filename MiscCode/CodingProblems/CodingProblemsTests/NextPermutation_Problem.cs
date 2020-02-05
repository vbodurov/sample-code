using System;
using CodingProblemsTests.Extensions;
using CodingProblemsTests.Utils;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class NextPermutation_Problem
    {
        [Test]
        [TestCase("1,2,3", "1,3,2")]
        [TestCase("3,2,1", "1,2,3")]
        [TestCase("1,1,5", "1,5,1")]
        public void NextPermutation(string strNums, string expected)
        {
            var nums = ListParser.ToListOfInts(strNums);

            nums = GetNextPermutation(nums);

            Assert.That(nums.JoinStrings(","), Is.EqualTo(expected));
        }
        [Test]
        /*
            1,3,2
            2,1,3
            2,3,1
            3,1,2
            3,2,1
            1,2,3
         */
        public void Permutate()
        {
            var nums = ListParser.ToListOfInts("1,2,3");

            for (int i = 0; i < 6; i++)
            {
                nums = GetNextPermutation(nums);
                Console.WriteLine(nums.JoinStrings(","));
            }
        }
        int[] GetNextPermutation(int[] nums)
        {
            var descending = -1;
            // 1. from right side find first descending
            for (var i = nums.Length - 2; i >= 0; i--)
            {
                var a = nums[i];
                var b = nums[i + 1];
                
                if (a < b)
                {
                    descending = i;
                    break;
                }
            }
            // 2. find first heigher than the left lower bound
            for (var i = nums.Length - 1; i > descending && descending >= 0; i--)
            {
                if (nums[i] > nums[descending])
                {
                    // 3. swap left lower bound and first heigher         
                    Swap(nums, i, descending);
                    break;
                }
            }



            // 4. reverse from left lower bound on
            var l = descending + 1;
            var r = nums.Length - 1;
            while (l < r)
            {
                Swap(nums, l, r);
                l++;
                r--;
            }

            return nums;
        }
        void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}