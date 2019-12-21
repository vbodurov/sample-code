using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class WordLadder_Problem
    {
        [Test]
        [TestCase("hit", "cog", new []{"hot","dot","dog","lot","log","cog" }, 5)]
        [TestCase("hit", "cog", new[] { "hot", "dot", "dog", "lot", "log" }, 0)]
        public void WordLadder(string beginWord, string endWord, string[] wordList, int expect)
        {
            var result = new Solution().LadderLength(beginWord, endWord, wordList);
            Assert.That(result, Is.EqualTo(expect));
        }

        public class Solution
        {
            public int LadderLength(string beginWord, string endWord, IList<string> wordList)
            {
                var set = new HashSet<string>(wordList);
                if (!set.Contains(endWord)) return 0;
                var queue = new Queue<string>();
                queue.Enqueue(beginWord);

                int level = 1;
                while (queue.Count > 0)
                {
                    var size = queue.Count;
                    for (int i = 0; i < size; i++)
                    {
                        var currentWord = queue.Dequeue();
                        char[] wordChars = currentWord.ToCharArray();
                        for (int j = 0; j < wordChars.Length; j++)
                        {
                            var ch = wordChars[j];
                            for (char c = 'a'; c <= 'z'; c++)
                            {
                                if(wordChars[j] == c) continue;
                                wordChars[j] = c;
                                var newWord = new String(wordChars);
                                if (newWord == endWord) return level + 1;
                                if (set.Contains(newWord))
                                {
                                    queue.Enqueue(newWord);
                                    set.Remove(newWord);
                                }

                            }
                            wordChars[j] = ch;
                        }
                    }

                    level++;
                }

                return 0;
            }
        }
    }
}