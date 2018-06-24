using System;
using System.Collections.Generic;

public class TreeGenerator
{
    private static Random random = new Random();

    /// generate a binary tree with a given count of nodes 
    public static Node GenerateTree(int size)
    {
        if (size <= 0)
        {
            return null;
        }

        Node root = new Node().WithValue(random.Next());
        int createdNodes = 1;

        Node curNode = root;

        while (createdNodes < size)
        {
            Node nextNode = ExpandTree(curNode);
            curNode = nextNode;
            createdNodes++;
        }

        return root;
    }

    /// Given a node,
    /// expand it be adding either a left or right child (50% chance of either),
    /// then return either the same node or the new child (50% chance of either).
    private static Node ExpandTree(Node parent)
    {
        if (parent == null)
        {
            return null;
        }

        if (parent.LeftChild != null && parent.RightChild != null)
        {
            // parent already fully expanded, move down to one of the children
            Node toExpand = CoinFlip() ? parent.LeftChild : parent.RightChild;
            return ExpandTree(toExpand);
        }

        Node child = new Node().WithValue(random.Next());

        if (CoinFlip())
        {
            if (parent.LeftChild == null)
            {
                parent.SetLeftChild(child);
            }
            else
            {
                parent.SetRightChild(child);
            }
        }
        else
        {
            if (parent.RightChild == null)
            {
                parent.SetRightChild(child);
            }
            else
            {
                parent.SetLeftChild(child);
            }
        }

        // half the time we move down to the next node,
        // half the time we stay to potentially add another child.
        if (CoinFlip())
        {
            return child;
        }
        else
        {
            return parent;
        }
    }

    /// 50% chance of true.
    private static bool CoinFlip()
    {
        return (random.NextDouble() > 0.50);
    }


    // Clones a random subtree from the parent...
    // ...but it has a 50% chance to modify values and corrupt the subtree!
    public static Node SubtreeLottery(Node parentTree)
    {
        Node randomNode = PickRandomNode(parentTree);

        Node clonedTree = CloneTree(randomNode);

        if (CoinFlip())
        {

        }

        return clonedTree;
    }


    /// Picks a random node from the given root.
    private static Node PickRandomNode(Node parentTree)
    {
        Node curNode = parentTree;

        var allNodes = new List<Node>();
        var q = new Queue<Node>();

        q.Enqueue(curNode);

        while (q.Count > 0)
        {
            Node node = q.Dequeue();

            if (node == null)
            {
                continue;
            }

            allNodes.Add(node);

            q.Enqueue(node.LeftChild);
            q.Enqueue(node.RightChild);
        }

        int randIndex = random.Next(allNodes.Count);

        return allNodes[randIndex];
    }

    private static Node CloneTree(Node root)
    {
        if (root == null)
        {
            return null;
        }

        Node clonedRoot = new Node().WithValue(root.Value);

        Node leftCloned = CloneTree(root.LeftChild);
        Node rightCloned = CloneTree(root.RightChild);

        clonedRoot.LeftChild = leftCloned;
        clonedRoot.RightChild = rightCloned;

        return clonedRoot;
    }

    /// Guaranteed to modify the tree in some way.
    /// Might change some node's value, or unlink children, etc.
    private static void FiddleWithTree(Node root)
    {
        Node randomNode = PickRandomNode(root);

        if (CoinFlip())
        {
            randomNode.Value = -randomNode.Value;
        }
        else
        {
            // we could swap children
            if (CoinFlip())
            {
                if (randomNode.LeftChild != null && randomNode.RightChild != null)
                {
                    Node temp = randomNode.LeftChild;
                    randomNode.LeftChild = randomNode.RightChild;
                    randomNode.RightChild = temp;
                }

                // no children to swap, so instead add a rogue child
                else
                {
                    Node cuckoo = new Node().WithValue(random.Next());
                    randomNode.LeftChild = cuckoo;
                    // I have never been prouder of a variable name, btw
                }
            }

            // or unset one of them
            else
            {
                if (CoinFlip())
                {
                    randomNode.LeftChild = null;
                }
                else
                {
                    randomNode.RightChild = null;
                }
            }
        }
    }
}