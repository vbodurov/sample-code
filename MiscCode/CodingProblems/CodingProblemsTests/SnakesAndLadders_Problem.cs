using System;
using System.Collections.Generic;
using System.Linq;
using CodingProblemsTests.Utils;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class SnakesAndLadders_Problem
    {
        [Test]
        [TestCase(@"[
        [-1, -1, -1, -1, -1, -1],
        [-1, -1, -1, -1, -1, -1],
        [-1, -1, -1, -1, -1, -1],
        [-1, 35, -1, -1, 13, -1],
        [-1, -1, -1, -1, -1, -1],
        [-1, 15, -1, -1, -1, -1]]", 4)]
        [TestCase(@"[
        [-1,-1,19,10,-1],
        [ 2,-1,-1, 6,-1],
        [-1,17,-1,19,-1],
        [25,-1,20,-1,-1],
        [-1,-1,-1,-1,15]]", 2)]
        public void SnakesAndLadders(string boardStr, int expect)
        {
            var board = ListParser.ToListOfLists(boardStr).Select(e=>e.ToArray()).ToArray();

            var result = new Solution().SnakesAndLadders(board);

            Assert.That(result, Is.EqualTo(expect));
        }
        [Test]
        [TestCase( 1, 6, 5, 0)]
        [TestCase( 2, 6, 5, 1)]
        [TestCase(31, 6, 0, 5)]
        [TestCase(21, 6, 2, 3)]
        [TestCase(11, 6, 4, 1)]
        [TestCase(17, 6, 3, 4)]
        [TestCase(36, 6, 0, 0)]
        [TestCase( 1, 5, 4, 0)]
        [TestCase(25, 5, 0, 4)]
        [TestCase( 8, 5, 3, 2)]
        public void CanGetRowAndCol(int n, int side, int expectR, int expectC)
        {
            var r = new Solution().ToRowAndCol(side, n);

            Console.WriteLine(r);

            Assert.That(r.row, Is.EqualTo(expectR));
            Assert.That(r.col, Is.EqualTo(expectC));
        }
    }
    public class Solution
    {
        public int SnakesAndLadders(int[][] board)
        {
            var side = board.Length;
            var dp = new Dictionary<int, int>();
            var queue = new Queue<int>();
            queue.Enqueue(1);
            dp.Add(1, 0);
            while (queue.Count > 0)
            {
                var n = queue.Dequeue();
                if (n == side * side) return dp[n];

                for (var i = n + 1; i <= Math.Min(n + 6, side * side); i++)
                {
                    var r = ToRowAndCol(board.Length, i);
                    var curr = board[r.row][r.col];
                    var x = curr == -1 ? i : curr;
                    if (!dp.ContainsKey(x))
                    {
                        dp[x] = dp[n] + 1;
                        queue.Enqueue(x);
                    }
                }
            }

            return -1;
        }
        public (int row, int col) ToRowAndCol(int side, int n)
        {
            var i = n - 1;
            var r = side - 1 - (i / side);
            var c = (side - 1 - r) % 2 == 0 ? i % side : side - 1 - i % side;
            return (row: r, col: c);
        }
    }
}