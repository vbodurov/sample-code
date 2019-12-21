using System;
using System.Collections.Generic;
using CodingProblemsTests.Utils;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class CriticalConnections_Problem
    {
        [Test]
        [TestCase(4, "[[0, 1], [1, 2], [2, 0], [1, 3]]", "[[1,3]]")]
//        [TestCase(5, "[[1, 2], [1, 3], [3, 4], [1, 4], [4, 5]]", "[[1, 2], [4, 5]]")]
        [TestCase(6, "[[1, 2], [1, 3], [2, 3], [2, 4], [2, 5], [4, 6], [5, 6]]", "[]")]
//        [TestCase(9, "[[1, 2], [1, 3], [2, 3], [3, 4], [3, 6], [4, 5], [6, 7], [6, 9], [7, 8], [8, 9]]","[[3, 4], [3, 6], [4, 5]]")]
        public void GetCriticalConnections(int n, string strInput, string strExpect)
        {
            var input = ListParser.ToListOfLists(strInput);
            var expect = ListParser.ToListOfLists(strExpect);

            var result = CriticalConnections(n, input);

            Console.WriteLine(ListParser.AsSortedString(result) + "=" + ListParser.AsSortedString(expect));

            Assert.That(ListParser.AsSortedString(result),
                Is.EqualTo(ListParser.AsSortedString(expect)));
        }
        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            LinkedList<int>[] neighbors = new LinkedList<int>[n + 1]; // neighbors[i] represents i's negihbors
            for (int i = 0; i < neighbors.Length; i++)
            {
                neighbors[i] = new LinkedList<int>();
            }

            int[] lowestRank = new int[n + 1]; // lowestRank[i] represents the lowest rank vertex that i can reach
            bool[] visited = new bool[n + 1];
            int[] parents = new int[n + 1]; // the reason we need parents array because during DFS, we want to igore the parent(incoming) vertex while processing a vertex

            // initialization
            for (int i = 0; i < n; i++)
            {
                neighbors[i] = new LinkedList<int>();
                lowestRank[i] = int.MaxValue;
            }
            foreach (var c in connections)
            {
                neighbors[c[0]].AddLast(c[1]);
                neighbors[c[1]].AddLast(c[0]);
            }
            parents[0] = 0;

            IList<IList<int>> res = new List<IList<int>>();
            DFS(neighbors, lowestRank, visited, parents, 0, 0, res); // since the graph is connected, it's ok to start from any vertex (here 0 is chosen)
            return res;
        }
        void DFS(LinkedList<int>[] neighbors, int[] lowestRank, bool[] visited, int[] parents, int current, int rank, IList<IList<int>> res)
        {
            if (!visited[current])
            { // the vertex hasn't been visited yet, we'll calculate the lowest rank vertex it can reach based on downstream vertices info
                visited[current] = true;
                lowestRank[current] = rank; // the initial value of lowestRank 
                foreach (int n in neighbors[current])
                {
                    if (n == parents[current])
                    {
                        continue;
                    }
                    parents[n] = current;
                    DFS(neighbors, lowestRank, visited, parents, n, rank + 1, res);
                    lowestRank[current] = Math.Min(lowestRank[current], lowestRank[n]); // update the lowestRank based on what current's downstream vertice can reach
                    if (lowestRank[n] >= rank + 1)
                    { // this basically checks if the downstream vertex n can reach any ancestor vertex without current->n path
                        IList<int> edge = new List<int>();
                        edge.Add(current);
                        edge.Add(n);
                        res.Add(edge);
                    }
                }
            }
            else
            { // update the parent's lowestRank
                lowestRank[parents[current]] = Math.Min(lowestRank[parents[current]], lowestRank[current]);
            }
        }
    }
}