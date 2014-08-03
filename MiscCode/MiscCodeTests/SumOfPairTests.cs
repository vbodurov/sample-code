using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class SumOfPairTests
    {
        [Test]
        public void SumOfPairTest()
        {
            int[] input = { 2, 45, 7, 3, 5, 1, 8, 9 };

            // what pairs if added together give 10
            var pairs = GetSumPairs(input, 10);

            foreach (var t in pairs)
            {
                Console.WriteLine(t.Item1+","+t.Item2);
            }
        }

        private IEnumerable<Tuple<int, int>> GetSumPairs(IEnumerable<int> input, int sum)
        {
            var pairs = new Dictionary<int, int>();
            var list = new List<Tuple<int, int>>();

            foreach (int curr in input)
            {
                if (pairs.ContainsKey(curr))
                    list.Add(Tuple.Create(curr, pairs[curr]));
                else
                    pairs.Add(sum - curr, curr);
            }
            return list;
        }
    }
}