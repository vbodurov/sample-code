using System;
using System.Collections.Generic;
using MiscCodeTests.Extensions;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class MiscTests
    {
        [Test]
        public void Test1()
        {
            var sd = new SortedDictionary<int, int>(Comparer<int>.Create((a,b)=>-a.CompareTo(b)));
            sd.Add(1, 1);
            sd.Add(100, 100);
            sd.Add(2, 2);
            sd.Add(15, 15);
            sd.Add(10, 10);
            foreach (var e in sd)
            {
                Console.WriteLine(e.Key+"="+e.Value);
            }


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

