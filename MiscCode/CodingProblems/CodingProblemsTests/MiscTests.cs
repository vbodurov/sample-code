using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class MiscTests
    {
        [Test]
        public void Test1()
        {



            var r = findSignatureCounts(new[] {4, 3, 2, 5, 1});
            Console.WriteLine(r.JoinStrings(","));
        }

        static int[] findSignatureCounts(int[] arr)
        {
            var result = new int[arr.Length];
            //Array.Fill(result, 1);
            var visited = new HashSet<int>();
            for (var i = 0; i < arr.Length; ++i)
            {
                if (visited.Contains(i)) continue;

                var j = i;
                var group = new HashSet<int>();
                group.Add(i);
                while (arr[j] - 1 != i)
                {
                    j = arr[j] - 1;
                    group.Add(j);
                }
                //Console.WriteLine(group.JoinStrings(","));
                foreach (var n in group)
                {
                    visited.Add(n);
                    result[n] = group.Count;
                }
            }
            return result;
        }

    }
}