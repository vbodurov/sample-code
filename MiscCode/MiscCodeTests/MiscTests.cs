using System;
using System.Collections.Generic;
using MiscCodeTests.Extensions;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class MiscTests
    {
        Action _f;
        [Test]
        public void Test1()
        {
            var f1 = new Action(() => Console.WriteLine("1"));
            var f2 = new Action(() => Console.WriteLine("2"));
            var f3 = new Action(() => Console.WriteLine("3"));

            _f += f1;
            _f += f2;
            _f += f3;

            _f -= f1;

            _f?.Invoke();
        }
        [Test]
        public void Test2()
        {
            Console.WriteLine("Test 2");
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
            var er = GenerateWithYield().GetEnumerator();

            if(er.MoveNext())
                Console.WriteLine(er.Current);
            if (er.MoveNext())
                Console.WriteLine(er.Current);
        }

        IEnumerable<int> GenerateWithYield()
        {
            Console.WriteLine("A");
            var i = 0;
            Console.WriteLine("B");
            while (i < 5)
            {
                Console.WriteLine("C");
                yield return ++i;
            }
                
        } 
        
    }
}

