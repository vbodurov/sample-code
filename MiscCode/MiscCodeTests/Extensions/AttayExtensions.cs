using System;
using System.Runtime.InteropServices;

namespace MiscCodeTests.Extensions
{
    public static class AttayExtensions
    {
        public static T[] GetRow<T>(this T[,] array, int rowIndex)
        {
            if (!typeof(T).IsPrimitive)
                throw new InvalidOperationException("Not supported for managed types.");

            if (array == null)
                throw new ArgumentNullException("array");

            int cols = array.GetLength(1);
            T[] result = new T[cols];
            int bytes = Marshal.SizeOf(typeof(T));

            Buffer.BlockCopy(array, rowIndex*cols*bytes, result, 0, cols*bytes);

            return result;
        }
    }
}