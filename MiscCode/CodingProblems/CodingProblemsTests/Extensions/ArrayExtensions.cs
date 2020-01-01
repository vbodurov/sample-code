using System;

namespace CodingProblemsTests.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Shuffle<T>(this T[] array)
        {
            if (array == null || array.Length <= 1) return array;
            var r = new Random();
            T[] shuffled = new T[array.Length];
            Array.Copy(array, shuffled, array.Length);
            for (int i = 0; i < shuffled.Length; i++)
            {
                var j = r.Next(0, array.Length);
                var temp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = temp;
            }
            return shuffled;
        }
    }
}