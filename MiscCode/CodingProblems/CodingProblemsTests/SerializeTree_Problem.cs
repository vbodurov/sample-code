using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class SerializeTree_Problem
    {
        [Test, Category(category.SpecificProblems)]
        public void SerializeTree()
        {
            var codec = new Codec();

            var root = codec.deserialize("[1,2,3,null,null,4,5]");

            
            codec.deserialize(codec.serialize(root));
        }
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }
        public class Codec
        {

            // Encodes a tree to a single string.
            public string serialize(TreeNode root)
            {
                var list = new List<string>();
                var queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                while (queue.Count > 0)
                {

                    var size = queue.Count;
                    for (var i = 0; i < size; ++i)
                    {
                        var node = queue.Dequeue();

                        list.Add(node == null ? "null" : node.val.ToString());

                        if (node != null)
                        {


                            queue.Enqueue(node.left);
                            queue.Enqueue(node.right);
                        }
                    }
                }
                var serialized = "[" + string.Join(",", ((IEnumerable<string>)list).Reverse().SkipWhile(e => e == "null").Reverse()) + "]";
                //Console.WriteLine(serialized);
                return serialized;
            }
            // Decodes your encoded data to tree.
            public TreeNode deserialize(string str)
            {
                var arr = str.Trim('[').Trim(']').Split(',');
                if (arr.Length == 1 && arr[0] == "") return null;
                var data = new Queue<string>(arr);
                var nodes = new Queue<TreeNode>();


                return DequeueItem(data, nodes);
            }
            TreeNode DequeueItem(Queue<string> data, Queue<TreeNode> nodes)
            {
                var root = DequeueDataAsNode(data);
                if (root != null)
                {
                    nodes.Enqueue(root);
                    while (nodes.Count > 0)
                    {
                        var node = nodes.Dequeue();
                        node.left = DequeueDataAsNode(data);
                        node.right = DequeueDataAsNode(data);
                        if (node.left != null) nodes.Enqueue(node.left);
                        if (node.right != null) nodes.Enqueue(node.right);
                    }
                }
                return root;
            }
            TreeNode DequeueDataAsNode(Queue<string> data)
            {
                if (data.Count == 0) return null;
                var val = data.Dequeue();
                if (val == "null") return null;
                return new TreeNode(int.Parse(val));
            }
        }
    }
}