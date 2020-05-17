using System;

namespace MiscCodeTests.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static byte[] ToBytes(this DateTimeOffset dto)
        {
            var arr1 = BitConverter.GetBytes(dto.DateTime.Ticks);
            var arr2 = BitConverter.GetBytes((short)dto.Offset.TotalMinutes);
            var result = new byte[10];
            Array.Copy(arr1, 0, result, 0, arr1.Length);
            Array.Copy(arr2, 0, result, 8, arr2.Length);
            return result;
        }
    }
}