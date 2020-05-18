using System;
using System.Collections.Generic;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class Permutations
    {
        /*
""
A
AB
ABC
AC
B
BC
C
         */
        [Test, Category(category.FundamentalAlgorythms)]
        [TestCase("ABC")]
        public void PrintLetters1_DFS(string letters)
        {
            IList<IList<char>> subsets = new List<IList<char>>();

            Permutate1(0, letters, new List<char>(), subsets);
            foreach (var s in subsets)
            {
                Console.WriteLine(s.JoinStrings(""));
            }

        }
        void Permutate1(int index, string letters, IList<char> current, IList<IList<char>> subsets)
        {
            subsets.Add(new List<char>(current));
            for (int i = index; i < letters.Length; i++)
            {
                current.Add(letters[i]);
                Permutate1(i + 1, letters, current, subsets);
                current.RemoveAt(current.Count - 1);
            }
        }


        /*
ABC
ACB
BAC
BCA
CAB
CBA
         */
        [Test, Category(category.FundamentalAlgorythms)]
        [TestCase("ABC")]
        public void PrintLetters2_DFS(string letters)
        {

            IList<IList<char>> result = new List<IList<char>>();
            var visited = new bool[letters.Length];

            Permutate2(visited, letters, new List<char>(), result);
            foreach (var s in result)
            {
                Console.WriteLine(s.JoinStrings(""));
            }
        }
        void Permutate2(bool[] visited, string letters, IList<char> current, IList<IList<char>> result)
        {
            if (current.Count == letters.Length) result.Add(new List<char>(current));

            for (int i = 0; i < letters.Length; i++)
            {
                if (visited[i]) continue;
                current.Add(letters[i]);
                visited[i] = true;
                Permutate2(visited, letters, current, result);
                visited[i] = false;
                current.RemoveAt(current.Count - 1);
            }
        }


        /*
ABC
ACB
BAC
BCA
CBA
CAB
         */
        [Test, Category(category.FundamentalAlgorythms)]
        [TestCase("ABC")]
        public void PrintLetters3_DFS(string letters)
        {
            var result = new List<string>();
            var chars = letters.ToCharArray();

            Permutate3(chars, 0, letters.Length - 1, result);

            Console.WriteLine(result.JoinStrings("\n"));
        }
        void Permutate3(char[] chars, int l, int r, IList<string> list)
        {
            if (l == r)
            {
                list.Add(new string(chars));
                return;
            }

            for (var i = l; i <= r; ++i)
            {
                Swap(chars, l, i);
                Permutate3(chars, l + 1, r, list);
                Swap(chars, l, i);
            }
        }
        void Swap(char[] chars, int i, int j)
        {
            var temp = chars[i];
            chars[i] = chars[j];
            chars[j] = temp;
        }


        /*

ABC
BC
AC
AB
C
B
A
""
         */
        [Test, Category(category.FundamentalAlgorythms)]
        [TestCase("ABC")]
        public void PrintLetters4_BFS(string letters)
        {
            var result = new List<string>();
            var queue = new Queue<string>();
            var queueElements = new HashSet<string>();
            queue.Enqueue(letters);
            queueElements.Add(letters);

            while (queue.Count > 0)
            {
                var curr = queue.Dequeue();
                result.Add(curr);
                for (var i = 0; i < curr.Length; i++)
                {
                    var sub = curr.Substring(0, i) + curr.Substring(i + 1);
                    if (!queueElements.Contains(sub))
                    {
                        queue.Enqueue(sub);
                        queueElements.Add(sub);
                    }
                }
            }

            Console.WriteLine(result.JoinStrings("\n"));
        }
    }
}