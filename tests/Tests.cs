using System;
using Xunit;

namespace tests
{
    public class Test
    {
        [Fact]
        public static void TestGenerateTree()
        {
            const int treeSize = 500;
            Node root = TreeGenerator.GenerateTree(treeSize);

            int actualTreeSize = CountTreeNodes(root);

            Assert.Equal(treeSize, actualTreeSize);
        }

        public static int CountTreeNodes(Node root)
        {
            if (root == null)
            {
                return 0;
            }

            return 1 + CountTreeNodes(root.LeftChild) + CountTreeNodes(root.RightChild);
        }
    }
}