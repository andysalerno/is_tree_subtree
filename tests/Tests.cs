using System;
using Xunit;
using DoesTreeContainSubtree;

namespace tests
{
    public class Test
    {
        [Fact]
        public static void CanGenerateTreeWithSize()
        {
            const int treeSize = 500;
            Node root = TreeGenerator.GenerateTree(treeSize);

            int actualTreeSize = CountTreeNodes(root);

            Assert.Equal(treeSize, actualTreeSize);
        }

        [Fact]
        public static void CanCloneTree()
        {
            const int treeSize = 500;
            Node root = TreeGenerator.GenerateTree(treeSize);

            Node cloned = TreeGenerator.CloneTree(root);

            Assert.True(MerkleExercise.CompareTrees(root, cloned));
        }

        [Fact]
        public static void CanFiddleWithTree()
        {
            const int treeSize = 500;
            Node root = TreeGenerator.GenerateTree(treeSize);

            Node cloned = TreeGenerator.CloneTree(root);

            TreeGenerator.FiddleWithTree(cloned);

            // a fiddled tree should never be identical
            Assert.False(MerkleExercise.CompareTrees(root, cloned));
        }

        /// This is the true test of the Merkle magic.
        /// This is confirming we can detect subtrees with a Merkle hashing strategy.
        [Fact]
        public static void CanDetectSubtrees()
        {
            const int treeSize = 500;
            Node bigTreeRoot = TreeGenerator.GenerateTree(treeSize);

            Node randomBigTreeNode = TreeGenerator.PickRandomNode(bigTreeRoot);

            Node subtree = TreeGenerator.CloneTree(randomBigTreeNode);

            Assert.True(MerkleExercise.IsSubtree(bigTreeRoot, subtree));
        }

        [Fact]
        public static void CanDetectFiddledSubtrees()
        {
            const int treeSize = 500;
            Node bigTreeRoot = TreeGenerator.GenerateTree(treeSize);

            Node randomBigTreeNode = TreeGenerator.PickRandomNode(bigTreeRoot);

            Node subtree = TreeGenerator.CloneTree(randomBigTreeNode);

            TreeGenerator.FiddleWithTree(subtree);

            Assert.False(MerkleExercise.IsSubtree(bigTreeRoot, subtree));
        }

        private static int CountTreeNodes(Node root)
        {
            if (root == null)
            {
                return 0;
            }

            return 1 + CountTreeNodes(root.LeftChild) + CountTreeNodes(root.RightChild);
        }
    }
}