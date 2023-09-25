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

    public void Remove(T value) => Root = RemoveNode(Root, value);
        
    private TreeNode<T>? RemoveNode(TreeNode<T>? root, T value)
    {
        if (Root is null) throw new InvalidOperationException("Tree is empty.");

        var comparison = value.CompareTo(root!.Data);

        switch (comparison)
        {
            case < 0:
                root.Left = RemoveNode(root.Left, value);
                break;
            case > 0:
                root.Right = RemoveNode(root.Right, value);
                break;
            default:
            {
                if (root.Left is null)
                {
                    return root.Right;
                }
                if (root.Right is null)
                {
                    return root.Left;
                }
                root.Data = AdditionalTreeFunctions<T>.FindMinValue(root.Right);
                root.Right = RemoveNode(root.Right, root.Data);
                break;
            }
        }
        return root;
    }

    public override string ToString()
    {
        if (Root is null) throw new InvalidOperationException("Tree is empty.");

        var height = AdditionalTreeFunctions<T>.GetTreeHeight(Root);
        var width = (int)Math.Pow(2, height + 1) - 1;  
        var tree = new string?[height, width]; 

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tree[i, j] = " ";
            }
        }

        AdditionalTreeFunctions<T>.FillTwoDimensionalArray(tree, Root, 0, 0, width - 1);
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