using System;
using System.Linq;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class ReverseWords_Problem
    {
        [Test]
        public void CanReverseWords()
        {
            const string origin = "abcd efg hijkl";
            const string expect = "dcba gfe lkjih";

            var result = Reverse(origin);

            Assert.That(result, Is.EqualTo(expect));
        }

        string Reverse(string origin)
        {
            var reversed = new string(origin.Reverse().ToArray());
            return string.Join(" ", reversed.Split(' ').Reverse());
        }
    }
}