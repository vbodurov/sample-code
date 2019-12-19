using System.Collections.Generic;
using NUnit.Framework;

namespace MiscCodeTests.Problems
{
    [TestFixture]
    public class TreasureIsland_Problem
    {
        [Test]
        public void TreasureIsland()
        {
            char[][] grid = {
                new[]{'O', 'O', 'O', 'O'},
                new[]{'D', 'O', 'D', 'O'},
                new[]{'O', 'O', 'O', 'O'},
                new[]{'X', 'D', 'D', 'O'}
            };
            var steps = MinSteps(grid);

            Assert.That(steps, Is.EqualTo(steps));
        }
        static readonly int[][] Directions = {new[] {1, 0}, new[] {0, 1}, new[] {-1, 0}, new[] {0, -1}};
        int MinSteps(char[][] grid)
        {
            var q = new Queue<Point>();
            q.Enqueue(new Point(0, 0));
            grid[0][0] = 'D'; // mark as visited
            for (int steps = 1; q.Count > 0; steps++)
            {
                for (int sz = q.Count; sz > 0; sz--)
                {
                    Point p = q.Dequeue();

                    foreach (int[] dir in Directions)
                    {
                        int r = p.r + dir[0];
                        int c = p.c + dir[1];

                        if (IsSafe(grid, r, c))
                        {
                            if (grid[r][c] == 'X') return steps;
                            grid[r][c] = 'D'; // mark as visited
                            q.Enqueue(new Point(r, c));
                        }
                    }
                }
            }
            return -1;
        }
        bool IsSafe(char[][] grid, int r, int c) =>
            r >= 0 && r < grid.Length &&
            c >= 0 && c < grid[0].Length &&
            grid[r][c] != 'D'
        ;
        public struct Point
        {
            public int r;
            public int c;
            public Point(int r, int c)
            {
                this.r = r;
                this.c = c;
            }
        }
    }
}