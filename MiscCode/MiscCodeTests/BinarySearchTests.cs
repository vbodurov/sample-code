using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class BinarySearchTests
    {
        [Test]
        public void CanFindElement()
        {
            const int notThere = 9;
            var arr = new[] {1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15};

            Assert.That(TryFindWithBinarySearch(arr, notThere), Is.False);
            
            foreach (var curr in arr)
                Assert.That(TryFindWithBinarySearch(arr, curr), Is.True);
            
        }

        static bool TryFindWithBinarySearch(int[] arr, int key, int minIndex = -1, int maxIndex = -1)
        {
            if (minIndex < 0) minIndex = 0;
            if (maxIndex < 0) maxIndex = arr.Length - 1;

            // test if array is empty
            if (maxIndex < minIndex)
            {
                // set is empty, so return value showing not found
                return false;
            }
            
            // calculate midpoint to cut set in half
            var midIndex = (int)(minIndex + (maxIndex - minIndex) / 2f);

            // three-way comparison
            // key is in lower subset
            if (arr[midIndex] == key)
            {
                return true;
            }
            if (arr[midIndex] > key && TryFindWithBinarySearch(arr, key, minIndex, midIndex - 1))
            {
                return true;
            }
            // key is in upper subset
            if (arr[midIndex] < key && TryFindWithBinarySearch(arr, key, midIndex + 1, maxIndex))
            {
                return true;
            }
            return false;
        }
    }
}