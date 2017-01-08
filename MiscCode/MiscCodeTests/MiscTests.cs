using System;
using System.IO;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class MiscTests
    {
        [Test]
        public void Test1()
        {
            var n = 0;
            for (var i = 0; i < 4; ++i)
            {
                Console.WriteLine("PRE:"+i);
                if (n == 0 && i == 2)
                {
                    Console.WriteLine(i+"=>"+(i-2));
                    ++n;
                    i -= 2;
                    continue;
                }
                Console.WriteLine("POS:"+i);
                
            }
        }
        [Test]
        public void Test2()
        {
            var span = TimeSpan.FromMinutes(-420);
            var d = new DateTimeOffset(DateTime.UtcNow.Ticks + span.Ticks, span);

            Console.WriteLine(d);
            Console.WriteLine(DateTimeOffset.Now+"|"+DateTimeOffset.Now.Offset.TotalMinutes);
        }
        [Test]
        public void Test3()
        {
            
        }
        [Test]
        public void Test4()
        {

        }
        [Test]
        public void Test5()
        {

        }
    }
}

