using System;

namespace MiscCodeTests.Extensions
{
    public static class ByteExtensions
    {
        public static DateTimeOffset ToDateTimeOffset(this byte[] bytes)
        {
            var date = BitConverter.ToInt64(bytes, 0);
            var offset = BitConverter.ToInt16(bytes, 8);
            return new DateTimeOffset(new DateTime(date), TimeSpan.FromMinutes(offset));
        }
    }
}