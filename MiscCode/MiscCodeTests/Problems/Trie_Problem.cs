using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;
using NUnit.Framework;

namespace MiscCodeTests.Problems
{
    [TestFixture]
    public class Trie_Problem
    {
        //https://leetcode.com/problems/implement-trie-prefix-tree/
        [Test]
        public void TestPrefixTree_Trie()
        {
            Trie trie = new Trie();

            trie.Insert("apple");
            
            Assert.That(trie.Search("apple"), Is.True);   // returns true
            Assert.That(trie.Search("app"), Is.False);     // returns false
            Assert.That(trie.StartsWith("app"), Is.True); // returns true
            trie.Insert("app");
            Console.WriteLine(trie.ToString());
            Assert.That(trie.Search("app"), Is.True);     // returns true
            
        }

        public class Trie
        {
            enum SearchResult : byte { FoundMid, FoundEnd, NotFound }
            class TrieNode
            {
                internal readonly Dictionary<char, TrieNode> Next = new Dictionary<char, TrieNode>();
                internal bool IsEndOfWord { get; set; }
                internal void Add(string word, int index)
                {
                    if (index >= word.Length) return;
                    if (index < 0) throw new Exception($"Invalid index {index} for word {word}");
                    var ch = word[index];
                    if (!Next.TryGetValue(ch, out var node))
                    {
                        Next[ch] = node = new TrieNode();
                    }
                    if (!node.IsEndOfWord && index == word.Length - 1) node.IsEndOfWord = true;
                    if (index < word.Length - 1) node.Add(word, index + 1);
                }
                internal SearchResult Find(string word, int index)
                {
                    if (index >= word.Length) return SearchResult.FoundMid;
                    var ch = word[index];
                    if (!Next.TryGetValue(ch, out var node))
                    {
                        return SearchResult.NotFound;
                    }
                    if (index == word.Length - 1 && node.IsEndOfWord)
                    {
                        return SearchResult.FoundEnd;
                    }

                    return node.Find(word, index + 1);
                }
            }
            readonly TrieNode _root = new TrieNode();
            /** Inserts a word into the trie. */
            public void Insert(string word)
            {
                _root.Add(word, 0);
            }
            /** Returns if the word is in the trie. */
            public bool Search(string word)
            {
                return _root.Find(word, 0) == SearchResult.FoundEnd;
            }

            /** Returns if there is any word in the trie that starts with the given prefix. */
            public bool StartsWith(string prefix)
            {
                return _root.Find(prefix, 0) != SearchResult.NotFound;
            }
        }
    }
}

