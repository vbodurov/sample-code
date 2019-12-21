using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class PairsToHierarchy_Problem
    {
        [Test]
        public void PairsToHierarchy_Recursive()
        {
            var map = new Dictionary<string, List<string>>();
            var children = new HashSet<string>();
            foreach (var e in Relations)
            {
                if (!map.TryGetValue(e.parent, out var list))
                {
                    map[e.parent] = list = new List<string>();
                }
                children.Add(e.child);
                list.Add(e.child);
            }
            var top = map.Keys.FirstOrDefault(p => !children.Contains(p));
            if(top == null) return;
            var sb = new StringBuilder();

            sb.AppendLine(top);

            PopulateResults(map, top, sb, 1);

            Assert.That(sb.ToString().Trim(), Is.EqualTo(Expect.Trim()));
        }
        void PopulateResults(Dictionary<string, List<string>> map, string top, StringBuilder sb, int level)
        {
            if (map.TryGetValue(top, out var list))
            {
                var prefix = string.Join("", Enumerable.Range(0, level).Select(i => "    "));
                foreach (var child in list)
                {
                    sb.AppendLine(prefix + child);
                    PopulateResults(map, child, sb, level + 1);
                }
            }
        }


        static List<(string parent, string child)> Relations = 
            new List<(string parent, string child)>
            {
                ("animal", "mammal"),
                ("animal", "bird"),
                ("lifeform", "animal"),
                ("cat", "lion"),
                ("mammal", "cat"),
                ("animal", "fish"),
            };

        static string Expect = @"
lifeform
    animal
        mammal
            cat
                lion
        bird
        fish
".Trim();
    }
}