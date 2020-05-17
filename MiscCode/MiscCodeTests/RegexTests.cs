using System.Text.RegularExpressions;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class RegexTests
    {
        [Test]
        [TestCase("Allowed . @ Allowed ! Allowed $", true)]
        [TestCase("Allowed", true)]
        [TestCase(" Allowed ", true)]
        [TestCase(" Allowed#Allowed ", true)]
        [TestCase("Allowed#Allowed-Allowed", true)]
        [TestCase("Some name / not allowed", false)]
        [TestCase("Some name \\ not allowed", false)]
        [TestCase("Some name \n not allowed", false)]
        [TestCase("Some name \r not allowed", false)]
        [TestCase("Other : not allowed", false)]
        [TestCase("Other ; not allowed", false)]
        [TestCase("Other < not allowed", false)]
        [TestCase("Other > not allowed", false)]
        [TestCase("Other * not allowed", false)]
        public void CanTestForFileName(string name, bool isValid)
        {
            var rx =new Regex("^[^<>:;,?\"*|/\\\\\n\r]+$");

            Assert.That(rx.IsMatch(name), Is.EqualTo(isValid));
        }
        
    }
}