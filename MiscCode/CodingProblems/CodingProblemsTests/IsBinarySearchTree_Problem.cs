using NUnit.Framework;

namespace CodingProblemsTests
{
    [TestFixture]
    public class IsBinarySearchTree_Problem
    {
        [Test]
        public void IsBinarySearchTreeTest()
        {
            var correct =
                new Node
                    {
                        Value = 4,
                        Left = new Node
                                   {
                                       Value = 2,
                                       Left = new Node { Value = 1 },
                                       Right = new Node { Value = 3 }
                                   },
                        Right = new Node
                                    {
                                        Value = 5
                                    }
                    };
            var incorrect =
                new Node
                {
                    Value = 3,
                    Left = new Node
                    {
                        Value = 2,
                        Left = new Node { Value = 1 },
                        Right = new Node { Value = 4 }
                    },
                    Right = new Node
                    {
                        Value = 5
                    }
                };

            Node prev = null;
            Assert.That(IsBinarySearchTree(correct, ref prev), Is.True, "BST");
            prev = null;
            Assert.That(IsBinarySearchTree(incorrect, ref prev), Is.False, "not a BST");
        }

        static bool IsBinarySearchTree(Node node, ref Node prev)
        {
            if(node != null)
            {
                if(!IsBinarySearchTree(node.Left, ref prev))
                {
                    return false;
                }
                // Allows only distinct valued nodes 
                if (prev != null && node.Value <= prev.Value)
                {
                    return false;
                }

                prev = node;

                return IsBinarySearchTree(node.Right, ref prev);
            }

            return true;
        }

        public class Node
        {
            public int Value;
            public Node Left;
            public Node Right;
            public override string ToString() { return "{" + Value + "}"; }
        }
    }
    
}