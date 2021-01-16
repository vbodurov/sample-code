using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class FileOperations
    {
        [Test]
        public async Task CanReadLines()
        {
            using (Stream stream = File.Open(@"../../Files/lines.txt", FileMode.Open, FileAccess.Read, FileShare.Read))
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

        [Test]
        public async Task CanOverrideLines()
        {
            using (Stream stream = File.Open(@"../../Files/lines.txt", FileMode.Truncate, FileAccess.ReadWrite, FileShare.Read))
            {
                using (var sw = new StreamWriter(stream))
                {
                    await sw.WriteLineAsync("AAA");
                    await sw.WriteLineAsync("BBB");
                    await sw.WriteAsync("CCC");
                }
            }
        }

        [Test]
        public async Task CanReadLines2()
        {
            using (Stream stream = File.Open(@"../../Files/lines.txt", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var sr = new StreamReader(stream))
                {
                    var i = 0;
                    while (true)
                    {
                        ++i;
                        var line = await sr.ReadLineAsync();
                        if (i == 1) await Task.Delay(10000);
                        if(line == null) break;
                        Console.WriteLine(line);
                    }
                }
            }
        }

    }
}