using System.Text;
namespace AdvancedBinarySearchTree;

internal class BinarySearchTree<T> where T : struct, IComparable<T> 
{
    public TreeNode<T>? Root { get; set; }
    public T Find(T value)
    {
        while (true)
        {
            if (Root is null) throw new ArgumentException("Tree is empty or value does not exist.");

            var comparison = value.CompareTo(Root.Data);
            switch (comparison)
            {
                case > 0:
                    Root = Root.Right;
                    break;
                case < 0:
                    Root = Root.Left;
                    break;
                default:
                    return Root.Data;
            }
        }
    }

    public void Insert(T value)
    {
        Root ??= new TreeNode<T> { Data = value };
        var current = Root;

        while (true)
        {
            switch (value.CompareTo(current.Data))
            {
                case > 0 when current.Right is null:
                    current.Right = new TreeNode<T> { Data = value };
                    return;
                case > 0:
                    current = current.Right;
                    break;
                case < 0 when current.Left is null:
                    current.Left = new TreeNode<T> { Data = value };
                    return;
                case < 0:
                    current = current.Left;
                    break;
                default:
                    return;
            }
        }
    }

    public void Remove(T value) => Root = AdditionalFunctions<T>.RemoveNode(Root, value);

    public static BinarySearchTree<T> operator +(BinarySearchTree<T> tree, T value)
    {
       return new BinarySearchTree<T>
       {
           Root = AdditionalFunctions<T>.IncreaseAllNodes(tree.Root, value)
       };
    }

    public static BinarySearchTree<T> operator +(BinarySearchTree<T> tree1, BinarySearchTree<T> tree2)
    {
        var nodes1 = AdditionalFunctions<T>.GetAllNodes(tree1.Root);
        var nodes2 = AdditionalFunctions<T>.GetAllNodes(tree2.Root);
        var newTree = new BinarySearchTree<T>();

        foreach (var node in nodes1) newTree.Insert(node.Data);
        foreach (var node in nodes2) newTree.Insert(node.Data);

        AdditionalFunctions<T>.BalanceTree(newTree);
        return newTree;
    }

    //public static BinarySearchTree<T> operator +(TreeNode<T> leftSubtree, TreeNode<T> rightSubtree)
    //{

    //}

    public override string ToString()
    {
        if (Root is null) throw new InvalidOperationException("Tree is empty.");

        var height = AdditionalFunctions<T>.GetHeight(Root);
        var width = (int)Math.Pow(2, height + 1) - 1;  
        var tree = new string?[height, width]; 

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tree[i, j] = " ";
            }
        }

        AdditionalFunctions<T>.FillTwoDimensionalArray(tree, Root, 0, 0, width - 1);
        StringBuilder output = new StringBuilder();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                output.Append(tree[i, j]);
            }
            output.AppendLine();
        }

        return output.ToString();
    }
}