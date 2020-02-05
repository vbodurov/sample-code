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
            var r = (dbl: 0.5, igr: 123, flag: false);

            Update(in r);


        }
        void Update(in (double dbl, int igr, bool flag) t)
        {
            
        }
    }
}