using System;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class InheritanceTests
    {
        [Test]
        public void Test()
        {
            IClassA x = new ClassB();

            x.DoWork("X");
        }
    }

    public interface IClassA
    {
        void DoWork(string s);
    }

    public class ClassA : IClassA
    {
        public void DoWork(string s)
        {
            Console.WriteLine("AAA: " + s);
        }
    }

    public class ClassB : ClassA, IClassA
    {
        public void DoWork(string s)
        {
            Console.WriteLine("BBB: " + s);
        }
    }
}