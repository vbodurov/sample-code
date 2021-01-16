using System;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class CharacterTests
    {
        [Test]
        public void ViewChars()
        {
            var phonemes = "abdefghijklmnopstuvwzæðŋɑɔəɛɜɪɻʃʊʌʒɪndi͡ə";

            foreach (var ch in phonemes)
            {
                Console.WriteLine(ch+"="+Char.GetUnicodeCategory(ch));
            }
        }
        
    }
}