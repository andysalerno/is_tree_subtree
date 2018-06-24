public class Node
{
    public int Value { get; private set; }
    public Node LeftChild { get; private set; }
    public Node RightChild { get; private set; }

    public void SetLeftChild(Node leftChild)
    {
        this.LeftChild = leftChild;
    }

    public void SetRightChild(Node rightChild)
    {
        this.RightChild = rightChild;
    }

    public Node WithValue(int value)
    {
        this.Value = value;
        return this;
    }
}