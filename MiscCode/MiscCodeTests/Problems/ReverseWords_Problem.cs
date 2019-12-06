using System;
using System.Linq;
using NUnit.Framework;

namespace MiscCodeTests.Problems
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

        private string Reverse(string origin)
        {
            var reversed = new String(origin.Reverse().ToArray());
            return String.Join(" ", reversed.Split(' ').Reverse());
        }
    }
}