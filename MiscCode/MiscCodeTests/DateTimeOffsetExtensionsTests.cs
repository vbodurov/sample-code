using System;
using System.Runtime.InteropServices;
using MiscCodeTests.Extensions;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class DateTimeOffsetExtensionsTests
    {
        [Test]
        public void Can_Convert_DateTimeOffset()
        {
            var arr = new[]
            {
                DateTimeOffset.Now,
                new DateTimeOffset(new DateTime(1555, 5, 5, 2, 23, 55), TimeSpan.FromMinutes(120)),
                new DateTimeOffset(new DateTime(2222, 8, 8, 3, 33, 15), TimeSpan.FromMinutes(120)),
                DateTimeOffset.UtcNow,
                new DateTimeOffset(new DateTime(1999, 12, 31, 23, 59, 59), TimeSpan.FromMinutes(-120)),
            };

            foreach (var ini in arr)
            {
                var bytes = ini.ToBytes();
                var transformed = bytes.ToDateTimeOffset();

                Assert.That(ini, Is.EqualTo(transformed));
            }
        }
    }
}