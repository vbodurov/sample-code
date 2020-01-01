using System;
using System.Diagnostics;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class QuickSelect_Problem
    {
        [Test]
        public void CanSelectAmongMany()
        {
            var fromBytes = GC.GetTotalMemory(false);
            var rand = new Random(DateTime.Now.Millisecond);

            var arr = new int[]{1,4,5,345,45,2,22,10,5,6,7};
//            var arr = new int[23000000];
//            for (var i = 0; i < arr.Length; ++i)
//            {
//                arr[i] = rand.Next(5, 10000000);
//            }

            var k = arr.Length / 2;
            var sw = Stopwatch.StartNew();
            var result = new QSResult();
            QuickSelect(arr, 0, arr.Length - 1, k, result);
            sw.Stop();
            var toBytes = GC.GetTotalMemory(false);
            Console.WriteLine("median:" + result.Result);
            Console.WriteLine(sw.Elapsed.Milliseconds + "ms|memory used:" + ((toBytes - fromBytes) / (1024 * 1024)) + "MB");


        }


        void QuickSelect(int[] nums, int left, int right, int k, QSResult result)
        {
            if (left <= right)
            {
                var pivotIndex = Partition(nums, left, right);
                if ((pivotIndex + 1) == k)
                {
                    result.Result = nums[pivotIndex];
                    result.IsFound = true;
                }
                if (!result.IsFound) QuickSelect(nums, left, pivotIndex - 1, k, result);
                if (!result.IsFound) QuickSelect(nums, pivotIndex + 1, right, k, result);
            }
        }
        int Partition(int[] nums, int left, int right)
        {
            var pivotValue = nums[right];
            var i = left; // we aim to keep i always before the pivot
            var j = left; // iterates through all
            for (; j < right; j++)
            {
                if (nums[j] < pivotValue)
                {
                    Swap(nums, i, j);
                    i++;
                }
            }
            Swap(nums, i, right); // bring the pivot in the middle
            return i;
        }
        void Swap(int[] nums, int i, int j)
        {
            var temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
        class QSResult
        {
            internal int Result;
            internal bool IsFound;
        }
    }
}