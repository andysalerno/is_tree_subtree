using System.Collections.Generic;

static class MerkleTreeHelper
{
    /// Find the Merkle value for a given node's subtree.
    public static int MerkleValue(this Node node)
    {
        int valueHash = node.Value.GetHashCode();

        if (node.LeftChild == null && node.RightChild == null)
        {
            return valueHash;
        }

        int leftMerkle = node.LeftChild?.MerkleValue() ?? 0;
        int rightMerkle = node.RightChild?.MerkleValue() ?? 0;

        // result is the combination of hashing this value with the subtree values
        // (overflowing the integer is very likely, but it's okay because in this case we desire that behavior)
        return (valueHash + leftMerkle + rightMerkle).GetHashCode();
    }

    /// Find the Merkle value for every subtree of the Node,
    /// and return the result as a dictionary mapping Merkle value -> [list of Nodes matching that value].
    public static Dictionary<int, List<Node>> AllMerkleValues(this Node node)
    {
        var result = new Dictionary<int, List<Node>>();

        // We could recursively call MerkleValue on each node in the tree,
        // but that would perform a depth-first-traversal on every single node, which is O(N^2).
        AllMerkleValuesHelper(node, result);

        return result;
    }

    /// This method is nearly identical to the MerkleValue() method,
    /// except it builds a Dictionary along the way of every Merkle value to the list of nodes that have that value.
    /// (With high probability, all the lists will have length 1, but that's not a guarantee).
    private static int AllMerkleValuesHelper(Node node, Dictionary<int, List<Node>> nodes)
    {
        if (node == null)
        {
            return 0;
        }

        int valueHash = node.Value.GetHashCode();

        if (node.LeftChild == null && node.RightChild == null)
        {
            AddOrCreateList(nodes, valueHash, node);
            return valueHash;
        }

        int leftMerkle = AllMerkleValuesHelper(node.LeftChild, nodes);
        int rightMerkle = AllMerkleValuesHelper(node.RightChild, nodes);

        // result is the combination of hashing this value with the subtree values
        // (overflowing the integer is very likely, but it's okay because in this case we desire that behavior)
        int derivedHash = (valueHash + leftMerkle + rightMerkle).GetHashCode();
        AddOrCreateList(nodes, derivedHash, node);
        return derivedHash;
    }

    private static void AddOrCreateList<Tkey, Tval>(Dictionary<Tkey, List<Tval>> dict, Tkey key, Tval value)
    {
        if (dict.TryGetValue(key, out List<Tval> values))
        {
            values.Add(value);
        }
        else
        {
            dict[key] = new List<Tval> { value };
        }
    }
}