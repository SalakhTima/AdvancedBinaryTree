namespace AdvancedBinarySearchTree;

internal static class AdditionalFunctions<T> where T : struct, IComparable<T>
{
    #region removal submethods
    public static TreeNode<T>? RemoveNode(TreeNode<T>? node, T value)
    {
        if (node is null) throw new InvalidOperationException("Tree is empty or value does not exist.");

        var comparison = value.CompareTo(node.Data); 

        switch (comparison)
        {
            case < 0:
                node.Left = RemoveNode(node.Left, value);
                break;
            case > 0:
                node.Right = RemoveNode(node.Right, value);
                break;
            default:
            {
                if (node.Left is null)
                {
                    return node.Right;
                }
                if (node.Right is null)
                {
                    return node.Left;
                }
                node.Data = FindMinValue(node.Right);
                node.Right = RemoveNode(node.Right, node.Data);
                break;
            }
        }
        return node;
    }

    public static T FindMinValue(TreeNode<T> node)
    {
        var minValue = node.Data;
        while (node.Left is not null)
        {
            minValue = node.Left.Data;
            node = node.Left;
        }
        return minValue;
    }
    #endregion

    #region traversal submethods
    public static string InfixTraverse(TreeNode<T>? root)
    {
        if (root is null) return string.Empty;

        var output = InfixTraverse(root.Left);
        output += root.Data + " ";
        output += InfixTraverse(root.Right);

        return output;
    }
     
    public static string PrefixTraverse(TreeNode<T>? root)
    {
        if (root is null) return string.Empty;

        var output = root.Data + " ";
        output += PrefixTraverse(root.Left);
        output += PrefixTraverse(root.Right);

        return output;
    }

    public static string PostfixTraverse(TreeNode<T>? root)
    {
        if (root is null) return string.Empty;

        var output = InfixTraverse(root.Left);
        output += InfixTraverse(root.Right);
        output += root.Data + " ";

        return output;
    }
    #endregion

    #region ToString submethod
    public static void FillTwoDimensionalArray(string?[,] tree, TreeNode<T>? node, int level, int left, int right)
    {
        while (true)
        {
            if (node is null) return;

            var middle = (left + right) / 2;
            tree[level, middle] = node.Data.ToString();

            FillTwoDimensionalArray(tree, node.Left, level + 1, left, middle - 1);
            node = node.Right;
            level += 1;
            left = middle + 1;
        }
    }
    #endregion

    #region tree balance submethods
    public static int GetHeight(TreeNode<T>? node)
    {
        if (node is null) return 0;

        var leftHeight = GetHeight(node.Left);
        var rightHeight = GetHeight(node.Right);

        return Math.Max(leftHeight, rightHeight) + 1;
    }

    private static int GetBalance(TreeNode<T>? node)
    {
        if (node is null) return 0;

        var leftHeight = GetHeight(node.Left);
        var rightHeight = GetHeight(node.Right);

        return leftHeight - rightHeight;
    }

    private static TreeNode<T> SmallLeftRotation(TreeNode<T> node)
    {
        var newRoot = node.Right;
        node.Right = newRoot!.Left;
        newRoot.Left = node;
        return newRoot;
    }

    private static TreeNode<T> SmallRightRotation(TreeNode<T> node)
    {
        var newRoot = node.Left;
        node.Left = newRoot!.Right;
        newRoot.Right = node;
        return newRoot;
    }

    private static TreeNode<T> BigLeftRotation(TreeNode<T> node)
    {
        node.Left = SmallLeftRotation(node.Left!);
        return SmallRightRotation(node);
    }

    private static TreeNode<T> BigRightRotation(TreeNode<T> node)
    {
        node.Right = SmallRightRotation(node.Right!);
        return SmallLeftRotation(node);
    }

    public static void BalanceTree(BinarySearchTree<T> tree)
    {
        if (tree.Root is null) throw new InvalidOperationException("Tree is empty.");

        switch (GetBalance(tree.Root))
        {
            case > 1 when GetBalance(tree.Root.Left) >= 0:
                tree.Root = SmallRightRotation(tree.Root);
                return;
            case > 1:
                tree.Root = BigRightRotation(tree.Root);
                return;
            case < -1 when GetBalance(tree.Root.Right) <= 0:
                tree.Root = SmallLeftRotation(tree.Root);
                return;
            case < -1:
                tree.Root = BigLeftRotation(tree.Root);
                return;
            default:
                return;
        }
    }
    #endregion

    #region operators overload submethods
    public static TreeNode<T>? IncreaseAllNodes(TreeNode<T>? node, T value)
    {
        if (node is null) return null;

        TreeNode<T> newRoot = new TreeNode<T>
        {
            Data = (T)((dynamic)node.Data + (dynamic)value),
            Left = IncreaseAllNodes(node.Left, value),
            Right = IncreaseAllNodes(node.Right, value)
        };

        return newRoot;
    }

    public static List<TreeNode<T>> GetAllNodes(TreeNode<T>? node)
    {
        List<TreeNode<T>> nodeList = new List<TreeNode<T>>();
        PrefixTraverse(node, nodeList);
        return nodeList;
    }

    private static void PrefixTraverse(TreeNode<T>? node, List<TreeNode<T>> nodeList)
    {
        if (node is null) return;
        PrefixTraverse(node.Left, nodeList);
        nodeList.Add(node);
        PrefixTraverse(node.Right, nodeList);
    }
    #endregion
}