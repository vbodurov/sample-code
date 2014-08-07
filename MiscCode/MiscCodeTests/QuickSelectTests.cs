using System;
using System.Diagnostics;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class QuickSelectTests
    {
        [Test]
        public void CanSelectAmongMany()
        {
            var fromBytes = GC.GetTotalMemory(false);
            var rand = new Random(DateTime.Now.Millisecond);


            var arr = new float[23000000];
            for (var i = 0; i < arr.Length; ++i)
            {
                arr[i] = rand.Next(5, 10000000);
            }

            var sw = Stopwatch.StartNew();
            var med = QuickSelect(arr, arr.Length / 2);
            sw.Stop();
            var toBytes = GC.GetTotalMemory(false);
            Console.WriteLine("median:" + med);
            Console.WriteLine(sw.Elapsed.Milliseconds + "ms|memory used:" + ((toBytes - fromBytes) / (1024 * 1024)) + "MB");


        }


        public static float QuickSelect(float[] list, int findIndex)
        {
            return QuickSelect(list, findIndex, 0, list.Length);
        }
        public static float QuickSelect(float[] list, int findIndex, int startIndex, int endIndex)
        {
            while (true)
            {
                // Assume startIndex <= findIndex < endIndex
                int pivotIndex = (startIndex + endIndex) / 2; //arbitrary, but good if sorted
                int splitIndex = Partition(list, startIndex, endIndex, pivotIndex);
                if (findIndex < splitIndex)
                    endIndex = splitIndex;
                else if (findIndex > splitIndex)
                    startIndex = splitIndex + 1;
                else //if (findIndex == splitI)
                    return list[findIndex];
            }
            //when this returns, all elements of list[i] <= list[findIndex] iif i <= findIndex
        }
        private static int Partition(float[] list, int startIndex, int endIndex, int pivotIndex)
        {
            float pivotValue = list[pivotIndex];
            list[pivotIndex] = list[startIndex];
            list[startIndex] = pivotValue;

            int storeIndex = startIndex + 1;//no need to store @ pivot item, it's good already.
            //Invariant: startIndex < storeI <= endIndex
            while (storeIndex < endIndex && list[storeIndex] <= pivotValue)
                ++storeIndex;
            for (var i = storeIndex + 1; i < endIndex; ++i)
                if (list[i] <= pivotValue)
                {
                    Swap(list, i, storeIndex);
                    ++storeIndex;
                }
            int newPivotIndex = storeIndex - 1;
            list[startIndex] = list[newPivotIndex];
            list[newPivotIndex] = pivotValue;
            //now [startIndex, newPivotIndex] are <= to pivotValue && list[newPivotIndex] == pivotValue.
            return newPivotIndex;
        }
        private static void Swap(float[] list, int index1, int index2)
        {
            float tmp = list[index1];
            list[index1] = list[index2];
            list[index2] = tmp;
        }
    }
}