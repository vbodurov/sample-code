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
            var s = "/Users/vladimirbodurov/Desktop/Code/GitHub/multiplayer-experiment/CircuitRacerBegin/Assets";
            //s:/Users/vladimirbodurov/Desktop/Code/GitHub/multiplayer-experiment/CircuitRacerBegin/Assets

            var arr = s.Split(new[] {'/', '\\'}, StringSplitOptions.None);

            //code:/Users/vladimirbodurov/Desktop/Code/
            var codeRoot = String.Join("/", arr.Take(arr.Length - 4))+"/";

            //gitRoot:/Users/vladimirbodurov/Desktop/Code/GitHub/multiplayer-experiment/
            var gitRoot = String.Join("/", arr.Take(arr.Length - 2))+"/";

            //unityRoot:/Users/vladimirbodurov/Desktop/Code/GitHub/multiplayer-experiment/CircuitRacerBegin/
            var unityRoot = String.Join("/", arr.Take(arr.Length - 1))+"/";

            Console.WriteLine(unityRoot);
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

