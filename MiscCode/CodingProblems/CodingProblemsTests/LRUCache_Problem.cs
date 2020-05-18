using System.Collections.Generic;
using CodingProblemsTests.Utils;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class LRUCache_Problem
    {
        // https://leetcode.com/problems/lru-cache/
        [Test, Category(category.FundamentalAlgorythms)]
        [TestCase(
            @"[""LRUCache"",""put"",""put"",""get"",""put"",""get"",""put"",""get"",""get"",""get""]",
            "[[2],[1,1],[2,2],[1,01],[3,3],[2],[4,4],[1],[3,03],[4,04]]",
            "[null,null,null,1,null,-1,null,-1,3,4]"
            )]
        [TestCase(
            @"[""LRUCache"",""put"",""get"",""put"",""get"",""get""]",
            "[[1],[2,1],[2],[3,2],[2],[3]]",
            "[null,null,1,null,-1,2]"
            )]
        [TestCase(
            @"[""LRUCache"",""get"",""put"",""get"",""put"",""put"",""get"",""get""]",
            "[[2],[2],[2, 6],[1],[1, 5],[1, 2],[1],[2]]",
            "[null,-1,null,-1,null,null,2,6]"
            )]
        [TestCase(
            @"[""LRUCache"",""put"",""put"",""put"",""put"",""get"",""get""]",
            "[[2],[2, 1],[1, 1],[2, 3],[4, 1],[1],[2]]",
            "[null,null,null,null,null,-1,3]"
            )]
        public void LRUCacheTest(string actionsStr, string valuesStr, string expectStr)
        {
            var actions = ListParser.ToListOfStrings(actionsStr);
            var values = ListParser.ToListOfLists(valuesStr);
            var extected = ListParser.ToListOfNullableInts(expectStr);

            var cache = new LRUCache(values[0][0]);

            for (var i = 1; i < actions.Count; ++i)
            {
                var action = actions[i];
                var value = values[i];
                if (action == "put")
                {
                    cache.Put(value[0], value[1]);
                }
                else if (action == "get")
                {
                    var result = cache.Get(value[0]);
                    Assert.That(result, Is.EqualTo(extected[i]));
                }
            }
        }
    }
    public class LRUCache
    {

        readonly Dictionary<int, LinkedListNode<(int key, int value)>> _dict = new Dictionary<int, LinkedListNode<(int key, int value)>>();
        readonly LinkedList<(int key, int value)> _list = new LinkedList<(int key, int value)>();
        readonly int _capacity;

        public LRUCache(int capacity)
        {
            _capacity = capacity;
        }

        public int Get(int key)
        {
            if (!_dict.TryGetValue(key, out LinkedListNode<(int key, int value)> node))
                return -1;

            _list.Remove(node);
            _dict[key] = node = _list.AddFirst(node.Value);


            return node.Value.value;
        }

        public void Put(int key, int value)
        {
            if (_dict.TryGetValue(key, out var node))
            {
                _list.Remove(node);
                _dict.Remove(key);
            }

            if (_list.Count > 0 && 
                _list.Count >= _capacity)
            {
                var last = _list.Last;
                _list.Remove(last);
                _dict.Remove(last.Value.key);
            }

            _dict[key] = _list.AddFirst((key, value));
        }
    }
}