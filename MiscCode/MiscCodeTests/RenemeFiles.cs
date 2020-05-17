using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class RenemeFiles
    {
        [Test]
        public void Rename()
        {
            const string path = @"C:\GitHub\podcast-visualizer\PodcastVisualizer\Assets\Models\James\James.images";
            foreach (var source in Directory.GetFiles(path))
            {
                var name = Path.GetFileName(source);
                Console.WriteLine(name);
                if (name.StartsWith("Rock"))
                {
                    File.Move(source, source.Replace(name, name.Replace("Rock", "James")));
                }
            }
        }
    }
}