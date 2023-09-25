namespace AdvancedBinarySearchTree;

internal class TreeNode<T> where T : struct, IComparable<T>
{
    public T Data { get; set; } 
    public TreeNode<T>? Left { get; set; }
    public TreeNode<T>? Right { get; set; }
}