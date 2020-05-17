using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class ReadFileLines
    {
        [Test]
        public async Task CanReadLines()
        {
            using (Stream stream = File.Open(@"../../Files/lines.txt", FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    for (int i = 1; i < 10; i++)
                    {
                        string line = await reader.ReadLineAsync();
                        Console.WriteLine($"line # {i} is-null:{line == null} is-empty:{line == ""} '{line}'");

                    }

                }
            }
        }
        
    }
}