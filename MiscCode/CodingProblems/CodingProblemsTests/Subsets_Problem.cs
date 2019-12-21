using System;
using System.Collections.Generic;
using CodingProblemsTests.Extensions;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class Subsets_Problem
    {
        //https://leetcode.com/problems/subsets/
        [Test]
        public void Subsets()
        {
            new Solution().Subsets(new[] {1, 2, 3});
        }
        public class Solution
        {
            public IList<IList<int>> Subsets(int[] nums)
            {
                IList<IList<int>> subsets = new List<IList<int>>();
                GenerateSubsets(0, nums, new List<int>(), subsets);
                return subsets;
            }
            void GenerateSubsets(int index, int[] nums, List<int> current, IList<IList<int>> subsets)
            {
//Console.WriteLine(">"+index+"|"+(index < nums.Length));
                subsets.Add(new List<int>(current));
//Console.WriteLine($"+ (+): " + current.JoinStrings(","));
                for (int i = index; i < nums.Length; i++)
                {
                    current.Add(nums[i]);
//Console.WriteLine($"A ({i}): " + current.JoinStrings(","));
                    GenerateSubsets(i + 1, nums, current, subsets);
                    current.RemoveAt(current.Count - 1);
//Console.WriteLine($"B ({i}): " + current.JoinStrings(","));
                }
            }
        }

        [Test]
        [TestCase("ABCD")]
        public void PrintLetters(string letters)
        {
            IList<IList<char>> subsets = new List<IList<char>>();

            PrintLettersInternal(0, letters, new List<char>(), subsets);
            foreach (var s in subsets)
            {
                Console.WriteLine("|"+s.JoinStrings(","));
            }

        }
        void PrintLettersInternal(int index, string letters, IList<char> current, IList<IList<char>> subsets)
        {
            subsets.Add(new List<char>(current));
            for (int i = index; i < letters.Length; i++)
            {
                current.Add(letters[i]);
                PrintLettersInternal(i + 1, letters, current, subsets);
                current.RemoveAt(current.Count - 1);
            }
        }



        [Test]
        [TestCase("ABC")]
        public void PrintLetters2(string letters)
        {

            IList<IList<char>> subsets = new List<IList<char>>();
            var visited = new bool[letters.Length];

            PrintLettersInternal2(visited, letters, new List<char>(), subsets);
            foreach (var s in subsets)
            {
                Console.WriteLine("|" + s.JoinStrings(","));
            }
        }
        void PrintLettersInternal2(bool[] visited, string letters, IList<char> current, IList<IList<char>> subsets)
        {
            if(current.Count == letters.Length) subsets.Add(new List<char>(current));

            for (int i = 0; i < letters.Length; i++)
            {
                if (visited[i]) continue;
                current.Add(letters[i]);
                visited[i] = true;
                PrintLettersInternal2(visited, letters, current, subsets);
                visited[i] = false;
                current.RemoveAt(current.Count - 1);
            }
        }



        [Test]
        [TestCase("ABC")]
        public void PrintLetters3(string letters)
        {
            var list = new List<string>();
            var chars = letters.ToCharArray();

            Permutate(chars, 0, letters.Length - 1, list);


            Console.WriteLine(list.JoinStrings("\n"));
        }
        void Permutate(char[] chars, int l, int r, IList<string> list)
        {
            if (l == r)
            {
                list.Add(new string(chars));
                return;
            }

            for (var i = l; i <= r; ++i)
            {
                Swap(chars, l, i);
                Permutate(chars, l + 1, r, list);
                Swap(chars, l, i);
            }
        }
        void Swap(char[] chars, int i, int j)
        {
            var temp = chars[i];
            chars[i] = chars[j];
            chars[j] = temp;
        }
    }
}