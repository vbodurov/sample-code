using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;
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

        }
        object Abc()
        {
            var obj = Activator.CreateInstance(typeof(ValueTuple<int, string>));

            obj.GetType().GetField("Item1").SetValue(obj, 564654);
            return obj;
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

