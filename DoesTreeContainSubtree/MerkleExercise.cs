using System;
using static MerkleTreeHelper;
using System.Collections.Generic;

namespace DoesTreeContainSubtree
{
    public class MerkleExercise
    {
        /// Given a bigger tree and smaller tree,
        /// return true iff the smaller tree is identical to a subtree in the bigger one.
        public static bool IsSubtree(Node bigTree, Node subTree)
        {
            int merkleValue = subTree.MerkleValue();

            // Map the Merkle value of every subtree in the big tree.
            // The value is a List<Node> because two subtrees may have the same Merkle value (however unlikely).
            var bigTreeMerkleValues = bigTree.AllMerkleValues();

            List<Node> matchingNodes;
            if (!bigTreeMerkleValues.TryGetValue(merkleValue, out matchingNodes))
            {
                // The smaller tree's Merkle value never appeared in the big tree, so it's definitely not a subtree.
                return false;
            }

            foreach (Node node in matchingNodes)
            {
                // we still need to compare, in case the Merkle values matched by coincidence.
                if (CompareTrees(node, subTree))
                {
                    return true;
                }
            }

            return false;
        }

        /// True iff treeA and treeB are identical trees.
        public static bool CompareTrees(Node treeA, Node treeB)
        {
            if (treeA == null && treeB == null)
            {
                return true;
            }

            if (treeA == null || treeB == null)
            {
                return false;
            }

            if (treeA.Value != treeB.Value)
            {
                return false;
            }

            return CompareTrees(treeA.LeftChild, treeB.LeftChild) &&
                    CompareTrees(treeA.RightChild, treeB.RightChild);
        }
    }
}
