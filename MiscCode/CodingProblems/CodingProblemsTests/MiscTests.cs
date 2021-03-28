using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class MiscTests
    {
        [Test]
        public void Test1()
        {
            var arr = new int[] {1, 2, 3, 4};


            Console.WriteLine(arr.Aggregate(1, (p,e) => p * e));
        }

    }
}